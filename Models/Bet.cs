using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bet
    {
        public long BetID { get; set; }
        public long UserID_Sender { get; set; }
        public long UserID_Receiver { get; set; }
        public DateTime Created_at { get; set; }
        public double BetA_Amount { get; set; }
        public double BetB_Amount { get; set; }
        public double Pending_Bet { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Sender_Result { get; set; }
        public string Receiver_Result { get; set; }
        public double Sender_Balance_Change { get; set; }
        public double Receiver_Balance_Change { get; set; }
        public long UserID_Mediator { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

