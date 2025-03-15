using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class User
    {
        public long? UserID { get; set; }  // Nullable
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long? PhoneNumber { get; set; }  // Nullable
        public double? Balance { get; set; }  // Nullable
        public long? NumWins { get; set; }  // Nullable
        public long? NumLoses { get; set; }  // Nullable
        public long? NumBets { get; set; }  // Nullable
        public DateTime? CreatedAt { get; set; }  // Nullable
        public string UserType { get; set; }
        public string Subscription { get; set; }
    }

}