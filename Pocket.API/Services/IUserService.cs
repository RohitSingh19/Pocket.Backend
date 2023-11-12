using Pocket.API.DTO;
using Pocket.API.Models;
using System.Threading.Tasks;

namespace Pocket.API.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);

        Task<bool> HasUserAlreadyRegistered(string email);

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByUsername(string userName);

        Task<User> GetUserById(string id);

        Task<bool> CreateUserName(PocketProfile pocketProfile);

        Task<bool> AddUserNameForUser(string email, string userName);

        Dictionary<string, List<Profession>> GetProfessions();

        Task<bool> AddAdditonalDetails(UserDetail userDetail, string email);

        Task<UserProfileDTO> GetUserProfile(string email);
    }
}
