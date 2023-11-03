using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pocket.API.Models
{
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("typeId")]
        public int TypeId { get; set; }
        
        [BsonElement("typeKey")]
        public string TypeKey { get; set; }
        
        [BsonElement("baseUrl")]
        public string BaseUrl { get; set; } 
    }
}
