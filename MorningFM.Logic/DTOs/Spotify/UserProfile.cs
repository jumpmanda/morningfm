using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs.Spotify
{
    [DataContract]
    public class Followers
    {
        [DataMember(Name ="total")]
        public int Total { get; set; }
    }

    [DataContract]
    public class UserProfile
    {
        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Email address entered when user created account; Note: email is unverified, do not assume email belongs to user. 
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "followers")]
        public Followers TotalFollowers { get; set; }

        [DataMember(Name = "images")]
        public ImageSource[] Images { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

    }
}
