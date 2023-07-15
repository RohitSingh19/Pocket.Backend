using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocket.API.DTO;
using Pocket.API.Models;
using Pocket.API.Services;


namespace Pocket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("createUserName")]
        public async Task<ActionResult<bool>> CreateUserName(CreateUserNameDTO userNameDTO)
        {
            var user = await _userService.GetUserByUsername(userNameDTO.UserName);
            if (user != null) return BadRequest("Username is already taken");

            var userIdDb = await _userService.GetUserById(userNameDTO.Id);
            if (userIdDb.Id != userNameDTO.Id) return BadRequest("Not Authorized");

            var pocketProfile = new PocketProfile()
            {
                UserName = userNameDTO.UserName,
                CreatedAt = DateTime.UtcNow,
                UserId = userNameDTO.Id,
                LastModifiedAt = DateTime.UtcNow,
            };

            await _userService.CreateUserName(pocketProfile);
            await _userService.UpdateUserNameInUsers(userNameDTO.UserName, userNameDTO.Id);
            return Ok(true);
        }
    }
}
