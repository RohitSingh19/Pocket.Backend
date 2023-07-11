using Pocket.API.Models;
using System.Threading.Tasks;

namespace Pocket.API.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
    }
}
