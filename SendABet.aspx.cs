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
    public partial class SendABet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // TODO: Future authentication system
                // This section will be updated when login functionality is implemented
                // Currently running with manual sender selection

                btnSendBet.Click += new EventHandler(btnSendBet_Click);
            }
        }

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text) ||
                    string.IsNullOrEmpty(txtBetA_Amount.Text) ||
                    string.IsNullOrEmpty(txtBetB_Amount.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                try
                {
                    // Get sender user ID from username input
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID from username input
                    long recipientUserID = await GetUserIDByUsernameAsync(txtRecipientUsername.Text);
                    if (recipientUserID == -1)
                    {
                        lblStatus.Text = "Invalid recipient. Please enter a valid username.";
                        return;
                    }

                    // Create a new bet object
                    var bet = new Bet
                    {
                        UserID_Sender = senderUserID,    // Use the retrieved sender ID
                        UserID_Receiver = recipientUserID,
                        BetA_Amount = Convert.ToDouble(txtBetA_Amount.Text),
                        BetB_Amount = Convert.ToDouble(txtBetB_Amount.Text),
                        Pending_Bet = Convert.ToDouble(txtBetA_Amount.Text), // Pending_Bet starts as Bet A amount
                        Description = txtDescription.Text,
                        Status = "Pending",
                        Sender_Result = txtSenderResult.Text,
                        Receiver_Result = txtReceiverResult.Text,
                        Sender_Balance_Change = 0, // Starts at 0
                        Receiver_Balance_Change = 0, // Starts at 0
                        UserID_Mediator = chkNeedMediator.Checked ? (long.TryParse(txtMediatorID.Text, out long mediatorId) ? mediatorId : 0) : 0, // Handle mediator ID properly
                        UpdatedAt = DateTime.Now,
                        Created_at = DateTime.Now,
                    };

                    // Log the bet object for debugging
                    Console.WriteLine($"Bet Object: Sender={bet.UserID_Sender}, Receiver={bet.UserID_Receiver}, BetA={bet.BetA_Amount}, BetB={bet.BetB_Amount}, Pending={bet.Pending_Bet}, Mediator={bet.UserID_Mediator}");

                    // Use a BetsController to save the bet
                    using (var httpClient = new HttpClient())
                    {
                        var betsController = new BetsController(httpClient);
                        var result = await betsController.AddBetAsync(bet);

                        // Check the result type
                        if (result is OkObjectResult okResult)  //a nathan marzina production
                        {
                            lblStatus.Text = "Bet sent successfully!";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (result is BadRequestObjectResult badRequestResult)
                        {
                            lblStatus.Text = $"Error sending bet: {badRequestResult.Value}";
                        }
                        else
                        {
                            lblStatus.Text = "Error sending bet. Please try again.";
                        }
                    }

                    // Clear the form
                    txtSenderUsername.Text = "";  // Clear the sender field
                    txtRecipientUsername.Text = "";
                    txtBetA_Amount.Text = "";
                    txtBetB_Amount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";
                    chkNeedMediator.Checked = false;
                    txtMediatorID.Text = "";

                    // a Nathan Marzina production
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
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

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void chkNeedMediator_CheckedChanged(object sender, EventArgs e)
        {
            //double clicked now we r stuck with this bullshit
        }
    }
}