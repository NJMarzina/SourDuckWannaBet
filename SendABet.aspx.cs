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

namespace SourDuckWannaBet
{
    public partial class SendABet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["CurrentUser"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                string.IsNullOrEmpty(txtDescription.Text) ||
                string.IsNullOrEmpty(txtSenderResult.Text) ||
                string.IsNullOrEmpty(txtReceiverResult.Text))
            {
                lblStatus.Text = "Please fill out all required fields.";
                return;
            }

            // Validate amounts based on bet type
            bool isOneSided = ddlBetType.SelectedValue == "one-sided";
            if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
            {
                lblStatus.Text = "Please enter your bet amount.";
                return;
            }
            else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
            {
                lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                return;
            }

            try
            {
                // Get current user from session
                var currentUser = Session["CurrentUser"] as User;

                // Get recipient user ID
                int recipientUserID;
                if (!int.TryParse(hdnRecipientUserID.Value, out recipientUserID))
                {
                    lblStatus.Text = "Invalid recipient. Please select a valid username.";
                    return;
                }

                // Create a new bet object
                var bet = new Bet
                {
                    UserID_Sender = currentUser.UserID,
                    UserID_Receiver = recipientUserID,
                    Created_at = DateTime.Now,
                    Description = txtDescription.Text,
                    Status = "Pending",
                    Sender_Result = txtSenderResult.Text,
                    Receiver_Result = txtReceiverResult.Text,
                    Sender_Balance_Change = 0, // Will be updated when bet is settled
                    Receiver_Balance_Change = 0, // Will be updated when bet is settled
                    UserID_Mediator = chkNeedMediator.Checked ? 0 : -1 // 0 means mediator needed but not assigned, -1 means no mediator
                };

                // Set amounts based on bet type
                if (isOneSided)
                {
                    bet.BetA_Amount = Convert.ToDouble(txtSenderAmount.Text);
                    bet.BetB_Amount = 0;
                    bet.Pending_Bet = bet.BetA_Amount;
                }
                else
                {
                    bet.BetA_Amount = Convert.ToDouble(txtSenderAmount2.Text);
                    bet.BetB_Amount = Convert.ToDouble(txtReceiverAmount.Text);
                    bet.Pending_Bet = bet.BetA_Amount + bet.BetB_Amount;
                }

                // Use a BetsController to save the bet (to be implemented)
                using (var httpClient = new HttpClient())
                {
                    var betsController = new BetsController(httpClient);
                    Task.Run(async () => await betsController.AddBetAsync(bet)).Wait();
                }

                // Show success message
                lblStatus.Text = "Bet sent successfully!";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                // Clear the form
                txtRecipientUsername.Text = "";
                hdnRecipientUserID.Value = "";
                txtSenderAmount.Text = "";
                txtSenderAmount2.Text = "";
                txtReceiverAmount.Text = "";
                txtDescription.Text = "";
                txtSenderResult.Text = "";
                txtReceiverResult.Text = "";
                chkNeedMediator.Checked = false;
                ddlBetType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error sending bet: " + ex.Message;
            }
        }

        [WebMethod]
        public static List<string> GetUsernames()
        {
            try
            {
                // Create HttpClient (in a real application, you'd want to manage this better)
                using (var httpClient = new HttpClient())
                {
                    // Create the controller
                    var _usersController = new UsersController(httpClient);

                    // Get all users - need to get the Result from the Task
                    var users = _usersController.GetAllUsersAsync().Result;

                    // Extract usernames and return as list
                    return users.Select(u => u.Username).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the error (in a real app)
                Console.WriteLine($"Error getting usernames: {ex.Message}");
                return new List<string>();
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

                    // Get all users - using .Result to get the actual List<User>
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
    }
}