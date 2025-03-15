using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class ViewMyBets : System.Web.UI.Page
    {
        private BetsController _betsController;
        private const int CURRENT_USER_ID = 1; // Hardcoded for now

        public ViewMyBets()
        {
            _betsController = new BetsController(new HttpClient());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadPendingBets();
            }
        }

        private async System.Threading.Tasks.Task LoadPendingBets()
        {
            try
            {
                // Get all bets for this user
                var allUserBets = await _betsController.GetBetsByUserIDAsync(CURRENT_USER_ID);

                // Filter to show only bets where this user is the receiver
                var receivedBets = allUserBets.FindAll(b => b.UserID_Receiver == CURRENT_USER_ID);

                // Bind the bets to the GridView
                if (receivedBets != null && receivedBets.Count > 0)
                {
                    gvBets.DataSource = receivedBets;
                    gvBets.DataBind();
                    lblNoBets.Visible = false;
                }
                else
                {
                    gvBets.Visible = false;
                    lblNoBets.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Display error message
                Response.Write($"Error loading bets: {ex.Message}");
            }
        }

        protected async void gvBets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AcceptBet" || e.CommandName == "DenyBet")
            {
                // Get the BetID from the command argument
                int betId = Convert.ToInt32(e.CommandArgument);

                // Get all the user's bets
                var allUserBets = await _betsController.GetBetsByUserIDAsync(CURRENT_USER_ID);

                // Find the specific bet
                var bet = allUserBets.Find(b => b.BetID == betId && b.UserID_Receiver == CURRENT_USER_ID);

                if (bet != null)
                {
                    if (e.CommandName == "AcceptBet")
                    {
                        // Update the bet status to Accepted
                        bet.Status = "Accepted";

                        // Create a transaction for the bet amount
                        // This would deduct the bet amount from the user's balance
                        await CreateTransactionForBet(bet);
                    }
                    else // DenyBet
                    {
                        // Update the bet status to Denied
                        bet.Status = "Denied";
                    }

                    // Update the bet in the database
                    await _betsController.UpdateBetAsync(bet);

                    // Create a notification for the sender
                    await CreateNotificationForBetAction(bet, e.CommandName == "AcceptBet" ? "accepted" : "denied");

                    // Reload the bets
                    await LoadPendingBets();
                }
            }
        }

        private async System.Threading.Tasks.Task CreateTransactionForBet(Bet bet)
        {
            try
            {
                // Create a new transaction
                var transaction = new Transaction(
                    CURRENT_USER_ID,
                    (int)bet.BetID,
                    (decimal)bet.BetA_Amount, // The receiver's stake amount
                    "bet_placement",
                    DateTime.Now
                );

                // Using a TransactionsController to add the transaction
                var transactionsController = new TransactionsController(new HttpClient());
                await transactionsController.AddTransactionAsync(transaction);
            }
            catch (Exception ex)
            {
                Response.Write($"Error creating transaction: {ex.Message}");
            }
        }

        private async System.Threading.Tasks.Task CreateNotificationForBetAction(Bet bet, string action)
        {
            try
            {
                // Create a notification for the bet sender
                var notification = new Notification(
                    (int)bet.UserID_Sender,
                    $"Your bet has been {action} by user {CURRENT_USER_ID}.",
                    false,
                    DateTime.Now
                );

                // Using a NotificationsController to add the notification
                var notificationsController = new NotificationsController(new HttpClient());
                await notificationsController.AddNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                Response.Write($"Error creating notification: {ex.Message}");
            }
        }
    }
}