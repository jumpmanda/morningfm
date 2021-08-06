using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "id")]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [DataMember(Name ="email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "userName")]
        public string Username { get; set; }

        [DataMember(Name = "accessToken")]
        public string SpotifyAccessToken { get; set; }
    }
}
