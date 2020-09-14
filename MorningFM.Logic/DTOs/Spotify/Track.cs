using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs.Spotify
{
    [DataContract]
    public class Artist
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "href")]
        public string Href { get; set; }
        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class Album
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "images")]
        public ImageSource[] Images { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class Track
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "album")]
        public Album Album { get; set; }
        [DataMember(Name = "artists")]
        public Artist[] Artists { get; set; }
    }

    [DataContract]
    public class TrackBlob
    {
        [DataMember(Name ="items")]
        public Track[] Items { get; set; }
    }
}
