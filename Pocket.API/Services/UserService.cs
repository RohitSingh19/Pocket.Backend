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

        public async Task<User> GetUserByEmail(string Email)
        {
           return await _usersCollection.Find<User>(x=> x.Email == Email).FirstOrDefaultAsync();
        }

        public async Task<bool> HasUserAlreadyRegistered(string Email)
        {
            try
            {
                if(string.IsNullOrEmpty(Email)) throw new ArgumentNullException("Email can not be null");

                return (await _usersCollection.Find(x => x.Email == Email).AnyAsync());
               
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
