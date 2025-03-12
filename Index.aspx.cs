using System;
using System.Net.Http;
using System.Web.UI;
using Models;
using SourDuckWannaBet.Controllers;
using Utilities;

namespace SourDuckWannaBet
{
    public partial class Index : Page
    {
        // Reference to the SupabaseService that you have in the Utilities library
        private SupabaseServices _supabaseService;

        public Index()
        {
            // Instantiate the SupabaseService (you can adjust this if you're using DI)
            _supabaseService = new SupabaseServices(new System.Net.Http.HttpClient());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize the SupabaseService with necessary configuration
                /*
                _supabaseService = new SupabaseServices(
                    new HttpClient(),
                    "https://sliykwxeogrnrqgysvrh.supabase.co", // Your Supabase URL
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InNsaXlrd3hlb2dybnJxZ3lzdnJoIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczNDcyNjIxMiwiZXhwIjoyMDUwMzAyMjEyfQ.ycvakwhbuLIowmE7X_V-AXCB5GB2EWmbr1_ua9JMzgM" // Your Supabase API Key
                );
                */

                _supabaseService = new SupabaseServices(new HttpClient());
            }
        }

        // This is the event handler for the Register button
        protected void RegisterButton_OnClick(object sender, EventArgs e)
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
                UserID = 123456, // Or generate an ID as needed
                //generate id

                Username = username,
                Password = password, // Remember to hash this password in a real-world application
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

            // Call AddUserAsync method to add the user to the database
            AddUserAsync(newUser);

            _supabaseService.AddUser(newUser);
            //UsersController uc = new UsersController();
            //_ = uc.AddUser();

        }

        private async void AddUserAsync(User newUser)
        {
            try
            {
                // Add the user to the Supabase database using the SupabaseService
                _supabaseService.AddUser(newUser);

                // Success message
                Response.Write("<script>alert('User added successfully');</script>");
            }
            catch (Exception ex)
            {
                // Display error message if the user creation fails
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}
