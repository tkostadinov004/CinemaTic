using CinemaTic.Data.Models;
using CinemaTic.Data;
using Microsoft.AspNetCore.Identity;
using CinemaTic.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using CinemaTic.ViewModels.Admin;
using CinemaTic.Data.Enums;
using CinemaTic.Core.Utilities;
using System.Text.RegularExpressions;
using System.Globalization;
using CinemaTic.Extensions;

namespace CinemaTic.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaDbContext _context;
        private readonly IImageService _imageService;

        public AdminService(
            UserManager<ApplicationUser> userManager, CinemaDbContext context, IImageService imageService)
        {
            _userManager = userManager;
            _context = context;
            _imageService = imageService;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> DeleteAccountAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }
            return false;
        }

        public async Task<bool> DemoteUserAsync(string id)
        {
            var owner = await _userManager.FindByIdAsync(id);
            if (owner != null && await _userManager.IsInRoleAsync(owner, "Owner"))
            {
                await _userManager.RemoveFromRoleAsync(owner, "Owner");
                await _userManager.AddToRoleAsync(owner, "Customer");
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> PromoteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (_userManager.IsInRoleAsync(user, "Owner").Result == false)
            {
                await _userManager.RemoveFromRoleAsync(user, "Customer");
                await _userManager.AddToRoleAsync(user, "Owner");
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Cinema>> GetAllCinemasAsync()
        {
            return await _context.Cinemas.Include(i => i.Owner).ToListAsync();
        }
        public async Task<IEnumerable<AdminAllCinemasViewModel>> SearchAndFilterCinemasAsync(string searchText, string filterValue, string sortBy, int? pageNumber)
        {
            var cinemas = _context.Cinemas.Include(i => i.Owner).OrderBy(i => i.Name).AsQueryable();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                cinemas = cinemas.Where(i => i.Name.ToLower().StartsWith(searchText.ToLower()));
            }
            if (string.IsNullOrEmpty(filterValue) == false && Regex.IsMatch(filterValue, @"^[0-9]*$"))
            {
                int enumValue = int.Parse(filterValue);
                if (enumValue > -1)
                {
                    cinemas = cinemas.Where(i => i.ApprovalStatus == (ApprovalStatus)enumValue);
                }
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        cinemas = cinemas.OrderBy(i => i.Name);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.Name);
                        }
                        break;
                    case "status":
                        cinemas = cinemas.OrderBy(i => i.ApprovalStatus);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.ApprovalStatus);
                        }
                        break;
                    case "addedon":
                        cinemas = cinemas.OrderBy(i => i.FoundedOn);
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => i.FoundedOn);
                        }
                        break;
                    case "addedby":
                        cinemas = cinemas.OrderBy(i => $"{i.Owner.FirstName} {i.Owner.LastName}");
                        if (sortDirection == "desc")
                        {
                            cinemas = cinemas.OrderByDescending(i => $"{i.Owner.FirstName} {i.Owner.LastName}");
                        }
                        break;
                }
            }
            return await PaginatedList<AdminAllCinemasViewModel>.CreateAsync(cinemas.Select(i => new AdminAllCinemasViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Status = (i.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : (i.ApprovalStatus == ApprovalStatus.PendingApproval ? "Pending approval" : "Denied approval")),
                AddedBy = $"{i.Owner.FirstName} {i.Owner.LastName}",
                AddedById = i.OwnerId,
                AddedOn = i.FoundedOn.ToString(Constants.DateTimeFormat),
                ImageUrl = i.ImageUrl
            }), pageNumber ?? 1, 10);
        }

        public async Task<UserDetailsViewModel> GetUserDetailsViewModelByIdAsync(string id, int? actionPageNumber)
        {
            var user = await _context.Users.Include(i => i.UserActions).FirstOrDefaultAsync(i => i.Id == id);

            return new UserDetailsViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                ImageUrl = user.ProfilePictureUrl,
                Actions = await PaginatedList<UserActionViewModel>.CreateAsync(user.UserActions.Select(i => new UserActionViewModel
                {
                    Id = i.Id,
                    Action = $"({i.Date.ToString("MM/dd/yyyy")}) {i.Message}",
                    Type = i.Type switch
                    {
                        UserActionType.Create => "create",
                        UserActionType.Read => "read",
                        UserActionType.Update => "update",
                        UserActionType.Delete => "delete",
                        UserActionType.AccountActions => "account-actions",
                        _ => ""
                    },
                    Date = i.Date.ToString("MM/dd/yyyy")
                }), actionPageNumber ?? 1, 5),
                Roles = string.Join(", ", await _userManager.GetRolesAsync(user))
            };
        }

        public async Task<AdminCinemaDetailsViewModel> GetCinemaDetailsViewModelByIdAsync(int? id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == id);

            return new AdminCinemaDetailsViewModel
            {
                Id = cinema.Id,
                Status = (cinema.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : (cinema.ApprovalStatus == ApprovalStatus.PendingApproval ? "Pending approval" : "Denied approval")),
                Description = cinema.Description,
                FoundedOn = cinema.FoundedOn.ToString(Constants.DateTimeFormat),
                Name = cinema.Name,
                ImageUrl = cinema.ImageUrl
            };

        }

        public async Task<ChangeCinemaApprovalStatusViewModel> GetChangeApprovalStatusViewModelByIdAsync(int? id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == id);
            return new ChangeCinemaApprovalStatusViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                ImageUrl = cinema.ImageUrl,
                ApprovalCode = (int)cinema.ApprovalStatus
            };
        }

        public async Task ChangeApprovalStatusByIdStatusAsync(int? id, ApprovalStatus approvalCode)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == id);
            if(cinema != null)
            {
                cinema.ApprovalStatus = approvalCode;
                if(approvalCode == ApprovalStatus.DeniedApproval)
                {
                    _context.CinemasMovies.RemoveRange(_context.CinemasMovies.Where(i => i.CinemaId == cinema.Id));
                    _context.CinemasMoviesTimes.RemoveRange(_context.CinemasMoviesTimes.Where(i => i.CinemaId == cinema.Id));
                    _context.CustomersCinemas.RemoveRange(_context.CustomersCinemas.Where(i => i.CinemaId == cinema.Id));
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserDetailsViewModel>> SearchAndFilterUsersAsync(string searchText, string filterValue, string sortBy, int? pageNumber)
        {
            var users = _context.Users.Where(i => i.UserName != "admin@admin.com").ToList().OrderBy(i => $"{i.FirstName} {i.LastName}").AsEnumerable();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                users = users.Where(i => $"{i.FirstName} {i.LastName}".ToLower().StartsWith(searchText.ToLower())).ToList();
            }
            if (string.IsNullOrEmpty(filterValue) == false && filterValue != "-Select a role-")
            {
                var userIdsInRole = (await _userManager.GetUsersInRoleAsync(filterValue)).Select(i => i.Id).ToList();
                users = users.Where(i => userIdsInRole.Contains(i.Id)).ToList();
            }
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                var sortParameter = sortBy.Split('-')[0];
                var sortDirection = sortBy.Split('-')[^1];

                switch (sortParameter)
                {
                    case "name":
                        users = users.OrderBy(i => $"{i.FirstName} {i.LastName}");
                        if (sortDirection == "desc")
                        {
                            users = users.OrderByDescending(i => $"{i.FirstName} {i.LastName}");
                        }
                        break;
                    case "email":
                        users = users.OrderBy(i => i.Email);
                        if (sortDirection == "desc")
                        {
                            users = users.OrderByDescending(i => i.Email);
                        }
                        break;
                    case "role":
                        users = users.OrderBy(i => string.Join(", ", _userManager.GetRolesAsync(i).Result));
                        if (sortDirection == "desc")
                        {
                            users = users.OrderByDescending(i => string.Join(", ", _userManager.GetRolesAsync(i).Result));
                        }
                        break;
                }
            }
            foreach (var item in users)
            {
                await _imageService.ReplaceWithDefaultIfNotPresentAsync(item.Email, "Users", item.ProfilePictureUrl);
            }
            return await PaginatedList<UserDetailsViewModel>.CreateAsync(users.Select(i => new UserDetailsViewModel
            {
                Id = i.Id,
                UserName = i.UserName,
                FullName = $"{i.FirstName} {i.LastName}",
                ImageUrl = i.ProfilePictureUrl,
                Roles = string.Join(", ", _userManager.GetRolesAsync(i).Result)
            }), pageNumber ?? 1, 8);
        }

        public async Task<AdminUserCRUDViewModel> GetAdminUserCRUDPartialAsync(string id)
        {
            var user = await this.GetUserByIdAsync(id);

            return new AdminUserCRUDViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Roles = string.Join(", ", await _userManager.GetRolesAsync(user))
            };
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            return await _context.Users.AnyAsync(i => i.Id == id);
        }
        public async Task<bool> CinemaExistsAsync(int? id)
        {
            return await _context.Cinemas.AnyAsync(i => i.Id == id);
        }
    }
}
