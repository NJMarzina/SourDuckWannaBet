using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class User
    {
        public long UserID { get; set; }  // int8 corresponds to long in C#
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }  // numeric corresponds to long in C#
        public double Balance { get; set; }  // float8 corresponds to double in C#
        public long NumWins { get; set; }  // int8 corresponds to long in C#
        public long NumLoses { get; set; }  // int8 corresponds to long in C#
        public long NumBets { get; set; }  // int8 corresponds to long in C#
        public DateTime CreatedAt { get; set; }  // timestamp corresponds to DateTime in C#
        public string UserType { get; set; }
        public string Subscription { get; set; }
    }

}