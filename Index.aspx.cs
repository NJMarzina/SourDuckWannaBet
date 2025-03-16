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

        protected async void btnBackupUsersTable_OnClick(object sender, EventArgs e)
        {
            try
            {
                // Create a label to display the backup status
                Label lblStatus = new Label();
                lblStatus.ID = "lblBackupStatus";

                // Initialize UsersController to access user data
                HttpClient httpClient = new HttpClient();
                UsersController usersController = new UsersController(httpClient);

                // Get all users from the users table
                lblStatus.Text = "Fetching users from database...";
                lblStatus.ForeColor = System.Drawing.Color.Blue;
                this.Controls.Add(lblStatus);

                List<User> allUsers = await usersController.GetAllUsersAsync();

                if (allUsers == null || allUsers.Count == 0)
                {
                    lblStatus.Text = "No users found to backup.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                lblStatus.Text = $"Found {allUsers.Count} users. Starting backup...";

                // Create a SupabaseServices instance for backup operations
                SupabaseServices supabaseService = new SupabaseServices(httpClient);

                // Counter for successfully backed up users
                int successCount = 0;

                // Process each user with direct insertion to preserve UserID
                foreach (User user in allUsers)
                {
                    try
                    {
                        // Create a direct insert query that includes the UserID
                        var url = $"{supabaseService._supabaseUrl}/rest/v1/users_backup";

                        // Create a user DTO that includes the UserID
                        var userDTO = new
                        {
                            userID = user.UserID,  // Preserve the original UserID
                            username = user.Username,
                            password = user.Password,
                            first_name = user.FirstName,
                            last_name = user.LastName,
                            email = user.Email,
                            phone_number = user.PhoneNumber,
                            balance = user.Balance,
                            num_wins = user.NumWins,
                            num_loses = user.NumLoses,
                            num_bets = user.NumBets,
                            created_at = user.CreatedAt,
                            user_type = user.UserType,
                            subscription = user.Subscription
                        };

                        var json = JsonConvert.SerializeObject(userDTO);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        // Create an HttpRequestMessage with POST method
                        var request = new HttpRequestMessage(HttpMethod.Post, url)
                        {
                            Content = content
                        };

                        // Set the headers
                        request.Headers.Add("apikey", supabaseService._supabaseServiceRoleKey);
                        request.Headers.Add("Authorization", $"Bearer {supabaseService._supabaseServiceRoleKey}");
                        request.Headers.Add("Prefer", "return=representation");

                        // Send the request asynchronously
                        var response = await supabaseService._httpClient.SendAsync(request);

                        if (response.IsSuccessStatusCode)
                        {
                            successCount++;
                        }
                        else
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Error backing up user {user.UserID}: {errorContent}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error but continue with the next user
                        Console.WriteLine($"Error backing up user {user.UserID}: {ex.Message}");
                    }
                }

                // Display the final result
                if (successCount == allUsers.Count)
                {
                    lblStatus.Text = $"Backup complete! {successCount} users successfully backed up with original UserIDs preserved.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = $"Backup partially complete. {successCount} of {allUsers.Count} users backed up.";
                    lblStatus.ForeColor = System.Drawing.Color.Orange;
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                Label lblError = new Label
                {
                    ID = "lblBackupError",
                    Text = $"Backup failed: {ex.Message}",
                    ForeColor = System.Drawing.Color.Red
                };
                this.Controls.Add(lblError);
            }
        }
    }
}