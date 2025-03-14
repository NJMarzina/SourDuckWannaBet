using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Mediation
    {
        public int MediationID { get; set; }      // Primary key
        public int BetID { get; set; }           // Foreign key to Bets table
        public int UserIDMediator { get; set; }   // Foreign key to Users table (mediator)
        public string MediationStatus { get; set; } // Status of the mediation (e.g., "pending", "resolved")
        public DateTime CreatedAt { get; set; }  // Timestamp of when the mediation was created
        public DateTime UpdatedAt { get; set; }  // Timestamp of when the mediation was last updated

        // Constructor for initializing a new mediation
        public Mediation(int betID, int userIDMediator, string mediationStatus, DateTime createdAt, DateTime updatedAt)
        {
            BetID = betID;
            UserIDMediator = userIDMediator;
            MediationStatus = mediationStatus;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Default constructor
        public Mediation() { }
    }
}
