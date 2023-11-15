using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pocket.API.Models
{
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("profileUserName")]
        public string ProfileUserName { get; set; }

        [BsonElement("profileType")]
        public string ProfileType { get; set; }

        [BsonElement("lastUpdated")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        
        [BsonElement("isVisibleToOthers")]
        public bool IsVisibleToOthers { get; set; } = true;

    }
}
