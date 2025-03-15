using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class ViewMyBets : System.Web.UI.Page
    {
        private BetsController _betsController;
        private const int CURRENT_USER_ID = 1; // Hardcoded for now

        public ViewMyBets()
        {
            _betsController = new BetsController(new HttpClient());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadBets();
            }
        }

        private async System.Threading.Tasks.Task LoadBets()
        {
            try
            {
                // Get all bets for this user
                var allUserBets = await _betsController.GetBetsByUserIDAsync(CURRENT_USER_ID);

                // Filter to show only bets where this user is the receiver
                //var receivedBets = new List<Bet>();
                var receivedBets = new List<Bet>();
                foreach (var bet in allUserBets)
                {
                    if (bet.UserID_Receiver == CURRENT_USER_ID)
                    {
                        receivedBets.Add(bet);
                    }
                }

                // Bind the bets to the GridView
                if (receivedBets.Count > 0)
                {
                    gvBets.DataSource = receivedBets;
                    gvBets.DataBind();
                    gvBets.Visible = true;
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
                Response.Write($"<script>alert('Error loading bets: {ex.Message}');</script>");
            }
        }

        protected async void gvBets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int betId = Convert.ToInt32(e.CommandArgument);
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

                // Reload the data
                if (success)
                {
                    await LoadBets();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }
    }
}