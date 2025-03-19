using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Friend
    {
        public int FriendID { get; set; } // Changed from Bet to int
        public long UserID_1 { get; set; }
        public long UserID_2 { get; set; }
        public string Status { get; set; } // "pending", "accepted", "rejected"
        public DateTime CreatedAt { get; set; }
        public DateTime? AcceptDate { get; set; } // Made nullable for pending requests
    }
}
