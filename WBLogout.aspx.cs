using System;
using System.Web;

namespace SourDuckWannaBet
{
    public partial class WBLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Delete the cookies by setting their expiration to a past date
            if (Request.Cookies["Username"] != null)
            {
                HttpCookie usernameCookie = new HttpCookie("Username");
                usernameCookie.Expires = DateTime.Now.AddDays(-1);  // Set expiry to past date
                Response.Cookies.Add(usernameCookie);  // Add the expired cookie to response to delete it
            }

            if (Request.Cookies["UserID"] != null)
            {
                HttpCookie userIdCookie = new HttpCookie("UserID");
                userIdCookie.Expires = DateTime.Now.AddDays(-1);  // Set expiry to past date
                Response.Cookies.Add(userIdCookie);  // Add the expired cookie to response to delete it
            }

            if (Request.Cookies["Balance"] != null)
            {
                HttpCookie balanceCookie = new HttpCookie("Balance");
                balanceCookie.Expires = DateTime.Now.AddDays(-1);  // Set expiry to past date
                Response.Cookies.Add(balanceCookie);  // Add the expired cookie to response to delete it
            }

            // Redirect to the login page after logging out
            Response.Redirect("WBLogin.aspx");
        }
    }
}
