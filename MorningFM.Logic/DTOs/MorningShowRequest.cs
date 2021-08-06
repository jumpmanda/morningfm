using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq; 

namespace MorningFM.Logic.DTOs
{
    [DataContract]
    public class MorningShowRequest
    {
        [DataMember(Name ="showIds")]
        public string[] ShowIds { get; set; }

        public override string ToString() {
            return $"{nameof(MorningShowRequest)} [ {ShowIds?.Aggregate((a, b) => a + ',' + b)} ]"; 
        }
    }
}
