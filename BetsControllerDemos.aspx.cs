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
            try
            {
                var bets = await _betsController.GetAllBetsAsync();
                if (bets != null && bets.Count > 0)
                {
                    gvAllBets.DataSource = bets;
                    gvAllBets.DataBind();
                    lblNoBets.Visible = false;
                }
                else
                {
                    gvAllBets.Visible = false;
                    lblNoBets.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblNoBets.Text = $"Error: {ex.Message}";
                lblNoBets.Visible = true;
            }
        }

        protected async void btnGetBetsByUserId_Click(object sender, EventArgs e)
        {
            if (long.TryParse(txtUserId.Text, out long userId))
            {
                try
                {
                    var bets = await _betsController.GetBetsByUserIDAsync((int)userId);
                    if (bets != null && bets.Count > 0)
                    {
                        gvBetsByUserId.DataSource = bets;
                        gvBetsByUserId.DataBind();
                        lblNoBetsByUserId.Visible = false;
                    }
                    else
                    {
                        gvBetsByUserId.Visible = false;
                        lblNoBetsByUserId.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblNoBetsByUserId.Text = $"Error: {ex.Message}";
                    lblNoBetsByUserId.Visible = true;
                }
            }
            else
            {
                lblNoBetsByUserId.Text = "Invalid User ID";
                lblNoBetsByUserId.Visible = true;
            }
        }

        protected async void btnAddBet_Click(object sender, EventArgs e)
        {
            if (long.TryParse(txtBetSenderId.Text, out long senderId) &&
                long.TryParse(txtBetReceiverId.Text, out long receiverId) &&
                double.TryParse(txtBetA_Amount.Text, out double betA_Amount) &&
                double.TryParse(txtBetB_Amount.Text, out double betB_Amount) &&
                double.TryParse(txtPendingBet.Text, out double pendingBet) &&
                long.TryParse(txtUserIDMediator.Text, out long mediatorId))
            {
                var bet = new Bet
                {
                    UserID_Sender = senderId,
                    UserID_Receiver = receiverId,
                    BetA_Amount = betA_Amount,
                    BetB_Amount = betB_Amount,
                    Pending_Bet = pendingBet,
                    Description = txtDescription.Text,
                    Status = txtStatus.Text,
                    Sender_Result = txtSenderResult.Text,
                    Receiver_Result = txtReceiverResult.Text,
                    Sender_Balance_Change = double.Parse(txtSenderBalanceChange.Text),
                    Receiver_Balance_Change = double.Parse(txtReceiverBalanceChange.Text),
                    UserID_Mediator = mediatorId,
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