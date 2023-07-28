﻿using Cinema.Core.Contracts;
using Cinema.Core.Utilities;
using Cinema.Data;
using Cinema.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly CinemaDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImageService(CinemaDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<string> UploadPhotoAsync(string imageType, IFormFile formFile)
        {
            string uniqueFileName = null;

            if (formFile != null)
            {
                string photosFolder = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolder, imageType);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string photoPathAndName = Path.Combine(photosFolder, uniqueFileName);

                Directory.CreateDirectory(photosFolder);
                using FileStream fileStream = new(photoPathAndName, FileMode.Create);

                formFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
        public async Task<bool> DeleteImageAsync(string imageType, string imageUrl)
        {
            string profilePhotoFileName = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolder, imageType, imageUrl);
            if (System.IO.File.Exists(profilePhotoFileName))
            {
                System.IO.File.Delete(profilePhotoFileName);
                return true;
            }
            return false;
        }

        public async Task<bool> ImageExistsAsync(string imageType, string imageUrl)
        {
            string profilePhotoFileName = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolder, imageType, imageUrl);

            return File.Exists(profilePhotoFileName);
        }
        public async Task ReplaceWithDefaultIfNotPresentAsync(string userEmail, string imageType, string imageUrl)
        {
            bool exists = await this.ImageExistsAsync(imageType, imageUrl);
            if (!exists)
            {
                string photosFolder = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolder);

                Directory.CreateDirectory(photosFolder);

                string photoUrl = $"{Guid.NewGuid().ToString()}.png";
                // using FileStream fileStream = new(Path.Combine(photosFolder, photoUrl), FileMode.Create);

                File.Copy(Path.Combine(photosFolder, "defaults", "man-avatar-profile-picture-vector-illustration_268834-538-removebg-preview.png"), Path.Combine(photosFolder, imageType, photoUrl));

                var user = await _userManager.FindByEmailAsync(userEmail);
                user.ProfilePictureUrl = photoUrl;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetRootPathAsync(string imageType)
        {
            return Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolder, imageType);
        }
    }
}