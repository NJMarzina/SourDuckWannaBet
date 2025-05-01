using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using SourDuckWannaBet.Controllers;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace SourDuckWannaBet
{
    public partial class WBSendABet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // TODO: Future authentication system
                // This section will be updated when login functionality is implemented
                // Currently running with manual sender selection

                //btnSendBet.Click += new EventHandler(btnSendBet_Click);
            }
        }
        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                var _betsController = new BetsController(new HttpClient());
                var _usersController = new UsersController(new HttpClient());

                try
                {
                    // Retrieve values from form fields
                    var senderUsername = Request.Cookies["Username"].Value; //changed from manual to "Logged in"
                    var recipientUsername = txtRecipientUsername.Text;
                    var betA_Amount = double.Parse(txtBetA_Amount.Text);
                    var betB_Amount = double.Parse(txtBetB_Amount.Text);
                    var description = txtDescription.Text;
                    var senderResult = txtSenderResult.Text;
                    var receiverResult = txtReceiverResult.Text;

                    double sender_balance = double.Parse(Request.Cookies["Balance"].Value);

                    // Get sender and recipient user IDs
                    var senderUserID = await GetUserIDByUsernameAsync(senderUsername);
                    var recipientUserID = await GetUserIDByUsernameAsync(recipientUsername);

                    //ensures the user's exist; a nathan marzina production
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    if (recipientUserID == -1)
                    {
                        lblStatus.Text = "Invalid recipient. Please enter a valid username.";
                        return;
                    }

                    if(senderUserID == recipientUserID)
                    {
                        lblStatus.Text = "You cannot send a bet to yourself.";
                        return;
                    }

                    if(betA_Amount > sender_balance)
                    {
                        lblStatus.Text = "You do not have a balance large enough to support that bet. Your balance is " + Request.Cookies["Balance"].Value;
                        return;
                    }

                    // Create a new Bet object
                    var newBet = new Bet
                    {
                        UserID_Sender = senderUserID,
                        UserID_Receiver = recipientUserID,
                        BetA_Amount = betA_Amount,
                        BetB_Amount = betB_Amount,
                        Pending_Bet = betA_Amount,
                        Description = description,
                        Status = "Pending",
                        Sender_Result = senderResult,
                        Receiver_Result = receiverResult,
                        Sender_Balance_Change = 0,
                        Receiver_Balance_Change = 0,
                        UserID_Mediator = 0,
                        UpdatedAt = DateTime.Now,
                        Created_at = DateTime.Now
                    };

                    // Call the controller to add the bet
                    var result = await _betsController.AddBetAsync(newBet);
                    Response.Write("Bet sent successfully!");

                    lblStatus.Text = "Bet sent successfully!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    //TODO add transaction

                    // Clear the form
                    txtRecipientUsername.Text = "";
                    txtBetA_Amount.Text = "";
                    txtBetB_Amount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";

                    //remove amount sent from sender's user account
                    var user = await _usersController.GetUserByUserIDAsync(senderUserID);
                    user.Balance -= betA_Amount;
                    var updateResult = await _usersController.UpdateUserAsync(user);
                }
                catch (Exception ex)
                {
                    // Display error message
                    Response.Write($"Error sending bet: {ex.Message}");
                    lblStatus.Text = $"Error sending bet " + ex.Message;
                }
            }));
        }
        private async Task<long> GetUserIDByUsernameAsync(string username)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var usersController = new UsersController(httpClient);
                    var users = await usersController.GetAllUsersAsync();
                    var user = users.FirstOrDefault(u => u.Username == username);
                    return user != null ? user.UserID : -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user ID: {ex.Message}");
                return -1;
            }
        }

        [WebMethod]
        public static long GetUserIDByUsername(string username)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var usersController = new UsersController(httpClient);
                    var users = usersController.GetAllUsersAsync().Result;
                    var user = users.FirstOrDefault(u => u.Username == username);
                    return user != null ? user.UserID : -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user ID: {ex.Message}");
                return -1;
            }
        }

        [WebMethod]
        public static List<string> GetUsernames()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var _usersController = new UsersController(httpClient);
                    var users = _usersController.GetAllUsersAsync().Result;
                    return users.Select(u => u.Username).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting usernames: {ex.Message}");
                return new List<string>();
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)   //changed from index to WBDashboard
        {
            Response.Redirect("WBDashboard.aspx");
        }

        protected void chkNeedMediator_CheckedChanged(object sender, EventArgs e)
        {
            //double clicked now we r stuck with this bullshit
        }
    }
}