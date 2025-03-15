using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bet
    {
        public long? BetID { get; set; }  // Should match "bet_id"
        public long? UserID_Sender { get; set; }  // Should match "user_id_sender"
        public long? UserID_Receiver { get; set; }  // Should match "user_id_receiver"
        public DateTime? Created_at { get; set; }  // Should match "created_at"
        public double? BetA_Amount { get; set; }  // Should match "bet_a_amount"
        public double? BetB_Amount { get; set; }  // Should match "bet_b_amount"
        public double? Pending_Bet { get; set; }  // Should match "pending_bet"
        public string Description { get; set; }  // Should match "description"
        public string Status { get; set; }  // Should match "status"
        public string Sender_Result { get; set; }  // Should match "sender_result"
        public string Receiver_Result { get; set; }  // Should match "receiver_result"
        public double? Sender_Balance_Change { get; set; }  // Should match "sender_balance_change"
        public double? Receiver_Balance_Change { get; set; }  // Should match "receiver_balance_change"
        public long? UserID_Mediator { get; set; }  // Should match "user_id_mediator"
        public DateTime? UpdatedAt { get; set; }  // Should match "updated_at"
    }
}

