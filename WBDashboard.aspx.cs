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
        private BetsController _betsController;
        private UsersController _usersController;

        public WBDashboard()
        {
            _betsController = new BetsController(new HttpClient());
            _usersController = new UsersController(new HttpClient());
        }
        protected async void Page_Load(object sender, EventArgs e)
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

                await LoadAcceptedBetsAsync();
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

        private async Task LoadAcceptedBetsAsync()
        {
            var bets = await _betsController.GetAllBetsAsync();
            var acceptedBets = new List<Bet>();

            foreach (var bet in bets)
            {
                if (bet.Status == "Accepted")
                {
                    acceptedBets.Add(bet);
                }
            }

            // Bind the filtered bets to the repeater
            rptAcceptedBets.DataSource = acceptedBets;
            rptAcceptedBets.DataBind();
        }

        protected async void rptAcceptedBets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var bet = (Bet)e.Item.DataItem;

                // Get labels
                Label lblSenderUsername = (Label)e.Item.FindControl("lblSenderUsername");
                Label lblReceiverUsername = (Label)e.Item.FindControl("lblReceiverUsername");

                // Get buttons
                Button btnSenderWin = (Button)e.Item.FindControl("btnSenderWin");
                Button btnReceiverWin = (Button)e.Item.FindControl("btnReceiverWin");

                // Get usernames
                var senderUser = await _usersController.GetUserByUserIDAsync(bet.UserID_Sender);
                var receiverUser = await _usersController.GetUserByUserIDAsync(bet.UserID_Receiver);

                if (senderUser != null && receiverUser != null)
                {
                    // Set usernames
                    lblSenderUsername.Text = senderUser.Username;
                    lblReceiverUsername.Text = receiverUser.Username;

                    // Set button text
                    btnSenderWin.Text = senderUser.Username + " wins";
                    btnReceiverWin.Text = receiverUser.Username + " wins";
                }
            }
        }

        protected async void BetWinner_Command(object sender, CommandEventArgs e)
        {
            long betId = Convert.ToInt64(e.CommandArgument);
            string winner = e.CommandName;

            // Determine which user won
            if (winner == "SenderWin")
            {
                // Update bet with sender as winner
                await _betsController.UpdateBetWinnerAsync(betId, "Sender");
            }
            else if (winner == "ReceiverWin")
            {
                // Update bet with receiver as winner
                await _betsController.UpdateBetWinnerAsync(betId, "Receiver");
            }

            // Reload the bets
            await LoadAcceptedBetsAsync();
        }


    }
}