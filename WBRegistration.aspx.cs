using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class WBRegistration : System.Web.UI.Page
    {
        private UsersController _usersController;

        public WBRegistration()
        {
            _usersController = new UsersController(new HttpClient());
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            // Validation checks (same as before)
            if (string.IsNullOrWhiteSpace(txtUsername.Value))
            {
                lblError.Text = "Username is required.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Value))
            {
                lblError.Text = "First Name is required.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Value))
            {
                lblError.Text = "Last Name is required.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Value))
            {
                lblError.Text = "Email is required.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Value))
            {
                lblError.Text = "Phone Number is required.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Value))
            {
                lblError.Text = "Password is required.";
                lblError.Visible = true;
                return;
            }

            // Fetch all users from the database
            var users = await _usersController.GetAllUsersAsync();

            // Validate if the username already exists
            if (users.Any(u => u.Username == txtUsername.Value))
            {
                lblError.Text = "Username already taken. Please choose a different username.";
                lblError.Visible = true;
                return;
            }

            // Validate if the email already exists
            if (users.Any(u => u.Email == txtEmail.Value))
            {
                lblError.Text = "Email already registered. Please use a different email.";
                lblError.Visible = true;
                return;
            }

            // Validate if the phone number already exists
            if (users.Any(u => u.PhoneNumber == long.Parse(txtPhoneNumber.Value)))
            {
                lblError.Text = "Phone number already registered. Please use a different phone number.";
                lblError.Visible = true;
                return;
            }

            // Create a new user object and hash the password
            User newUser = new User
            {
                Username = txtUsername.Value,
                Password = PasswordHasher.HashPassword(txtPassword.Value), // Hash password here
                FirstName = txtFirstName.Value,
                LastName = txtLastName.Value,
                Email = txtEmail.Value,
                PhoneNumber = long.Parse(txtPhoneNumber.Value),
                Balance = 500,  // Default balance
                NumWins = 0,    // Default wins
                NumLoses = 0,   // Default losses
                NumBets = 0,    // Default bets
                CreatedAt = DateTime.Now,  // Timestamp for when the user is created
                UserType = "Basic",  // Default user type
                Subscription = "Basic"  // Default subscription type
            };

            // Add user to the database
            var result = await _usersController.AddUserAsync(newUser);

            if (result != null) // Check if the user was successfully added
            {
                lblError.Visible = false; // Hide the error label on success
                Response.Redirect("WBDashboard.aspx"); // Redirect to the dashboard after successful registration
            }
            else
            {
                lblError.Text = "Registration failed. Please try again.";
                lblError.Visible = true; // Show error message if registration failed
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Redirect to login page if user already has an account
            Response.Redirect("WBLogin.aspx");
        }
    }
}
