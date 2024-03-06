using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocket.API.DTO;
using Pocket.API.Handlers;
using Pocket.API.Models;
using Pocket.API.Services;
using System.Net;

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
        /// <summary>
        /// Returns all social profiles
        /// </summary>
        /// <returns></returns>
        [HttpGet("getSocialProfiles")] //GET: api/profile/getSocialProfiles
        public async Task<IActionResult> GetSocialProfiles() 
        {
            return Ok(new ApiResponse<IEnumerable<SocialProfilesDTO>>
            {
                Success = true,
                Message = null,
                Data = await _pocketService.GetSocialProfiles(),
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        /// <summary>
        /// Return social profile filtered with profile name
        /// </summary>
        /// <param name="profileName"></param>
        /// <returns></returns>
        [HttpGet("getSocialProfile")] //GET: api/profile/getSocialProfile
        public async Task<IActionResult> GetSocialProfiles(string profileName)
        {
            return Ok(new ApiResponse<SocialProfile>
            {
                Success = true,
                Message = null,
                Data = await _pocketService.GetSocialProfile(profileName),
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        
        [HttpPost("addPocketProfile")] //POST: api/profile/addPocketProfile
        public async Task<IActionResult> AddPocketProfile(CreatePocketProfileItemDTO createPocketProfile, string userName)
        { 
            var result = await _pocketService.AddProfile(createPocketProfile, userName);

            return Ok(new ApiResponse<object> { 
                Data = result ? true : false,
                Message = Constants.Messages.UserProfileAdded,
                Success = result ? true : false,
                StatusCode = result ? (int)HttpStatusCode.Created : (int)HttpStatusCode.BadRequest
            });
        }

        [HttpGet("getPocketProfile")] //GET: api/profile/getPocketProfile
        public async Task<IActionResult> GetPocketProfile(string userName)
        {
            return Ok(new ApiResponse<PocketProfile>
            {
                Success = true,
                Message = null,
                Data = await _pocketService.GetPocketProfile(userName),
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPut("updatePocketProfile")] //PUT: api/profile/updatePocketProfile
        public async Task<IActionResult> UpdatePocketProfile([FromBody] PocketProfileUpdateDTO pocketProfileUpdateDTO, string userName)
        {
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = null,
                Data = await _pocketService.UpdatePocketProfile(userName, pocketProfileUpdateDTO),
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpDelete("deletePocketProfile")] //PUT: api/profile/deletePocketProfile
        public async Task<IActionResult> UpdatePocketProfile(string userName, string profileName)
        {
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = null,
                Data = await _pocketService.DeletePocketProfile(userName, profileName),
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
