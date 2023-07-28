using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface IImageService
    {
        Task<string> GetRootPathAsync(string imageType);
        Task<string> UploadPhotoAsync(string imageType, IFormFile formFile);
        Task<bool> DeleteImageAsync(string imageType, string imageUrl);
        Task<bool> ImageExistsAsync(string imageType, string imageUrl);
        Task ReplaceWithDefaultIfNotPresentAsync(string userEmail, string imageType, string imageUrl);
    }
}
