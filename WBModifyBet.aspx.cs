using Models;
using SourDuckWannaBet.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace SourDuckWannaBet
{
    public partial class WBModifyBet : System.Web.UI.Page
    {
        private long _betId;
        private Bet _bet;
        private User _user;
        private User _user2;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the bet ID from the query string
                if (Request.Cookies["BetID"] != null)
                {
                    _betId = Convert.ToInt64(Request.Cookies["BetID"].Value);
                    await LoadBetDataAsync();  // Await the async method here
                }
            }
        }

        private async Task LoadBetDataAsync()
        {
            var betsController = new BetsController(new HttpClient());
            _bet = await betsController.GetBetByIdAsync(_betId);
            if (_bet != null)
            {
                lblBetDescription.Text = _bet.Description;
                lblSenderVsReceiver.Text = $"{_bet.UserID_Sender} vs {_bet.UserID_Receiver}";
                txtBetA_Amount.Text = _bet.BetA_Amount.ToString();
                txtBetB_Amount.Text = _bet.BetB_Amount.ToString();
                txtSender_Result.Text = _bet.Sender_Result;
                txtReceiver_Result.Text = _bet.Receiver_Result;
            }
            else
            {
                // Handle the case where the bet is not found (optional)
                lblBetDescription.Text = "Bet not found.";
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            var betsController = new BetsController(new HttpClient());
            _bet = await betsController.GetBetByIdAsync(long.Parse(Request.Cookies["BetID"].Value));

            // Ensure _bet is not null before continuing
            if (_bet == null)
            {
                // Handle the case where the bet is not found
                lblBetDescription.Text = "Bet not found.";
                return; // Exit the method early
            }

            // Get the original pending bet amount before updating
            double originalPendingBet = _bet.Pending_Bet;
            long originalSenderId = _bet.UserID_Sender;

            // Store the original receiver to swap roles
            long originalReceiverId = _bet.UserID_Receiver;

            // Update the bet object with the modified values
            _bet.BetA_Amount = long.Parse(txtBetB_Amount.Text);     //swapped txtBetA_Amount and b below, since we r swapping perspectives!!!
            _bet.BetB_Amount = long.Parse(txtBetA_Amount.Text);     //see here too
            _bet.Sender_Result = txtReceiver_Result.Text;           //swap      
            _bet.Receiver_Result = txtSender_Result.Text;           //swap
            _bet.UpdatedAt = DateTime.Now;
            _bet.Status = "Pending"; // Set the status to "Pending"

            // Set the new pending bet amount
            _bet.Pending_Bet = _bet.BetA_Amount;

            // Swap sender and receiver
            _bet.UserID_Sender = originalReceiverId;
            _bet.UserID_Receiver = originalSenderId;

            // Get the current user to refund their original bet
            if (Request.Cookies["UserID"] != null)
            {
                long currentUserId = long.Parse(Request.Cookies["UserID"].Value);
                var usersController = new UsersController(new HttpClient());
                _user = await usersController.GetUserByUserIDAsync(_bet.UserID_Sender);

                // Refund the original pending bet amount
                if (_user != null)
                {
                    _user.Balance += originalPendingBet;
                    await usersController.UpdateUserAsync(_user);
                }

                _user2 = await usersController.GetUserByUserIDAsync(_bet.UserID_Receiver);
                _user2.Balance -= _bet.BetA_Amount;
                await usersController.UpdateUserAsync(_user2);
            }

            // Save the updated bet
            var betsController2 = new BetsController(new HttpClient());
            await betsController2.UpdateBetAsync(_bet);

            // Redirect back to the dashboard
            Response.Redirect("WBDashboard.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

    }
}