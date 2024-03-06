using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Pocket.API.Data;
using Pocket.API.DTO;
using Pocket.API.Models;

namespace Pocket.API.Services
{
    public class PocketService : IPocketService
    {
        private readonly IMongoCollection<PocketProfile> _pocketProfileCollection;
        private readonly IMongoCollection<SocialProfile> _socialProfileCollection;
        private readonly IMongoCollection<User> _userCollection;
        public PocketService(IOptions<PocketDatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _pocketProfileCollection = database.GetCollection<PocketProfile>(databaseSettings.Value.PocketProfileCollectionName);
            _socialProfileCollection = database.GetCollection<SocialProfile>(databaseSettings.Value.SocialProfileCollectionName);
            _userCollection = database.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
        }

        public async Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO, string userName)
        {
            var userProfile = await GetUserProfile(userName);

            if (userProfile == null)
            {
                throw new Exception($"No user profile found for {userName}");
            }

            var socialProfile = await GetSocialProfileById(profileItemDTO.ProfileId);

            if (socialProfile == null)
            {
                throw new Exception("Invalid profile type");
            }

            var filter = Builders<PocketProfile>.Filter.Eq(user => user.UserName, userName);

            var pocketProfile = new UserProfile()
            {
                ProfileUserName = profileItemDTO.UserName,
                Name = socialProfile.Name,
                BaseUrl = socialProfile.BaseUrl,
                Category = socialProfile.Category,
            };

            var update = Builders<PocketProfile>.Update.Push(user => user.Profiles, pocketProfile);

            var result = await _pocketProfileCollection.UpdateOneAsync(filter, update);

            return (result.ModifiedCount > 0);
        }

        public async Task<PocketProfile> GetPocketProfile(string username)
        {
            var pocketProfiles = await _pocketProfileCollection.Find(p => p.UserName == username).FirstOrDefaultAsync();
            
            var profiles = pocketProfiles.Profiles.ToList().OrderByDescending(x => x.lastModifiedAt);
            pocketProfiles.Profiles = profiles.ToArray();

            return pocketProfiles;
        }

        public async Task<SocialProfile> GetSocialProfile(string type)
        {
            return await _socialProfileCollection.Find(pr => pr.Name.ToLower() == type).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserProfile(string userName)
        {
            return await _userCollection.Find(t => t.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SocialProfilesDTO>> GetSocialProfiles()
        {
            List<SocialProfile> socialProfiles = await _socialProfileCollection.Find(Builders<SocialProfile>.Filter.Empty).ToListAsync();

            return socialProfiles.GroupBy(x => x.Category)
            .Select(profile => new SocialProfilesDTO
            {
                Category = profile.Key,
                Profiles = profile.ToList()
            }).ToList();
        }

        public async Task<SocialProfile> GetSocialProfileById(string id)
        {
            return await _socialProfileCollection.Find(pr => pr.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePocketProfile(string userName, PocketProfileUpdateDTO pocketProfileUpdateDTO)
        {
            var pocketProfile = await GetPocketProfile(userName);

            if(pocketProfile == null)
            {
                //handle this part individually
            }
            var filter = Builders<PocketProfile>.Filter.Where(profile => profile.UserName == userName &&
                                                profile.Profiles.Any(x=> x.Name == pocketProfileUpdateDTO.ProfileName)); 

            var updates = Builders<PocketProfile>.Update.Combine (
                Builders<PocketProfile>.Update.Set(x => x.Profiles.FirstMatchingElement().ProfileUserName, pocketProfileUpdateDTO.ProfileUserName),
                Builders<PocketProfile>.Update.Set(x => x.Profiles.FirstMatchingElement().IsVisibleToOthers, pocketProfileUpdateDTO.IsVisibleToOthers),
                Builders<PocketProfile>.Update.Set(x => x.Profiles.FirstMatchingElement().lastModifiedAt, DateTime.UtcNow)
            );
            
            var result = await _pocketProfileCollection.UpdateOneAsync(filter, updates);
            
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeletePocketProfile(string userName, string profileName)
        {
            var pocketProfile = await GetPocketProfile(userName);

            if (pocketProfile == null)
            {
                //handle this part individually
            }
            var filter = Builders<PocketProfile>.Filter.Where(profile => profile.UserName == userName &&
                                                profile.Profiles.Any(x => x.Name == profileName));

            var update = Builders<PocketProfile>.Update.PullFilter(x=> x.Profiles, builder => builder.Name == profileName);
            var result = await _pocketProfileCollection.UpdateOneAsync(filter, update);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}


