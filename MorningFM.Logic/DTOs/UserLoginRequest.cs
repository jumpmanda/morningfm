using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MorningFM.Logic.DTOs
{
    public enum LoginAction
    {
        LOGIN,
        SIGNUP
    }

    [DataContract]
    public class UserLoginRequest
    {
        [DataMember(Name ="email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "userName")]
        public string Username { get; set; }

        [DataMember(Name = "action")]
        public LoginAction LoginAction { get; set; }
    }
}
