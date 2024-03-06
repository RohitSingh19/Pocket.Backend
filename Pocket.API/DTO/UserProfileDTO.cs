using Pocket.API.Models;

namespace Pocket.API.DTO
{
    public class UserProfileDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public Constants.UserProfileStages Stage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActiveAt { get; set;}
        public UserDetail AdditionalDetails { get; set; }    
    }
}
