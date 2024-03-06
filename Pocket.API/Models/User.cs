using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using Pocket.API.Constants;

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

        [BsonElement("stage")]
        public UserProfileStages Stage { get; set; } = UserProfileStages.LoginStage;

        [BsonElement("additionalDetails")]
        public UserDetail AdditionalDetails { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("lastActiveAt")]
        public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;
    }
}
