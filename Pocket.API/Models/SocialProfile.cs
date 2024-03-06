using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pocket.API.Models
{
    public class SocialProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        
        [BsonElement("category")]
        public string Category { get; set; }
        
        [BsonElement("baseUrl")]
        public string BaseUrl { get; set; }

        [BsonElement("searchKey")]
        public string SearchKey { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
