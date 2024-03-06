using Pocket.API.DTO;
using Pocket.API.Models;

namespace Pocket.API.Services
{
    public interface IPocketService
    {
        Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO, string userName);
        Task<IEnumerable<SocialProfilesDTO>> GetSocialProfiles();
        Task<SocialProfile> GetSocialProfile(string type);
        Task<SocialProfile> GetSocialProfileById(string id);
        Task<PocketProfile> GetPocketProfile(string username);
        Task<bool> UpdatePocketProfile(string userName, PocketProfileUpdateDTO pocketProfileUpdateDTO);
        Task<bool> DeletePocketProfile(string userName, string profileName);
    }
}
