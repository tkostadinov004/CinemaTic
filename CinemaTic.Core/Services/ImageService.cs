using CinemaTic.Core.Contracts;
using CinemaTic.Core.Utilities;
using CinemaTic.Data;
using CinemaTic.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.Services
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
        /// <summary>
        /// <para>Uploads an image to the application storage.</para>
        /// <para>If no image is attached, the method throws an <see cref="ArgumentNullException"/>.</para>
        /// </summary>
        /// <returns>The unique image url as a <see cref="string"/></returns>
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
            else
            {
                throw new ArgumentNullException("There is no uploaded photo");
            }
            return uniqueFileName;
        }
        /// <summary>
        /// <para>Deletes an image from the application storage.</para>
        /// </summary>
        /// <returns>A <see cref="bool"/> value showing whether the image was successfully deleted</returns>
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
        /// <summary>
        /// <para>Checks whether an image exists in the application storage.</para>
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> ImageExistsAsync(string imageType, string imageUrl)
        {
            string profilePhotoFileName = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolder, imageType, imageUrl);

            return File.Exists(profilePhotoFileName);
        }
        /// <summary>
        /// <para>Sets a default profile picture for a user if no such profile picture exists in the application storage.</para>
        /// </summary>
        /// <returns>A <see cref="bool"/> value showing whether a replacement was made</returns>
        public async Task<bool> ReplaceWithDefaultIfNotPresentAsync(string userEmail, string imageType, string imageUrl)
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
                return true;
            }
            return false;
        }
    }
}
