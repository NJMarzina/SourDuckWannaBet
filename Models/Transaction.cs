using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        public int TransactionID { get; set; } // Primary key
        public int BetID { get; set; }        // Foreign key to Bets table
        public decimal Amount { get; set; }   // Amount involved in the transaction
        public string TransactionType { get; set; } // Type of transaction (e.g., "bet", "win", "loss", "refund")
        public DateTime TransactionDate { get; set; } // Timestamp of the transaction
        public int SenderID { get; set; }     // Sender's UserID
        public int ReceiverID { get; set; }   // Receiver's UserID
        public string Status { get; set; }    // Status of the transaction (e.g., "pending", "completed", "refunded")

        // Constructor for initializing a new transaction
        public Transaction(int betID, decimal amount, string transactionType, DateTime transactionDate, int senderID, int receiverID, string status)
        {
            BetID = betID;
            Amount = amount;
            TransactionType = transactionType;
            TransactionDate = transactionDate;
            SenderID = senderID;
            ReceiverID = receiverID;
            Status = status;
        }

        // Default constructor
        public Transaction() { }
    }
}
