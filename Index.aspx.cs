using System;
using System.Net.Http;
using System.Web.UI;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class Index : Page
    {
        private UsersController _usersController;

        public Index()
        {
            // Initialize UsersController with a new HttpClient
            _usersController = new UsersController(new HttpClient());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // No initialization needed here
        }

        // Event handler for the Register button
        protected async void RegisterButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from form fields
                var username = txtUsername.Value;
                var password = txtPassword.Value;
                var firstName = txtFirstName.Value;
                var lastName = txtLastName.Value;
                var email = txtEmail.Value;
                var phoneNumber = txtPhoneNumber.Value;
                var balance = double.Parse(txtBalance.Value);
                var numWins = long.Parse(txtNumWins.Value);
                var numLoses = long.Parse(txtNumLoses.Value);
                var numBets = long.Parse(txtNumBets.Value);
                var userType = txtUserType.Value;
                var subscription = txtSubscription.Value;

                // Create a new User object
                var newUser = new User
                {
                    Username = username,
                    Password = password, // Consider implementing password hashing
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = long.Parse(phoneNumber),
                    Balance = balance,
                    NumWins = numWins,
                    NumLoses = numLoses,
                    NumBets = numBets,
                    CreatedAt = DateTime.UtcNow,
                    UserType = userType,
                    Subscription = subscription
                };

                // Call the controller to add the user
                var result = await _usersController.AddUserAsync(newUser);
                Response.Write("User registered successfully!");
            }
            catch (Exception ex)
            {
                // Display error message
                Response.Write($"Error registering user: {ex.Message}");
            }
        }

        protected void btnViewAllUsers_OnClick(object sender, EventArgs e)
        {
             Response.Redirect("ViewAllUsers.aspx");
        }

        protected void btnSendABet_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SendABet.aspx");
        }

        protected void btnViewBets_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewMyBets.aspx");
        }

        protected void btnViewAllBets_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewAllBets.aspx");
        }

        protected void btnViewSelectedBets_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewSelectedBets.aspx");
        }

        protected void btnBetsControllerDemos_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BetsControllerDemos.aspx");
        }
    }
}