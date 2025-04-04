using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SourDuckWannaBet
{
    public partial class GRDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

                // Set username and balance if available
                if (Request.Cookies["Username"] != null)
                {
                    lblUsername.Text = Request.Cookies["Username"].Value;
                }
                if (Request.Cookies["Balance"] != null)
                {
                    lblBalance.Text = Request.Cookies["Balance"].Value;
                }
            }
        }

        protected void btnReturnToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("WBDashboard.aspx");
        }

        protected void btnCoinFlip_Click(object sender, EventArgs e)
        {
            Response.Redirect("GRCoinFlip.aspx");
        }

        protected void btnDice_Click(object sender, EventArgs e)
        {
            Response.Redirect("GRDice.aspx");
        }

        protected void btnBlackjack_Click(object sender, EventArgs e)
        {
            Response.Redirect("GRBlackjack.aspx");
        }
    }
}