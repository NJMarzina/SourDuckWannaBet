using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            string username = txtUsername.Value;
            string password = txtPassword.Value;

            var users = await _usersController.GetAllUsersAsync();

            foreach (User user in users)
            {
                if (user.Username == username && PasswordHasher.VerifyPassword(user.Password, password))
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
            }

            // If the username and password don't match
            //lblError.Text = "Invalid username or password.";
            //lblError.Visible = true;
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
    }
}
