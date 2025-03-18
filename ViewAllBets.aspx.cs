using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class ViewAllBets : System.Web.UI.Page
    {
        private BetsController _betsController;

        public ViewAllBets()
        {
            _betsController = new BetsController(new HttpClient());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadAllBets();
            }
        }

        private async System.Threading.Tasks.Task LoadAllBets()
        {
            try
            {
                // Get all bets from the controller
                // We'll need to modify the BetsController to add a method for this
                var bets = await _betsController.GetAllBetsAsync();

                // Bind the bets to the GridView
                if (bets != null && bets.Count > 0)
                {
                    gvBets.DataSource = bets;
                    gvBets.DataBind();
                    lblNoBets.Visible = false;
                }
                else
                {
                    gvBets.Visible = false;
                    lblNoBets.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Display error message
                Response.Write($"<div style='color:red;'>Error loading bets: {ex.Message}</div>");
            }
        }

        protected string GetStatusCssClass(string status)
        {
            switch (status)
            {
                case "Pending":
                    return "status-pending";
                case "Accepted":
                    return "status-accepted";
                case "Denied":
                    return "status-denied"; //a nathan marzina production
                default:
                    return "";
            }
        }

        protected async void gvBets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Convert the CommandArgument to a long (BetID)
                long betId = Convert.ToInt64(e.CommandArgument);

                // Fetch the existing bet details
                var bets = await _betsController.GetAllBetsAsync();
                var existingBet = bets.FirstOrDefault(b => b.BetID == betId);

                if (existingBet == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Bet with ID {betId} not found.');", true);
                    return;
                }

                bool success = false;
                string message = "";

                if (e.CommandName == "AcceptBet")
                {
                    // Calculate the new Pending_Bet value
                    double newPendingBet = existingBet.BetA_Amount + existingBet.BetB_Amount;

                    // Call AcceptOrDenyBetAsync with the new Pending_Bet value
                    success = await _betsController.AcceptOrDenyBetAsync(betId, "Accepted", newPendingBet);
                    message = success ? "Bet accepted successfully!" : "Failed to accept bet.";
                }
                else if (e.CommandName == "DenyBet")
                {
                    // Set Pending_Bet to 0 for denied bets
                    success = await _betsController.AcceptOrDenyBetAsync(betId, "Denied", 0);   //added 0 to end and overloaded, a nathan marzina production
                    message = success ? "Bet denied successfully!" : "Failed to deny bet.";
                }

                // Show the result
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);

                // Reload the data
                if (success)
                {
                    await LoadAllBets();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }

        protected void gvBets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the status label
                var lblStatus = e.Row.FindControl("lblStatus") as Label;
                if (lblStatus != null)
                {
                    string status = DataBinder.Eval(e.Row.DataItem, "Status")?.ToString() ?? "";

                    // Set the CSS class based on the status
                    switch (status)
                    {
                        case "Pending":
                            lblStatus.CssClass = "status-pending";
                            break;
                        case "Accepted":
                            lblStatus.CssClass = "status-accepted";
                            //TODO add 1 to num_bets in users table
                            break;
                        case "Denied":
                            lblStatus.CssClass = "status-denied";
                            break;
                    }
                }

                // Find the actions panel
                var pnlActions = e.Row.FindControl("pnlActions") as Panel;
                if (pnlActions != null)
                {
                    string status = DataBinder.Eval(e.Row.DataItem, "Status")?.ToString() ?? "";
                    // Only show actions for Pending bets
                    pnlActions.Visible = status == "Pending";
                }
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}