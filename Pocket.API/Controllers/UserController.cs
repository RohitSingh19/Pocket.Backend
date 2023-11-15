using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Pocket.API.DTO;
using Pocket.API.Handlers;
using Pocket.API.Handlers.Attributes;
using Pocket.API.Models;
using Pocket.API.Services;


namespace Pocket.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("createUserName")] //POST: api/user/createUserName
        [ValidateTokenAndEmail]
        public async Task<IActionResult> CreateUserName(CreateUserNameDTO userNameDTO, string email)
        {
            var user = await _userService.GetUserByUsername(userNameDTO.UserName);
            if (user != null) return BadRequest($"({userNameDTO.UserName}) {Constants.Messages.UserNameAlreadyTaken}");

            var pocketProfile = new PocketProfile()
            {
                Email = email,
                UserName = userNameDTO.UserName,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
            };

            /*create user's pocket profile with the user-name*/
            var createUserProfile = _userService.CreateUserName(pocketProfile);
            var updateUserName = _userService.AddUserNameForUser(email, userNameDTO.UserName);
           
           await Task.WhenAll(updateUserName, createUserProfile);

           return Ok(new ApiResponse<CreateUserNameDTO>
           { 
                Success = true,
                Message = Constants.Messages.UserNameCreated,
                Data = new CreateUserNameDTO { Email = email, UserName = userNameDTO.UserName }
           });
        }

        [HttpPost("createAdditionalDetails")] //POST: api/user/createAdditionalDetails
        [ValidateTokenAndEmail]
        public async Task<IActionResult> CreateAdditionalDetails(UserDetail userDetail, string email)
        {
            return Ok(new ApiResponse<object>
            { 
                Data = await this._userService.AddAdditonalDetails(userDetail, email),
                Success = true,
                Message = Constants.Messages.UserAdditionalDetails
            });
        }

        [HttpGet("getAllProfessions")] //GET: api/user/getAllProfessions
        public async Task<IActionResult> GetAllProfessions()
        {
            return Ok(new ApiResponse<object> { 
                Data = await Task.FromResult(this._userService.GetProfessions()),
                Message = Constants.Messages.UserProfessions,
                Success = true
            });
        }

        [HttpGet("getUserProfile")] //GET: api/user/getUserProfile
        [ValidateTokenAndEmail] 
        public async Task<IActionResult> GetUserProfile(string email)
        {
            return Ok(new ApiResponse<UserProfileDTO>
            {
                Data = await this._userService.GetUserProfile(email),
                Success = true,
                Message = Constants.Messages.UserProfileFetched
            });
        }
    }
}
