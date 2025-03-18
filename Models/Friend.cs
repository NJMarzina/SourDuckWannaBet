using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Friend
    {
        public Bet FriendID { get; set; }
        public long UserID_1 { get; set; }
        public long UserID_2 { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }  // timestamp corresponds to DateTime in C#
        public DateTime AcceptDate {  get; set; }
    }
}
