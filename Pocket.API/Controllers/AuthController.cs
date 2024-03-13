using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocket.API.DTO;
using Pocket.API.Handlers;
using Pocket.API.Models;
using Pocket.API.Services;
using System.Net;
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
            if(await IsUserRegisterd(userRegisteredDTO))
            {
                return Ok(new ApiResponse<UserResponseDTO>
                {
                    Success = false,
                    Message = Constants.Messages.UserAlreadyRegistered,
                    Data = null,
                    StatusCode = (int) HttpStatusCode.BadRequest
                });
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Email = userRegisteredDTO.Email,
                UserName = userRegisteredDTO.UserName,
                Stage = Constants.UserProfileStages.AdditionalDetailsStage,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisteredDTO.Password)),
                PasswordSalt = hmac.Key
            };

            await _userService.AddUser(user);

            return Ok(new ApiResponse<UserResponseDTO>
            {
                Success = true,
                Message = Constants.Messages.UserRegistered,
                Data = CreateUserResponseWithToken(user),
                StatusCode = (int)HttpStatusCode.Created
            });
        }

        [HttpPost("login")] //POST: api/auth/login
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userService.GetUserByEmail(loginDTO.Email);

            /*if the user enetered email is not in database*/
            if (user == null) {
                return Ok(new ApiResponse<UserResponseDTO> {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = Constants.Messages.EmailNotFound
                });
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Ok(new ApiResponse<UserResponseDTO>
                    {
                        Success = false,
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = Constants.Messages.PasswordIncorrect
                    });
                }
            }

            return Ok(new ApiResponse<UserResponseDTO>
            {
                Success = true,
                Message = Constants.Messages.UserRegistered,
                Data = CreateUserResponseWithToken(user)
            });
        }


        private async Task<bool> IsUserRegisterd(UserRegisterDTO userRegisteredDTO)
        {
            return (await _userService.HasUserAlreadyRegistered(userRegisteredDTO.Email, userRegisteredDTO.UserName));
        }

        private UserResponseDTO CreateUserResponseWithToken(User user)
        {
            return new UserResponseDTO() { Email = user.Email, UserName = user.UserName, Stage = user.Stage, Token = _tokenService.CreateToken(user) };
        }
    }
}
