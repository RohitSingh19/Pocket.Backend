using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pocket.API.Models
{
    public class UserProfile
    {
        [BsonElement("profileUserName")]
        public string ProfileUserName { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("baseUrl")]
        public string BaseUrl { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("lastModifiedAt")]
        public DateTime lastModifiedAt { get; set; } = DateTime.UtcNow;
        
        [BsonElement("isVisibleToOthers")]
        public bool IsVisibleToOthers { get; set; } = true;

    }
}
