using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class ViewSelectedBets : System.Web.UI.Page
    {
        private BetsController _betsController;
        private UsersController _usersController;

        public ViewSelectedBets()
        {
            _betsController = new BetsController(new HttpClient());
            _usersController = new UsersController(new HttpClient());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if username parameter was passed in the URL
                string username = Request.QueryString["username"];
                if (!string.IsNullOrEmpty(username))
                {
                    txtUsername.Text = username;
                    SearchBetsByUsername(username);
                }
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if (!string.IsNullOrEmpty(username))
            {
                SearchBetsByUsername(username);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a username.');", true);
            }
        }

        private async void SearchBetsByUsername(string username)
        {
            try
            {
                // First, find the user by username
                var users = await _usersController.GetAllUsersAsync();
                var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    // User found, display user info
                    lblUserDisplayName.Text = $"{user.FirstName} {user.LastName} ({user.Username})";
                    lblUserID.Text = user.UserID.ToString();
                    pnlUserInfo.Visible = true;
                    pnlNoUser.Visible = false;

                    // Get bets for this user
                    await LoadBetsForUser(int.Parse(user.UserID.ToString()));

                    //a nathan marzina production
                }
                else
                {
                    // User not found
                    pnlUserInfo.Visible = false;
                    pnlBets.Visible = false;
                    pnlNoUser.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }

        private async System.Threading.Tasks.Task LoadBetsForUser(int userId)
        {
            try
            {
                // Get bets for this user
                var userBets = await _betsController.GetBetsByUserIDAsync(userId);

                if (userBets != null && userBets.Count > 0)
                {
                    gvBets.DataSource = userBets;
                    gvBets.DataBind();
                    gvBets.Visible = true;
                    lblNoBets.Visible = false;
                    pnlBets.Visible = true;
                }
                else
                {
                    gvBets.Visible = false;
                    lblNoBets.Visible = true;
                    pnlBets.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load bets: {ex.Message}");
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

        protected async void gvBets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                long betId = Convert.ToInt64(e.CommandArgument);
                bool success = false;
                string message = "";

                if (e.CommandName == "AcceptBet")
                {
                    success = await _betsController.AcceptOrDenyBetAsync(betId, "Accepted");
                    message = success ? "Bet accepted successfully!" : "Failed to accept bet.";
                }
                else if (e.CommandName == "DenyBet")
                {
                    success = await _betsController.AcceptOrDenyBetAsync(betId, "Denied");
                    message = success ? "Bet denied successfully!" : "Failed to deny bet.";
                }

                // Show the result
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);

                // If successful, reload the bets
                if (success)
                {
                    int userId = Convert.ToInt32(lblUserID.Text);
                    await LoadBetsForUser(userId);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }
    }
}