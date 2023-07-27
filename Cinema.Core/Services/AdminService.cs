using Cinema.Data.Models;
using Cinema.Data;
using Microsoft.AspNetCore.Identity;
using Cinema.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Cinema.ViewModels.Admin;
using Cinema.Data.Enums;
using Cinema.Core.Utilities;
using System.Text.RegularExpressions;

namespace Cinema.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CinemaDbContext _context;

        public AdminService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, CinemaDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task DeleteAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task DemoteUser(string id)
        {
            var owner = await _userManager.FindByIdAsync(id);
            if (_userManager.IsInRoleAsync(owner, "Owner").Result)
            {
                await _userManager.RemoveFromRoleAsync(owner, "Owner");
                await _userManager.AddToRoleAsync(owner, "Customer");
            }
        }

        public async Task<ApplicationUser> FindById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task PromoteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (_userManager.IsInRoleAsync(user, "Owner").Result == false)
            {
                await _userManager.RemoveFromRoleAsync(user, "Customer");
                await _userManager.AddToRoleAsync(user, "Owner");
            }
        }

        public async Task<IEnumerable<Cinema.Data.Models.Cinema>> GetAllCinemasAsync()
        {
            return await _context.Cinemas.Include(i => i.Owner).ToListAsync();
        }
        public async Task<IEnumerable<AdminAllCinemasViewModel>> SearchAndFilterCinemasAsync(string searchText, string filterValue, string sortBy)
        {
            var cinemas = await this.GetAllCinemasAsync();
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
            return cinemas.Select(i => new AdminAllCinemasViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Status = (i.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : (i.ApprovalStatus == ApprovalStatus.PendingApproval ? "Pending approval" : "Denied approval")),
                AddedBy = $"{i.Owner.FirstName} {i.Owner.LastName}",
                AddedById = i.OwnerId,
                AddedOn = i.FoundedOn.ToString(Constants.DateTimeFormat),
                ImageUrl = i.ImageUrl
            });
        }

        public async Task<UserDetailsViewModel> GetUserDetailsAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return new UserDetailsViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                ImageUrl = user.ProfilePictureUrl,
                Actions = user.UserActions.Select(i => new UserActionViewModel
                {
                    Id = i.Id,
                    Action = i.Message,
                    TypeCode = i.Type
                }),
                Roles = string.Join(", ", await _userManager.GetRolesAsync(user))
            };
        }

        public async Task<AdminCinemaDetailsViewModel> GetCinemaDetailsAsync(int? id)
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

        public async Task<ChangeCinemaApprovalStatusViewModel> GetCASViewModelAsync(int id)
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

        public async Task ChangeApprovalStatusAsync(int id, ApprovalStatus approvalCode)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == id);
            cinema.ApprovalStatus = approvalCode;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDetailsViewModel>> SearchAndFilterUsersAsync(string searchText, string filterValue)
        {
            var users = await _context.Users.Where(i => i.UserName != "admin@admin.com").ToListAsync();
            if (string.IsNullOrEmpty(searchText) == false)
            {
                users = users.Where(i => $"{i.FirstName} {i.LastName}".ToLower().StartsWith(searchText.ToLower())).ToList();
            }
            if (string.IsNullOrEmpty(filterValue) == false)
            {
                var userIdsInRole = (await _userManager.GetUsersInRoleAsync(filterValue)).Select(i => i.Id).ToList();
                users = users.Where(i => userIdsInRole.Contains(i.Id)).ToList();
            }
            return users.Select(i => new UserDetailsViewModel
            {
                Id = i.Id,
                UserName = i.UserName,
                FullName = $"{i.FirstName} {i.LastName}",
                ImageUrl = i.ProfilePictureUrl,
                Actions = i.UserActions.Select(i => new UserActionViewModel
                {
                    Id = i.Id,
                    Action = i.Message,
                    TypeCode = i.Type
                }),
                Roles = string.Join(", ", _userManager.GetRolesAsync(i).Result)
            });
        }

        public async Task<AdminUserCRUDViewModel> GetAdminUserCRUDPartialAsync(string id)
        {
            var user = await this.FindById(id);

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
        public async Task<bool> CinemaExistsAsync(int id)
        {
            return await _context.Cinemas.AnyAsync(i => i.Id == id);
        }
    }
}
