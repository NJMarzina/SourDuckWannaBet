﻿using System;
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

                var user = await _usersController.GetUserByUserIDAsync(long.Parse(Request.Cookies["UserID"].Value));

                //lblUsername.Text = Request.Cookies["Username"].Value;
                //lblUsername2.Text = Request.Cookies["Username"].Value;
                //lblBalance.Text = Request.Cookies["Balance"].Value;

                lblUsername.Text = user.Username;
                lblUsername2.Text = user.Username;
                lblBalance.Text = user.Balance.ToString();

                await LoadAcceptedBetsAsync();
                await LoadPendingBetsAsync();
            }
        }

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            // TODO: Implement Send A Bet functionality
            Response.Redirect("WBSendABet.aspx");
        }

        protected void btnAddFriend_Click(object sender, EventArgs e)
        {
            // TODO: Implement Add A Friend functionality
            Response.Redirect("WBAddAFriend.aspx");
        }

        private async Task LoadAcceptedBetsAsync()
        {
            var bets = await _betsController.GetAllBetsAsync();
            var acceptedBets = new List<Bet>();

            foreach (var bet in bets)
            {
                if (bet.Status == "Accepted")
                {
                    if (bet.UserID_Sender == long.Parse(Request.Cookies["UserID"].Value) ||
                        bet.UserID_Receiver == long.Parse(Request.Cookies["UserID"].Value))
                    {
                        acceptedBets.Add(bet);
                    }
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

                // Get sender and receiver results
                Label lblSenderResult = (Label)e.Item.FindControl("lblSenderResult");
                Label lblReceiverResult = (Label)e.Item.FindControl("lblReceiverResult");

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

                    // Set the sender and receiver results
                    //lblSenderResult.Text = bet.Sender_Result ?? "No result yet"; // Display default message if no result
                    //lblReceiverResult.Text = bet.Receiver_Result ?? "No result yet"; // Display default message if no result
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

        private async Task LoadPendingBetsAsync()
        {
            var bets = await _betsController.GetAllBetsAsync();
            var pendingBets = new List<Bet>();

            foreach (var bet in bets)
            {
                if (bet.Status == "Pending" && bet.UserID_Receiver == long.Parse(Request.Cookies["UserID"].Value))
                {
                    pendingBets.Add(bet);
                }
            }

            // Bind the filtered bets to the repeater
            rptPendingBets.DataSource = pendingBets;
            rptPendingBets.DataBind();
        }

        protected async void rptPendingBets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var bet = (Bet)e.Item.DataItem;

                // Get labels
                Label lblSenderUsername = (Label)e.Item.FindControl("lblSenderUsername");

                // Get buttons
                Button btnAccept = (Button)e.Item.FindControl("btnAccept");
                Button btnDecline = (Button)e.Item.FindControl("btnDecline");
                Button btnModify = (Button)e.Item.FindControl("btnModify");

                // Get usernames
                var senderUser = await _usersController.GetUserByUserIDAsync(bet.UserID_Sender);
                var receiverUser = await _usersController.GetUserByUserIDAsync(bet.UserID_Receiver);

                Label lblPendingSides = (Label)e.Item.FindControl("lblPendingSides");
                lblPendingSides.Text = "Their proposition: " + bet.Sender_Result.ToString() + "<br /> Your expected proposition: " + bet.Receiver_Result.ToString();

                if (senderUser != null && receiverUser != null)
                {
                    // Set usernames
                    lblSenderUsername.Text = "From " + senderUser.Username;
                }
            }
        }


        protected async void BetResponse_Command(object sender, CommandEventArgs e)
        {
            long betId = Convert.ToInt64(e.CommandArgument);
            string response = e.CommandName;

            // Store the bet ID in session
            Session["BetID"] = betId;

            if (response == "Modify")
            {
                // Ensure any necessary async operations are completed before redirecting
                await LoadPendingBetsAsync();  // Or any other async method that should finish first

                Response.Cookies["BetID"].Value = betId.ToString(); // Store the bet ID in session

                Response.Redirect("WBModifyBet.aspx");
                //Context.ApplicationInstance.CompleteRequest();
            }
            else if (response == "Accept")
            {
                // Update bet status to Accepted
                var betsController = new BetsController(new HttpClient());
                Bet _bet = await betsController.GetBetByIdAsync(betId);

                var usersController = new UsersController(new HttpClient());
                User _user = await usersController.GetUserByUserIDAsync(long.Parse(Request.Cookies["UserID"].Value));

                _bet.Pending_Bet += _bet.BetB_Amount;
                _bet.Status = "Accepted";

                _user.Balance -= _bet.BetB_Amount;

                // TODO
                // add to total bets in users table ??

                await betsController.UpdateBetAsync(_bet);
                await usersController.UpdateUserAsync(_user);
            }
            else if (response == "Decline")
            {
                // Update bet status to Declined
                var betsController = new BetsController(new HttpClient());
                Bet _bet = await betsController.GetBetByIdAsync(betId);

                var usersController = new UsersController(new HttpClient());
                User _user = await usersController.GetUserByUserIDAsync(long.Parse(_bet.UserID_Sender.ToString()));

                _user.Balance += _bet.BetA_Amount;

                _bet.Pending_Bet = 0;
                _bet.Status = "Declined";

                // TODO
                // remove 1 from total bets in users table ??

                await betsController.UpdateBetAsync(_bet);
                await usersController.UpdateUserAsync(_user);
            }

            // Reload the bets
            await LoadPendingBetsAsync();
        }

        /*
        protected async void BetResponse_Command(object sender, CommandEventArgs e)
        {
            long betId = Convert.ToInt64(e.CommandArgument);
            string response = e.CommandName;

            if (response == "Accept")
            {
                // Update bet status to Accepted
                await _betsController.UpdateBetStatusAsync(betId, "Accepted");
            }
            else if (response == "Decline")
            {
                // Update bet status to Declined
                await _betsController.UpdateBetStatusAsync(betId, "Declined");
            }
            else if (response == "Modify")
            {
                Response.Redirect($"WBModifyBet.aspx?betID={betId}", false);
                Context.ApplicationInstance.CompleteRequest(); // Ensure that the response is completed
            }

            // Reload the bets
            await LoadPendingBetsAsync();
        }
        */
    }
}

//wbaddafriend
//wbdashboard
//wbforgotpassword
//wblogout
//wbmodifybet
//wbmybets
//wbprofile
//wbregistration
//wbsendabet