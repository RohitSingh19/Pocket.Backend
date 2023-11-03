using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Pocket.API.Data;
using Pocket.API.DTO;
using Pocket.API.Models;

namespace Pocket.API.Services
{
    public class PocketService : IPocketService
    {
        private readonly IMongoCollection<PocketProfile> _pocketProfileCollection;
        private readonly IMongoCollection<Profile> _profileCollection;
        public PocketService(IOptions<PocketDatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _pocketProfileCollection = database.GetCollection<PocketProfile>(databaseSettings.Value.PocketProfileCollectionName);
            _profileCollection = database.GetCollection<Profile>(databaseSettings.Value.ProfileCollectionName);
        }
        public async Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO)
        {
            var profile = await GetProfileByKey(profileItemDTO.ProfileTypeKey);
            var filter = Builders<PocketProfile>.Filter.Eq(user => user.UserId, profileItemDTO.UserId);
            var userProfile = new UserProfile() { ProfileTypeId = profile.Id, ProfileUserName = profileItemDTO.ProfileUserName, IsVisibleToOthers = true, LastUpdated = DateTime.UtcNow };
            var update = Builders<PocketProfile>.Update.Push(user => user.Profiles, userProfile);
            await _pocketProfileCollection.UpdateOneAsync(filter, update);
            return true;
        }

        public async Task<Profile> GetProfileByKey(string key)
        {
            var filter = Builders<Profile>.Filter.Eq(t => t.TypeKey,key);
            var emptyFilter = Builders<Profile>.Filter.Empty;
            var allUsers = await _profileCollection.Find(emptyFilter).ToListAsync();
            return await _profileCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<PocketProfile> GetUserProfile(string userName)
        {
            var data = await _pocketProfileCollection.Find(t => t.UserName == userName).FirstOrDefaultAsync();
            return data;
        }
    }

}
