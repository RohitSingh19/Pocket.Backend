using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Pocket.API.DTO;
using Pocket.API.Handlers;
using Pocket.API.Models;
using Pocket.API.Services;
using System.Security.Cryptography;
using System.Text;


namespace Pocket.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")] //POST: api/auth/register
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisteredDTO)
        {
            if (await _userService.HasUserAlreadyRegistered(userRegisteredDTO.Email)) 
            { 
                return BadRequest(new ApiResponse<UserResponseDTO> {
                    Success = false,
                    Message = Constants.Messages.UserAlreadyRegistered,
                });
            }
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Email = userRegisteredDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisteredDTO.Password)),
                PasswordSalt = hmac.Key
            };

            await _userService.AddUser(user);

            return Ok(new ApiResponse<UserResponseDTO>
            {
                Success = true,
                Message = Constants.Messages.UserRegistered,
                Data = CreateUserResponseWithToken(user)
            });
        }

        [HttpPost("login")] //POST: api/auth/login
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userService.GetUserByEmail(loginDTO.Email);   

            if (user == null) return Unauthorized(Constants.Messages.EmailNotFound);

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized(Constants.Messages.PasswordIncorrect);

            return Ok(new ApiResponse<UserResponseDTO>
            {
                Success = true,
                Message = Constants.Messages.UserRegistered,
                Data = CreateUserResponseWithToken(user)
            });
        }

        private UserResponseDTO CreateUserResponseWithToken(User user)
        {
            return new UserResponseDTO() { Email = user.Email, Token = _tokenService.CreateToken(user) };
        }
    }
}
