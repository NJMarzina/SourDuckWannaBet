using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class WBDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in (check for cookies)
                if (Request.Cookies["UserID"] == null)
                {
                    // Redirect to login page if not logged in
                    Response.Redirect("WBLogin.aspx");
                    return;
                }
            }
        }
        

        private async Task UpdateBetWinnerAsync(long betId, long winnerId)
        {
            try
            {
                var httpClient = new HttpClient();
                var betsController = new BetsController(httpClient);

                // Fetch all bets
                var bets = await betsController.GetAllBetsAsync();
                var bet = bets.FirstOrDefault(b => b.BetID == betId);

                if (bet == null)
                {
                    return;
                }

                var usersController = new UsersController(httpClient);

                // Get winner and loser - fixed to handle long to int conversion
                var winner = await usersController.GetUserByUserIDAsync((int)winnerId);
                var loserId = (winnerId == bet.UserID_Sender) ? bet.UserID_Receiver : bet.UserID_Sender;
                var loser = await usersController.GetUserByUserIDAsync((int)loserId);

                if (winner != null && loser != null)
                {
                    // Update the bet with the winner
                    bet.Sender_Result = (winnerId == bet.UserID_Sender) ? "Win" : "Lose";
                    bet.Receiver_Result = (winnerId == bet.UserID_Receiver) ? "Win" : "Lose";
                    bet.Status = "Completed";
                    bet.Sender_Balance_Change = (winnerId == bet.UserID_Sender) ? bet.Pending_Bet : -bet.Pending_Bet;
                    bet.Receiver_Balance_Change = (winnerId == bet.UserID_Receiver) ? bet.Pending_Bet : -bet.Pending_Bet;

                    // Save the updated bet
                    await betsController.UpdateBetAsync(bet);

                    // Update user stats
                    winner.Balance += double.Parse(bet.Pending_Bet.ToString());
                    winner.NumWins += 1;
                    await usersController.UpdateUserAsync(winner);

                    loser.NumLoses += 1;
                    await usersController.UpdateUserAsync(loser);

                    // Create transactions
                    var transactionsController = new TransactionsController(httpClient);

                    // Win transaction
                    Transaction transaction1 = new Transaction
                    {
                        BetID = Convert.ToInt32(bet.BetID),
                        Amount = Convert.ToInt32(bet.Pending_Bet),
                        TransactionType = "win",
                        TransactionDate = DateTime.Now,
                        SenderID = Convert.ToInt32(bet.UserID_Sender),
                        ReceiverID = Convert.ToInt32(bet.UserID_Receiver),
                        Status = (winnerId == bet.UserID_Sender) ? "completed_win_sender" : "completed_win_receiver"
                    };

                    // Loss transaction
                    Transaction transaction2 = new Transaction
                    {
                        BetID = Convert.ToInt32(bet.BetID),
                        Amount = Convert.ToInt32(bet.Pending_Bet) * -1,
                        TransactionType = "loss",
                        TransactionDate = DateTime.Now,
                        SenderID = Convert.ToInt32(bet.UserID_Sender),
                        ReceiverID = Convert.ToInt32(bet.UserID_Receiver),
                        Status = (winnerId != bet.UserID_Sender) ? "completed_loss_sender" : "completed_loss_receiver"
                    };

                    // Add transactions to the database
                    await transactionsController.AddTransactionAsync(transaction1);
                    await transactionsController.AddTransactionAsync(transaction2);

                    // Update session balance
                    int currentUserId = Convert.ToInt32(Session["UserID"]);
                    if (currentUserId == winner.UserID)
                    {
                        Session["Balance"] = winner.Balance;
                    }
                    else if (currentUserId == loser.UserID)
                    {
                        Session["Balance"] = loser.Balance;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating bet winner: {ex.Message}");
                throw;
            }
        }

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            // TODO: Implement Send A Bet functionality
            Response.Redirect("SendABet.aspx");
        }

        protected void btnAddFriend_Click(object sender, EventArgs e)
        {
            // TODO: Implement Add A Friend functionality
            Response.Redirect("AddAFriend.aspx");
        }
    }
}