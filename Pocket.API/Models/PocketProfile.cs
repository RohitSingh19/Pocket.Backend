using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pocket.API.Models
{
    public class PocketProfile
    {
        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }
        
        [BsonElement("userProfiles")]
        public Profile[] Profiles { get; set; }  
    }
}
