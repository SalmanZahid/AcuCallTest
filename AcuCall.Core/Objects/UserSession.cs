using System;
using System.ComponentModel.DataAnnotations;

namespace AcuCall.Core.Objects
{
    public class UserSession
    {
        [Key]
        public int SessionId { get; set; }
        public string Username { get; set; }
        public DateTime Login_DateTime { get; set; }
        public DateTime? Logout_DateTime { get; set; }
    }
}
