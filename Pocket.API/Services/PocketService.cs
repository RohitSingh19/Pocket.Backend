using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pocket.API.Data;
using Pocket.API.DTO;
using Pocket.API.Models;

namespace Pocket.API.Services
{
    public class PocketService : IPocketService
    {
        private readonly IMongoCollection<PocketProfile> _pocketProfileCollection;
        private readonly IMongoCollection<SocialProfile> _socialProfileCollection;
        public PocketService(IOptions<PocketDatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _pocketProfileCollection = database.GetCollection<PocketProfile>(databaseSettings.Value.PocketProfileCollectionName);
            _socialProfileCollection = database.GetCollection<SocialProfile>(databaseSettings.Value.SocialProfileCollectionName);
        }
       
        public async Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO, string userName)
        {
            var pocketProfile = await GetPocketProfile(userName);

            if(pocketProfile == null )
            {
                throw new Exception($"no pocket profile found for {userName}");
            }
            
            var socialProfile = await GetSocialProfile(profileItemDTO.ProfileType);

            if(socialProfile == null )
            {
                throw new Exception($"No profile type was found for profile type {profileItemDTO.ProfileType}");
            }

            var filter = Builders<PocketProfile>.Filter.Eq(user => user.UserName, userName);

            var userProfile = new UserProfile()
            {
                ProfileType = profileItemDTO.ProfileType,
                ProfileUserName = profileItemDTO.SocialProfileUserName,
                IsVisibleToOthers = profileItemDTO.IsVisibleToOthers,
                LastUpdated = profileItemDTO.LastUpdated
            };

            var update = Builders<PocketProfile>.Update.Push(user => user.Profiles, userProfile);

            var result = await _pocketProfileCollection.UpdateOneAsync(filter, update);
            return (result.ModifiedCount > 0);
        }

        public async Task<PocketProfile> GetPocketProfile(string username)
        {
            return await _pocketProfileCollection.Find<PocketProfile>(p => p.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<SocialProfile> GetSocialProfile(string type)
        {
            return await _socialProfileCollection.Find(pr => pr.Type == type).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SocialProfile>> GetSocialProfiles()
        {
            return await _socialProfileCollection.Find(Builders<SocialProfile>.Filter.Empty).ToListAsync();
        }

        public async Task<PocketProfile> GetUserProfile(string userName)
        {
            return await _pocketProfileCollection.Find(t => t.UserName == userName).FirstOrDefaultAsync();
        }
    }

}
