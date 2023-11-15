using Pocket.API.Constants;

namespace Pocket.API.DTO
{
    public class CreatePocketProfileItemDTO
    {
        public string ProfileType { get; set; } 
        public string SocialProfileUserName { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public bool IsVisibleToOthers { get; set; } = true;
    }
}
