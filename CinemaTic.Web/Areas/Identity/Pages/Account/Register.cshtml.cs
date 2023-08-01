﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data.Enums;
using CinemaTic.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CinemaTic.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ILogService _logService;
        private readonly IImageService _imageService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ILogService logService,
            IImageService imageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _logService = logService;
            _imageService = imageService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Enter an email address!")]
            [EmailAddress(ErrorMessage = "Enter a correct email address!")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Enter a password!")]
            [StringLength(100, ErrorMessage = "The password must contain at least {2} characters and no more than {1} characters!", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The two passwords don't match!")]
            [Required(ErrorMessage = "Enter a password!")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage = "The first name should be at least {2} characters long and no more than {1} characters long!", MinimumLength = 5)]
            [Required(ErrorMessage = "Enter a first name")]
            [Display(Name = "First name")]
            public string FirstName { get; set; }
            [DataType(DataType.Text)]
            [Required(ErrorMessage = "Enter a last name")]
            [StringLength(100, ErrorMessage = "The last name should be at least {2} characters long and no more than {1} characters long!", MinimumLength = 5)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }
            [Display(Name = "Profile picture")]
            [Required(ErrorMessage = "Upload a profile picture")]
            public IFormFile Image { get; set; }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    CreationDate = DateTime.Now,
                    ProfilePictureUrl = await _imageService.UploadPhotoAsync("Users", Input.Image)
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Customer");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    TempData["UserRegistered"] = true;
                    TempData["NewUser"] = true;
                    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}