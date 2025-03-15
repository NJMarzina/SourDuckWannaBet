using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class ViewMyBets : System.Web.UI.Page
    {
        private BetsController _betsController;

        public ViewMyBets()
        {
            _betsController = new BetsController(new HttpClient());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // Hardcoded recipient user ID
                    int recipientUserId = 1;

                    // Get all bets from the controller
                    var bets = await _betsController.GetBetsByUserIDAsync(recipientUserId);

                    // Filter bets where the current user is the recipient and the status is "pending"
                    var receivedBets = bets.Where(b => b.UserID_Receiver == recipientUserId && b.Status == "Pending"); //.ToList();

                    // Bind the bets to the GridView
                    if (receivedBets != null && receivedBets.Count() > 0)
                    {
                        gvBets.DataSource = receivedBets;
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
                    Response.Write($"Error loading bets: {ex.Message}");
                }
            }
        }

        protected async void gvBets_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accept" || e.CommandName == "Deny")
            {
                int betId = Convert.ToInt32(e.CommandArgument);
                string newStatus = e.CommandName == "Accept" ? "accepted" : "denied";

                try
                {
                    // Update the bet status
                    await _betsController.UpdateBetStatusAsync(betId, newStatus);

                    // Refresh the GridView
                    await LoadBets();
                }
                catch (Exception ex)
                {
                    Response.Write($"Error updating bet status: {ex.Message}");
                }
            }
        }

        private async Task LoadBets()
        {
            try
            {
                // Hardcoded recipient user ID
                int recipientUserId = 1;

                // Get all bets from the controller
                var bets = await _betsController.GetBetsByUserIDAsync(recipientUserId);

                // Filter bets where the current user is the recipient and the status is "pending"
                var receivedBets = bets.Where(b => b.UserID_Receiver == recipientUserId && b.Status == "pending").ToList();

                // Bind the bets to the GridView
                if (receivedBets != null && receivedBets.Count > 0)
                {
                    gvBets.DataSource = receivedBets;
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
                Response.Write($"Error loading bets: {ex.Message}");
            }
        }
    }
}