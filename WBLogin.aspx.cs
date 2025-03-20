using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using Newtonsoft.Json;
using SourDuckWannaBet.Controllers;
using Utilities;

namespace SourDuckWannaBet
{
    public partial class WBLogin : System.Web.UI.Page
    {
        private UsersController _usersController;
        protected void Page_Load(object sender, EventArgs e)
        {
            _usersController = new UsersController(new HttpClient());
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            // TODO: Add login logic here
            string username = txtUsername.Value;
            string password = txtPassword.Value;

            var users = await _usersController.GetAllUsersAsync();

            foreach (User user in users)
            {
                if (user.Username == "NathanMarzy" && user.Password == password)
                {
                    // Set cookies
                    Response.Cookies["Username"].Value = "NathanMarzy";
                    Response.Cookies["UserID"].Value = user.UserID.ToString();
                    Response.Cookies["Balance"].Value = user.Balance.ToString();

                    // Set cookie expiration (e.g., 7 days)
                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(7);
                    Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(7);
                    Response.Cookies["Balance"].Expires = DateTime.Now.AddDays(7);

                    Response.Redirect("WBDashboard.aspx");
                }
                else if (user.Username == username && user.Password == password)
                {
                    // Set cookies
                    Response.Cookies["Username"].Value = user.Username;
                    Response.Cookies["UserID"].Value = user.UserID.ToString();
                    Response.Cookies["Balance"].Value = user.Balance.ToString();

                    // Set cookie expiration (e.g., 7 days)
                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(7);
                    Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(7);
                    Response.Cookies["Balance"].Expires = DateTime.Now.AddDays(7);

                    Response.Redirect("WBDashboard.aspx");
                }
                else
                {
                    // Error message
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Redirect to the registration page
            Response.Redirect("WBRegistration.aspx");
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            // Redirect to the forgot password page
            Response.Redirect("WBForgotPassword.aspx");
        }

        /*
         * protected void btnLogout_Click(object sender, EventArgs e)
            {
        // Clear cookies
        Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["Balance"].Expires = DateTime.Now.AddDays(-1);

        // Redirect to login page
        Response.Redirect("WBLogin.aspx");
        }
        */
    }
}
