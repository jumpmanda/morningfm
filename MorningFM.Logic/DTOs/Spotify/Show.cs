using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs.Spotify
{

    [DataContract]
    public class SavedShowBlob
    {
        [DataMember(Name ="items")]
        public ShowBlob[] Items { get; set; }
    }

    [DataContract]
    public class ShowBlob
    {
        [DataMember(Name= "added_at")]
        public DateTime AddedAt { get; set; }

        [DataMember(Name ="show")]
        public Show Show { get; set; }
    }

    [DataContract]
    public class Show
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name="images")]
        public ImageSource[] Images { get; set; }

    }
}
