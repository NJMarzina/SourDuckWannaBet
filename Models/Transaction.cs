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
        public int UserID { get; set; }       // Foreign key to Users table
        public int BetID { get; set; }        // Foreign key to Bets table
        public decimal Amount { get; set; }   // Amount involved in the transaction
        public string TransactionType { get; set; } // Type of transaction (e.g., "bet", "win", "loss")
        public DateTime TransactionDate { get; set; } // Timestamp of the transaction

        // Constructor for initializing a new transaction
        public Transaction(int userID, int betID, decimal amount, string transactionType, DateTime transactionDate)
        {
            UserID = userID;
            BetID = betID;
            Amount = amount;
            TransactionType = transactionType;
            TransactionDate = transactionDate;
        }

        // Default constructor
        public Transaction() { }
    }
}
