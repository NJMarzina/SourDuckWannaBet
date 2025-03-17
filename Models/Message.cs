using System;

namespace Models
{
    public class Message
    {
        public int MessageID { get; set; } // Primary key
        public int UserID { get; set; } // Hardcoded to 1 for now
        public string Header { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } // Automatically set to NOW
    }
}