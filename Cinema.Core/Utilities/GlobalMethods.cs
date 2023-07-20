using Cinema.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Core.Utilities
{
    public static class GlobalMethods
    {
        public static string UploadPhoto(string imageType, IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;

            if (formFile != null)
            {
                string photosFolder = Path.Combine(webHostEnvironment.WebRootPath, Constants.ImagesFolder, imageType);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string photoPathAndName = Path.Combine(photosFolder, uniqueFileName);

                Directory.CreateDirectory(photosFolder);
                using FileStream fileStream = new(photoPathAndName, FileMode.Create);

                formFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
        public static async Task<bool> DeleteImage(string imageType, string imageUrl, CinemaDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            string profilePhotoFileName = Path.Combine(webHostEnvironment.WebRootPath, Constants.ImagesFolder, imageType, imageUrl);

            if (await context.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(profilePhotoFileName))
                {
                    System.IO.File.Delete(profilePhotoFileName);
                    return true;
                }
            }
            return false;
        }
        public static IEnumerable<string> GetCountries()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            var countries = cultures.Select(cult => (new RegionInfo(cult.LCID)).TwoLetterISORegionName).Distinct();

            var userCountries = countries.Select(i => IsoNames.CountryNames.GetName(CultureInfo.GetCultureInfo("en"), i)).Where(i => i != null).OrderBy(i => i).ToList();

            userCountries.Insert(0, null);
            return userCountries;
        }
    }
}
