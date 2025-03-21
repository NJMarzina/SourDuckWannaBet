using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class WBDashboard : System.Web.UI.Page
    {
        private BetsController _betsController;
        private UsersController _usersController;

        public WBDashboard()
        {
            _betsController = new BetsController(new HttpClient());
            _usersController = new UsersController(new HttpClient());
        }
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in (check for cookies)
                if (Request.Cookies["UserID"] == null)
                {
                    // Redirect to login page if not logged in
                    Response.Redirect("WBLogin.aspx");
                    return;
                }

                await LoadAcceptedBetsAsync();
            }
        }

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            // TODO: Implement Send A Bet functionality
            Response.Redirect("SendABet.aspx");
        }

        protected void btnAddFriend_Click(object sender, EventArgs e)
        {
            // TODO: Implement Add A Friend functionality
            Response.Redirect("AddAFriend.aspx");
        }

        private async Task LoadAcceptedBetsAsync()
        {
            var bets = await _betsController.GetAllBetsAsync();

            var acceptedBets = new List<Bet>();

            foreach (var bet in bets)
            {
                if (bet.Status == "Accepted")
                {
                    acceptedBets.Add(bet);
                }
            }


        }
    }
}