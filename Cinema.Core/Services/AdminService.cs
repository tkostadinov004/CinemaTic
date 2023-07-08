using Cinema.Data.Models;
using Cinema.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Cinema.ViewModels.Admin;
using Cinema.Data.Enums;
using Cinema.Utilities;
using Cinema.ViewModels.Cinemas;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Text.RegularExpressions;

namespace Cinema.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaDbContext _context;

        public AdminService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, CinemaDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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
                await _userManager.AddToRoleAsync(owner, "Visitor");
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
                await _userManager.RemoveFromRoleAsync(user, "Visitor");
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
                cinemas = cinemas.Where(i => i.Name.StartsWith(searchText));
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

        public async Task<UserDetailsViewModel> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

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
                })
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

        public async Task ChangeApprovalStatusAsync(int id, int approvalCode)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(i => i.Id == id);
            cinema.ApprovalStatus = (ApprovalStatus)approvalCode;

            await _context.SaveChangesAsync();
        }
    }
}
