using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs.Spotify
{
    [DataContract]
    public class Episode
    {
        [DataMember(Name ="id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name ="release_date")]
        public string ReleaseDate { get; set; }
    }

    [DataContract]
    public class EpisodeBlob
    {
        [DataMember(Name = "items")]
        public Episode[] Items { get; set; }
    }

}
