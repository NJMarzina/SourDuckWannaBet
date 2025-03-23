using System;
using Microsoft.AspNetCore.Mvc;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class WBModifyBet : System.Web.UI.Page
    {
        private BetsController _betsController;

        public WBModifyBet()
        {
            _betsController = new BetsController(new System.Net.Http.HttpClient());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["BetID"] == null)
            {
                // Handle the case if BetID is not found in session
                Response.Redirect("WBDashboard.aspx");
                return;
            }

            long betId = (long)Session["BetID"];

            if (!IsPostBack)
            {
                // Fetch the bet using BetID from the database
                Bet bet = await _betsController.GetBetByIdAsync(betId);

                if (bet != null)
                {
                    // Pre-populate form with current bet details
                    hiddenBetID.Value = bet.BetID.ToString();
                    txtBetAmountA.Value = bet.BetA_Amount.ToString();
                    txtBetAmountB.Value = bet.BetB_Amount.ToString();
                    txtDescription.Value = bet.Description;
                    txtSenderResult.Value = bet.Sender_Result;
                    txtReceiverResult.Value = bet.Receiver_Result;
                    ddlStatus.SelectedValue = bet.Status;
                }
                else
                {
                    lblError.Text = "Bet not found.";
                    lblError.Visible = true;
                }
            }
        }

        protected async void btnUpdateBet_Click(object sender, EventArgs e)
        {
            // Validate inputs (you can expand this validation)
            if (string.IsNullOrEmpty(txtBetAmountA.Value) || string.IsNullOrEmpty(txtBetAmountB.Value) ||
                string.IsNullOrEmpty(txtDescription.Value) || string.IsNullOrEmpty(txtSenderResult.Value) ||
                string.IsNullOrEmpty(txtReceiverResult.Value))
            {
                lblError.Text = "Please fill in all fields.";
                lblError.Visible = true;
                return;
            }

            // Get the current bet details
            long betId = Convert.ToInt64(hiddenBetID.Value);
            Bet bet = new Bet
            {
                BetID = betId,
                BetA_Amount = Convert.ToDouble(txtBetAmountA.Value),
                BetB_Amount = Convert.ToDouble(txtBetAmountB.Value),
                Description = txtDescription.Value,
                Sender_Result = txtSenderResult.Value,
                Receiver_Result = txtReceiverResult.Value,
                Status = ddlStatus.SelectedValue
            };

            // Call the UpdateBetAsync method and handle the IActionResult
            var result = await _betsController.UpdateBetAsync(bet);

            if (result is OkObjectResult okResult)
            {
                // If the result is Ok, redirect to the dashboard
                Response.Redirect("WBDashboard.aspx");
            }
            else if (result is BadRequestObjectResult badRequestResult)
            {
                // If the result is BadRequest, show the error message
                var message = badRequestResult.Value as dynamic;
                lblError.Text = message?.Message ?? "Failed to update bet.";
                lblError.Visible = true;
            }
            else
            {
                lblError.Text = "An unexpected error occurred.";
                lblError.Visible = true;
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect back to the dashboard or a safe place
            Response.Redirect("WBDashboard.aspx");
        }
    }
}
