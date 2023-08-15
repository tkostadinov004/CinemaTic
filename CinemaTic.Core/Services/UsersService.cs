using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using CinemaTic.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogService _logger;
        private readonly IImageService _imageService;

        public UsersService(CinemaDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogService logger, IImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _imageService = imageService;
        }
        /// <summary>
        /// <para>Gets the <see cref="ApplicationUser"/> by a given email.</para>
        /// </summary>
        /// <returns>An <see cref="ApplicationUser"/> object</returns>
        public async Task<ApplicationUser> GetUserByEmailAsync(string userEmail)
        {
            return await _userManager.FindByEmailAsync(userEmail);
        }
        /// <summary>
        /// <para>Gets the sidebar view model of a given <see cref="ApplicationUser"/>.</para>
        /// </summary>
        /// <returns>A <see cref="SidebarUserViewModel"/> object</returns>
        public async Task<SidebarUserViewModel> GetSidebarViewModelByEmailAsync(string userEmail)
        {
            var user = await this.GetUserByEmailAsync(userEmail);
            if(user != null)
            {
                return new SidebarUserViewModel
                {
                    Name = $"{user.FirstName} {user.LastName}",
                    ImageUrl = user.ProfilePictureUrl
                };
            }
            return null;
        }
        /// <summary>
        /// <para>Gets a view model used for changing the password of a given <see cref="ApplicationUser"/>.</para>
        /// </summary>
        /// <returns>A <see cref="ChangePasswordViewModel"/> object</returns>
        public async Task<ChangePasswordViewModel> GetChangePasswordViewModelAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if(user != null )
            {
                return new ChangePasswordViewModel
                {
                    Id = user.Id,
                    Email = user.Email
                };
            }
            return null;
        }
        /// <summary>
        /// <para>Changes the password of a given <see cref="ApplicationUser"/>.</para>
        /// </summary>
        public async Task ChangePasswordAsync(ChangePasswordViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id);
            await _userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);
            await _signInManager.RefreshSignInAsync(user);
            await _logger.LogActionAsync(UserActionType.AccountActions, LogMessages.ChangePasswordMessage);
        }
        /// <summary>
        /// <para>Gets a view model used for changing the profile picture of a given <see cref="ApplicationUser"/>.</para>
        /// </summary>
        /// <returns>A <see cref="ChangeProfilePictureViewModel"/> object</returns>
        public async Task<ChangeProfilePictureViewModel> GetChangeProfilePictureViewModelAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if(user != null)
            {
                return new ChangeProfilePictureViewModel
                {
                    Id = user.Id
                };
            }
            return null;
        }
        /// <summary>
        /// <para>Changes the profile picture of a given <see cref="ApplicationUser"/>.</para>
        /// </summary>
        public async Task ChangeProfilePictureViewModelAsync(ChangeProfilePictureViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id);

            await _imageService.DeleteImageAsync("Users", user.ProfilePictureUrl);
            user.ProfilePictureUrl = await _imageService.UploadPhotoAsync("Users", viewModel.Image);

            await _context.SaveChangesAsync();
        }
    }
}
