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
    public class Session
    {
        [DataMember(Name = "id")]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [DataMember(Name ="token")]
        public Guid Token { get; set; }

        [DataMember(Name = "spotifyAccess")]
        public SpotifyAccessBlob spotifyAccess { get; set; }
    }
}
