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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
    }
}
