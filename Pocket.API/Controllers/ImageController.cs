using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocket.API.DTO;
using Pocket.API.Handlers;
using Pocket.API.Services;
using System.Net;

namespace Pocket.API.Controllers
{
    [Route("api/image")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly ICloudinaryImageUploadService _imageUploadService;

        public ImageController(ICloudinaryImageUploadService imageUploadService)
        {
            _imageUploadService = imageUploadService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            try
            {
                var result = await _imageUploadService.UploadImageAsync(file);

                return Ok(new ApiResponse<ImageUploadResult>
                {
                    Success = true,
                    Message = "Image Uploaded Successfully",
                    Data = result,
                    StatusCode = (int)HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
