using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs.Spotify
{
    [DataContract]
    public class PlaylistRequest
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name = "public")]
        public bool PublicMode { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }


    [DataContract]
    public class Playlist
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "public")]
        public bool PublicMode { get; set; }

        //TODO: Requires some finessing - could be either track or episode
        //[DataMember(Name = "public")]
        //public Track[] Tracks { get; set; }
    }
}
