using System.Runtime.Serialization;

namespace MorningFM.Logic.DTOs.Spotify
{
    [DataContract]
    public class ImageSource
    {
        [DataMember(Name ="url")]
        public string Url { get; set; }

    }
}
