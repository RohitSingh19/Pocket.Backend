using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_Backend.DTO;
using Pocket.API.Models;
using Pocket.API.Services;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pocket.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Register(UserRegisterDTO userRegisteredDTO)
        {
            if (await _userService.HasUserAlreadyRegistered(userRegisteredDTO.Email))
                return BadRequest("User Already Registered");

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Email = userRegisteredDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisteredDTO.Password)),
                PasswordSalt = hmac.Key
            };

            await _userService.AddUser(user);

            return Ok(CreateUserResponseWithToken(user));
        }

        [HttpPost("login")] //POST: api/auth/login
        public async Task<ActionResult<User>> Login(LoginDTO loginDTO)
        {
            var user = await _userService.GetUserByEmail(loginDTO.Email);   

            if (user == null) return Unauthorized("email not found");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Password incorrect");

            return Ok(CreateUserResponseWithToken(user));
        }

        private UserResponseDTO CreateUserResponseWithToken(User user)
        {
            return new UserResponseDTO() { Email = user.Email, Token = _tokenService.CreateToken(user) };
        }
    }
}
