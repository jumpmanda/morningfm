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

        [DataMember(Name = "release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [DataMember(Name="resume_point")]
        public EpisodeResumeMetadata ResumeMetadata { get; set; }
    }

    [DataContract]
    public class EpisodeBlob
    {
        [DataMember(Name = "items")]
        public Episode[] Items { get; set; }
    }

    [DataContract]
    public class EpisodeResumeMetadata
    {
        [DataMember(Name ="fully_played")]
        public bool IsFullyPlayed { get; set; }
    }

}
