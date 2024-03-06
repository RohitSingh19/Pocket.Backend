using MongoDB.Bson.Serialization.Attributes;

namespace Pocket.API.Models
{
    public class UserDetail
    {
        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("bio")]
        public string Bio { get; set; }

        [BsonElement("profession")]
        public string Profession { get; set; }
        
        [BsonElement("category")]
        public string Category { get; set; }
        
        [BsonElement("profilePictureUrl")]
        public string ProfilePictureUrl { get; set; }
        
        [BsonElement("profileTheme")]
        public string ProfileTheme { get; set; }   
    }
}
