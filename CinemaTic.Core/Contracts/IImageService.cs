using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Core.Contracts
{
    public interface IImageService
    {
        Task<string> UploadPhotoAsync(string imageType, IFormFile formFile);
        Task<bool> DeleteImageAsync(string imageType, string imageUrl);
        Task<bool> ImageExistsAsync(string imageType, string imageUrl);
        Task<bool> ReplaceWithDefaultIfNotPresentAsync(string userEmail, string imageType, string imageUrl);
    }
}
