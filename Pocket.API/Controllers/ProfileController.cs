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
    public class ProfileController : ControllerBase
    {
        private readonly IPocketService _pocketService;
        public ProfileController(IPocketService pocketService)
        {
            _pocketService = pocketService;
        }

        [HttpPost("addProfile")]
        public async Task<ActionResult<bool>> AddProfile(CreatePocketProfileItemDTO createPocketProfile)
        {
            await _pocketService.AddProfile(createPocketProfile);
            return Ok();
        }
    }
}
