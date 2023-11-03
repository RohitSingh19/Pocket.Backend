using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pocket.API.Models
{
    public class PocketProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }
        
        [BsonElement("userProfiles")]
        public UserProfile[] Profiles { get; set; }  = new UserProfile[0];
    }
}
