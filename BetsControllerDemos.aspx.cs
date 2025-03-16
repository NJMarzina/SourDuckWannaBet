using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SourDuckWannaBet.Controllers;
using Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace SourDuckWannaBet
{
    public partial class BetsControllerDemos : System.Web.UI.Page
    {
        private BetsController _betsController;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the BetsController with HttpClient
            var httpClient = new HttpClient();
            _betsController = new BetsController(httpClient);
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected async void btnGetAllBets_Click(object sender, EventArgs e)
        {
            var bets = await _betsController.GetAllBetsAsync();
            lblAllBets.Text = JsonConvert.SerializeObject(bets, Formatting.Indented);
        }

        protected async void btnGetBetsByUserId_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtUserId.Text, out int userId))
            {
                var bets = await _betsController.GetBetsByUserIDAsync(userId);
                lblBetsByUserId.Text = JsonConvert.SerializeObject(bets, Formatting.Indented);
            }
            else
            {
                lblBetsByUserId.Text = "Invalid User ID";
            }
        }

        protected async void btnAddBet_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtBetSenderId.Text, out int senderId) &&
                int.TryParse(txtBetReceiverId.Text, out int receiverId) &&
                decimal.TryParse(txtBetAmount.Text, out decimal amount))
            {
                var bet = new Bet
                {
                    UserID_Sender = senderId,
                    UserID_Receiver = receiverId,
                    Pending_Bet = double.Parse(amount.ToString()),
                    Status = "Pending",
                    Created_at = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var result = await _betsController.AddBetAsync(bet);
                lblAddBetResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            else
            {
                lblAddBetResult.Text = "Invalid input data";
            }
        }

        protected async void btnUpdateBetStatus_Click(object sender, EventArgs e)
        {
            if (long.TryParse(txtBetId.Text, out long betId))
            {
                string newStatus = ddlBetStatus.SelectedValue;
                bool success = await _betsController.AcceptOrDenyBetAsync(betId, newStatus);
                lblUpdateBetStatusResult.Text = success ? "Bet status updated successfully!" : "Failed to update bet status.";
            }
            else
            {
                lblUpdateBetStatusResult.Text = "Invalid Bet ID";
            }
        }
    }
}