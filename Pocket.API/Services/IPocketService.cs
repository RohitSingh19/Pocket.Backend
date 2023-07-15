using Pocket.API.DTO;

namespace Pocket.API.Services
{
    public interface IPocketService
    {
        Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO);
    }
}
