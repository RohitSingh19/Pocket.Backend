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
            try
            {
                await _pocketProfileCollection.InsertOneAsync(pocketProfile);
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
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
        public async Task<bool> AddUserNameForUser(string email, string userName)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            
            var update = Builders<User>.Update
                .Set(u =>  u.UserName, userName)
                .Set(u=> u.Stage, Constants.UserProfileStages.AdditionalDetailsStage);

            var updatedResut = await _usersCollection.UpdateOneAsync(filter, update);
            
            return (updatedResut.ModifiedCount > 0);
        }
        public Dictionary<string, List<Profession>> GetProfessions()
        {
             return new UserProfession().GetUserProfessions();   
        }

        public async Task<bool> AddAdditonalDetails(UserDetail userDetail, string email)
        {
            var userDeailObj = new UserDetail();
            userDeailObj.FullName = userDetail.FullName;
            userDeailObj.Profession = userDetail.Profession;
            userDeailObj.Category = userDetail.Category;

            
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            var update = Builders<User>.Update
                .Set(u => u.AdditionalDetails, userDeailObj)
                .Set(u=> u.Stage, Constants.UserProfileStages.ActiveStage);

            var updatedResut = await _usersCollection.UpdateOneAsync(filter, update);

            return (updatedResut.ModifiedCount > 0);
        }

        public async Task<UserProfileDTO> GetUserProfile(string email)
        {
            var userInDb = await _usersCollection.Find<User>(x => x.Email == email).FirstOrDefaultAsync();
            UserProfileDTO profile = new UserProfileDTO();
            if (userInDb != null) { 
                profile.UserName = userInDb.UserName;
                profile.Email = email;
                profile.Stage = userInDb.Stage;
                profile.CreatedAt = userInDb.CreatedAt;
                profile.LastActiveAt = userInDb.LastActiveAt;
                profile.AddtionalDetails = userInDb.AdditionalDetails;
            }
            return profile;
        }
    }
}
