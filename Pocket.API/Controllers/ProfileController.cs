using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocket.API.DTO;
using Pocket.API.Handlers;
using Pocket.API.Models;
using Pocket.API.Services;

namespace Pocket.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IPocketService _pocketService;
        public ProfileController(IPocketService pocketService)
        {
            _pocketService = pocketService;
        }

        [HttpGet("getSocialProfiles")]
        public async Task<IActionResult> GetSocialProfiles() 
        {
            return Ok(new ApiResponse<IEnumerable<SocialProfile>>
            {
                Success = true,
                Message = null,
                Data = await _pocketService.GetSocialProfiles()
            });
        }

        [HttpGet("getSocialProfile")]
        public async Task<IActionResult> GetSocialProfiles(string type)
        {
            return Ok(new ApiResponse<SocialProfile>
            {
                Success = true,
                Message = null,
                Data = await _pocketService.GetSocialProfile(type)
            });
        }

        [HttpPost("addPocketProfile")]
        public async Task<IActionResult> AddPocketProfile(CreatePocketProfileItemDTO createPocketProfile, string userName)
        { 
            var result = await this._pocketService.AddProfile(createPocketProfile, userName);
            return Ok(new ApiResponse<object> { 
                Data = result ? true : false,
                Message = "Profile added successfully",
                Success = result ? true : false,
            });
        }
    }
}
