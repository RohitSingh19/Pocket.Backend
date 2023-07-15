using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pocket.API.Data;
using Pocket.API.DTO;
using Pocket.API.Models;
using System.Threading.Tasks;

namespace Pocket.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<PocketProfile> _pocketProfileCollection;
        public UserService(IOptions<PocketDatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
            _pocketProfileCollection = database.GetCollection<PocketProfile>(databaseSettings.Value.PocketProfileCollectionName);
        }
        public async Task<bool> AddUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
            return true;
        }
        public async Task<bool> CreateUserName(PocketProfile pocketProfile)
        {
            await _pocketProfileCollection.InsertOneAsync(pocketProfile);
            return true;
        }
        public async Task<User> GetUserByEmail(string Email)
        {
           return await _usersCollection.Find<User>(x=> x.Email == Email).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserById(string Id)
        {
            return await _usersCollection.Find<User>(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByUsername(string UserName)
        {
            return await _usersCollection.Find<User>(x => x.UserName == UserName).FirstOrDefaultAsync();
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
        public async Task<bool> UpdateUserNameInUsers(string userName, string userId)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
            var update = Builders<User>.Update.Set(user => user.UserName, userName);
            await _usersCollection.UpdateOneAsync(filter, update); 
            return true;
        }
    }
}
