using Pocket.API.Models;

namespace Pocket.API.DTO
{
    public class SocialProfilesDTO
    {
        public string Category { get; set; }
        public List<SocialProfile> Profiles { get; set; }
    }
}
