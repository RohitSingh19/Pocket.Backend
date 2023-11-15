using Pocket.API.DTO;
using Pocket.API.Models;

namespace Pocket.API.Services
{
    public interface IPocketService
    {
        Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO, string userName);
        Task<IEnumerable<SocialProfile>> GetSocialProfiles();
        Task<SocialProfile> GetSocialProfile(string type);
        Task<PocketProfile> GetPocketProfile(string username);

    }
}
