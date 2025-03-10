using System;
using System.Web.UI;
using Models;
using Utilities;

namespace SourDuckWannaBet
{
    public partial class Index : Page
    {
        // Reference to the SupabaseService that you have in the Utilities library
        private readonly SupabaseServices _supabaseService;

        public Index()
        {
            // Instantiate the SupabaseService (you can adjust this if you're using DI)
            //_supabaseService = new SupabaseServices(new System.Net.Http.HttpClient());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // If it's a postback and the register button was clicked, we will handle the user registration
            if (IsPostBack && Request["__EVENTTARGET"] == "register")
            {
                string[] userData = Request["__EVENTARGUMENT"].Split(',');

                if (userData.Length == 12)
                {
                    var newUser = new User
                    {
                        UserID = long.Parse(userData[0]), // Handle UserID generation as needed
                        Username = userData[1],
                        Password = userData[2],
                        FirstName = userData[3],
                        LastName = userData[4],
                        Email = userData[5],
                        PhoneNumber = long.Parse(userData[6]),
                        Balance = double.Parse(userData[7]),
                        NumWins = long.Parse(userData[8]),
                        NumLoses = long.Parse(userData[9]),
                        NumBets = long.Parse(userData[10]),
                        CreatedAt = DateTime.UtcNow,
                        UserType = userData[11],
                        Subscription = userData[12]
                    };

                    // Add the user to the database
                    AddUserAsync(newUser);
                }
            }
        }

        private async void AddUserAsync(User newUser)
        {
            try
            {
                // Call SupabaseService to add the user
                await _supabaseService.AddUserAsync(newUser);
                // Display success message to the user
                Response.Write("<script>alert('User added successfully');</script>");
            }
            catch (Exception ex)
            {
                // Display failure message if something goes wrong
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}
