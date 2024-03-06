using Pocket.API.Constants;

namespace Pocket.API.DTO
{
    public class UserResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public UserProfileStages Stage { get; set; }

    }
}
