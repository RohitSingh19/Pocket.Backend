using CloudinaryDotNet.Actions;

namespace Pocket.API.Services
{
    public interface ICloudinaryImageUploadService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    }

}
