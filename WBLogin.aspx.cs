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

            foreach(User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    Response.Redirect("Index.aspx");
                }
            }   
            // Example of how you might process the login (authentication, etc.)
            // if (AuthenticateUser(username, password)) {
            //     Response.Redirect("Dashboard.aspx");
            // } else {
            //     // Show error message
            // }
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
