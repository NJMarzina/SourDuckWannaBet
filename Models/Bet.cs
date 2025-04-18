﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bet
    {
        public long BetID { get; set; }  // Primary key, auto-generated by the database
        public long UserID_Sender { get; set; }
        public long UserID_Receiver { get; set; }
        public DateTime? Created_at { get; set; }  // Auto-generated by the database
        public double BetA_Amount { get; set; }
        public double BetB_Amount { get; set; }
        public double Pending_Bet { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Sender_Result { get; set; }
        public string Receiver_Result { get; set; }
        public double Sender_Balance_Change { get; set; }
        public double Receiver_Balance_Change { get; set; }
        public long? UserID_Mediator { get; set; }  // Nullable
        public DateTime? UpdatedAt { get; set; }  // Nullable
    }
}

/*// Given the following Model representing the Supabase Database (Message.cs)
[Table("messages")]
public class Message : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("username")]
    public string UserName { get; set; }

    [Column("channel_id")]
    public int ChannelId { get; set; }

    public override bool Equals(object obj)
    {
        return obj is Message message &&
                Id == message.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}

void Initialize()
{
    // Get All Messages
    var response = await client.Table<Message>().Get();
    List<Message> models = response.Models;

    // Insert
    var newMessage = new Message { UserName = "acupofjose", ChannelId = 1 };
    await client.Table<Message>().Insert();

    // Update
    var model = response.Models.First();
    model.UserName = "elrhomariyounes";
    await model.Update();

    // Delete
    await response.Models.Last().Delete();

    // etc.
}
*/

