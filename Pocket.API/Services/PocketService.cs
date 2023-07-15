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
        public PocketService(IOptions<PocketDatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _pocketProfileCollection = database.GetCollection<PocketProfile>(databaseSettings.Value.PocketProfileCollectionName);
        }
        public async Task<bool> AddProfile(CreatePocketProfileItemDTO profileItemDTO)
        {
            var filter = Builders<PocketProfile>.Filter.Eq(user => user.UserId, profileItemDTO.UserId);
            var profile = new Profile() { ProfileUserName = profileItemDTO.ProfileUserName, IsVisibleToOthers = true, LastUpdated = DateTime.UtcNow };
            var update = Builders<PocketProfile>.Update.Push(user => user.Profiles, profile);
            await _pocketProfileCollection.UpdateOneAsync(filter, update);
            return true;
        }
    }
}
