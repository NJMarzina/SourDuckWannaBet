using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class WBMyBets : System.Web.UI.Page
    {
        private BetsController _betsController;
        private UsersController _usersController;

        public WBMyBets()
        {
            _betsController = new BetsController(new HttpClient());
            _usersController = new UsersController(new HttpClient());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadMyBets();
            }
        }

        private async Task LoadMyBets()
        {
            try
            {
                // Get user ID from cookie
                long currentUserId = -1;
                if (Request.Cookies["UserID"] != null && long.TryParse(Request.Cookies["UserID"].Value, out currentUserId))
                {
                    // Get all bets from the controller
                    var allBets = await _betsController.GetAllBetsAsync();

                    // Filter bets to only show those where the current user is sender or receiver
                    var myBets = allBets.Where(b =>
                        b.UserID_Sender == currentUserId ||
                        b.UserID_Receiver == currentUserId).ToList();

                    // Bind the filtered bets to the GridView
                    if (myBets != null && myBets.Count > 0)
                    {
                        gvBets.DataSource = myBets;
                        gvBets.DataBind();
                        lblNoBets.Visible = false;
                    }
                    else
                    {
                        gvBets.Visible = false;
                        lblNoBets.Visible = true;
                    }
                }
                else
                {
                    // User ID not found in cookie
                    gvBets.Visible = false;
                    lblNoBets.Text = "Please log in to view your bets.";
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
                    return "status-denied";
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

                // Get user ID from cookie
                long currentUserId = -1;
                if (Request.Cookies["UserID"] == null || !long.TryParse(Request.Cookies["UserID"].Value, out currentUserId))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You must be logged in to perform this action.');", true);
                    return;
                }

                // Fetch the existing bet details
                var allBets = await _betsController.GetAllBetsAsync();
                var existingBet = allBets.FirstOrDefault(b => b.BetID == betId);

                if (existingBet == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Bet with ID {betId} not found.');", true);
                    return;
                }

                // Verify the user is the receiver of the bet
                if (existingBet.UserID_Receiver != currentUserId)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You can only accept or deny bets sent to you.');", true);
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
                    success = await _betsController.AcceptOrDenyBetAsync(betId, "Denied", 0);
                    message = success ? "Bet denied successfully!" : "Failed to deny bet.";
                }

                // Show the result
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);

                // Reload the data
                if (success)
                {
                    await LoadMyBets();
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
                            break;
                        case "Denied":
                            lblStatus.CssClass = "status-denied";
                            break;
                    }
                }
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("WBDashboard.aspx");
        }
    }
}
