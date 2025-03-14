using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Notification
    {
        public int NotificationID { get; set; } // Primary key
        public int UserID { get; set; }         // Foreign key to Users table
        public string Message { get; set; }     // Notification message
        public bool IsRead { get; set; }       // Indicates if the notification has been read
        public DateTime CreatedAt { get; set; } // Timestamp of when the notification was created

        // Constructor for initializing a new notification
        public Notification(int userID, string message, bool isRead, DateTime createdAt)
        {
            UserID = userID;
            Message = message;
            IsRead = isRead;
            CreatedAt = createdAt;
        }

        // Default constructor
        public Notification() { }
    }
}
