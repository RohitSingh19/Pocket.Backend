using Pocket.API.DTO;
using Pocket.API.Models;

namespace Pocket.API.Services
{
    public interface IPocketService
    {
        Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO);
        Task<Profile> GetProfileByKey(string key);

        Task<PocketProfile> GetUserProfile(string userName);
    }
}
