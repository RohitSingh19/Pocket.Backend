using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pocket.API.Data;
using Pocket.API.Models;
using System.Threading.Tasks;

namespace Pocket.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        public UserService(IOptions<PocketDatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
        }
        public async Task<bool> AddUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
            return true;
        }
    }
}
