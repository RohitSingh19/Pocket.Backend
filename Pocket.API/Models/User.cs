using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Pocket.API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public byte[] PasswordHash { get; set; }

        [BsonElement("passwordSalt")]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }
        
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
        
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [BsonElement("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }
    }
}
