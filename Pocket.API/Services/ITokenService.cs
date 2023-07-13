using Pocket.API.Models;

namespace Pocket.API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
