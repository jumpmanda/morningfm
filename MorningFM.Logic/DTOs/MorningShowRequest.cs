using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs
{
    [DataContract]
    public class MorningShowRequest
    {
        [DataMember(Name ="showIds")]
        public string[] ShowIds { get; set; }
    }
}
