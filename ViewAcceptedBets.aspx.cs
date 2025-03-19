using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class ViewAcceptedBets : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadAcceptedBetsAsync();
            }
        }

        private async Task LoadAcceptedBetsAsync()
        {
            try
            {
                // Hardcoded userID for demonstration
                int userId = 1;

                // Fetch accepted bets for the user
                var betsController = new BetsController(new HttpClient());
                var acceptedBets = await betsController.GetAcceptedBetsByUserIDAsync(userId);

                // Bind the data to the repeater
                rptAcceptedBets.DataSource = acceptedBets;
                rptAcceptedBets.DataBind();
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., display a message)
                Response.Write($"Error loading bets: {ex.Message}");
            }
        }

        protected async void rptAcceptedBets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectWinner")
            {
                // Parse the command argument
                string[] args = e.CommandArgument.ToString().Split('|');
                long betId = long.Parse(args[0]);
                long winnerId = long.Parse(args[1]);

                // Update the bet with the winner
                await UpdateBetWinnerAsync(betId, winnerId);

                // Reload the bets
                await LoadAcceptedBetsAsync();
            }
        }

        private async Task UpdateBetWinnerAsync(long betId, long winnerId)
        {
            try
            {
                var betsController = new BetsController(new HttpClient());

                // Fetch all bets
                var bets = await betsController.GetAllBetsAsync();
                var bet = bets.FirstOrDefault(b => b.BetID == betId);

                var usersController = new UsersController(new HttpClient());

                //get user by winnerId
                var winner = await usersController.GetUserByUserIDAsync(winnerId);

                var loser = await usersController.GetUserByUserIDAsync((winnerId == bet.UserID_Sender) ? bet.UserID_Receiver : bet.UserID_Sender);

                if (bet != null)
                {
                    // Update the bet with the winner
                    bet.Sender_Result = (winnerId == bet.UserID_Sender) ? "Win" : "Lose";
                    bet.Receiver_Result = (winnerId == bet.UserID_Receiver) ? "Win" : "Lose";
                    bet.Status = "Completed";
                    bet.Sender_Balance_Change = (winnerId == bet.UserID_Sender) ? bet.Pending_Bet : -bet.Pending_Bet;
                    bet.Receiver_Balance_Change = (winnerId == bet.UserID_Receiver) ? bet.Pending_Bet : -bet.Pending_Bet;

                    // Save the updated bet
                    await betsController.UpdateBetAsync(bet);

                    winner.Balance += double.Parse(bet.Pending_Bet.ToString());
                    winner.NumWins += 1;

                    await usersController.UpdateUserAsync(winner);

                    loser.NumLoses += 1;
                    await usersController.UpdateUserAsync(loser);   // a nathan marzina production

                    //TODO create the transaction here.
                }
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., display a message)
                Response.Write($"Error updating bet: {ex.Message}");
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            // Redirect to the index page
            Response.Redirect("Index.aspx");
        }
    }
}

/*
 * // Fetch sender and receiver usernames
            string senderUsername = await usersController.GetUserNameByUserIDAsync(bet.UserID_Sender);
            string receiverUsername = await usersController.GetUserNameByUserIDAsync(bet.UserID_Receiver);

            // Create BetWithUsernames object
            var betWithUsernames = new BetWithUsernames
            {
                Bet = bet,
                UserName_Sender = senderUsername ?? "Unknown Sender",
                UserName_Receiver = receiverUsername ?? "Unknown Receiver"
            };
*/