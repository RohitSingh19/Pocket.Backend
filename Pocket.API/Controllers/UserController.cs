using Microsoft.AspNetCore.Mvc;
using Pocket.API.Models;
using Pocket.API.Services;
using System.Threading.Tasks;

namespace Pocket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User obj)
        { 
            await _userService.AddUser(obj);
            return Ok(obj);
        }
    }
}
