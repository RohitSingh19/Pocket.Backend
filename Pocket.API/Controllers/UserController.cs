using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocket.API.DTO;
using Pocket.API.Handlers;
using Pocket.API.Handlers.Attributes;
using Pocket.API.Models;
using Pocket.API.Services;
using System.Net;

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


        [HttpGet("userNameTaken")] //GET: api/user/userNameTaken
        [AllowAnonymous]
        public async Task<IActionResult> UserNameTaken(string userName)
        {
            var user = await _userService.GetUserByUsername(userName);
            
            var response = new ApiResponse<bool>
            {
                Data = (user != null),
                Success = true,
                Message = (user != null) ? $"{userName} {Constants.Messages.UserNameAlreadyTaken}" : null,
                StatusCode = (user != null) ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK
            };

            return Ok(response);
        }

        [HttpGet("emailTaken")] //GET: api/user/emailTaken
        [AllowAnonymous]
        public async Task<IActionResult> EmailTaken(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            var response = new ApiResponse<bool>
            {
                Data = (user != null),
                Success = true,
                Message = (user != null) ? $"{email} {Constants.Messages.EmailAlreadyRegistered}" : null,
                StatusCode = (user != null) ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK
            };

            return Ok(response);
        }

        [HttpPost("createAdditionalDetails")] //POST: api/user/createAdditionalDetails
        [ValidateTokenAndEmail]
        public async Task<IActionResult> CreateAdditionalDetails(UserDetail userDetail, string email)
        {
            return Ok(new ApiResponse<object>
            { 
                Data = await _userService.AddAdditonalDetails(userDetail, email),
                Success = true,
                Message = Constants.Messages.UserAdditionalDetails
            });
        }

        [HttpGet("getAllProfessions")] //GET: api/user/getAllProfessions
        public async Task<IActionResult> GetAllProfessions()
        {
            return Ok(new ApiResponse<object> { 
                Data = await Task.FromResult(_userService.GetProfessions()),
                Message = Constants.Messages.UserProfessions,
                Success = true
            });
        }

        [HttpGet("getUserProfile")] //GET: api/user/getUserProfile
        public async Task<IActionResult> GetUserProfile(string userName)
        {
            return Ok(new ApiResponse<UserProfileDTO>
            {
                Data = await this._userService.GetUserProfile(userName),
                Success = true,
                Message = Constants.Messages.UserProfileFetched
            });
        }
    }
}
