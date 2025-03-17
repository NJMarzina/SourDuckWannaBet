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
                this.Controls.Add(lblStatus);

                // Initialize UsersController to access user data
                HttpClient httpClient = new HttpClient();
                UsersController usersController = new UsersController(httpClient);

                // Create a SupabaseServices instance for backup operations
                SupabaseServices supabaseService = new SupabaseServices(httpClient);

                // Step 1: Delete all users from the users_backup table (optional, if you want to clear old data)
                lblStatus.Text = "Clearing users_backup table...";
                lblStatus.ForeColor = System.Drawing.Color.Blue;

                var deleteUrl = $"{supabaseService._supabaseUrl}/rest/v1/users_backup";
                var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, deleteUrl);

                deleteRequest.Headers.Add("apikey", supabaseService._supabaseServiceRoleKey);
                deleteRequest.Headers.Add("Authorization", $"Bearer {supabaseService._supabaseServiceRoleKey}");
                deleteRequest.Headers.Add("Prefer", "return=minimal");

                var deleteResponse = await supabaseService._httpClient.SendAsync(deleteRequest);

                if (!deleteResponse.IsSuccessStatusCode)
                {
                    lblStatus.Text = "Warning: Could not clear backup table. Proceeding with backup...";
                    lblStatus.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    lblStatus.Text = "users_backup table cleared. Fetching users from database...";
                }

                // Step 2: Get all users from the users table
                List<User> allUsers = await usersController.GetAllUsersAsync();

                if (allUsers == null || allUsers.Count == 0)
                {
                    lblStatus.Text = "No users found to backup.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                lblStatus.Text = $"Found {allUsers.Count} users. Starting backup...";

                // Step 3: Process each user
                foreach (User user in allUsers)
                {
                    var userDTO = new
                    {
                        userID = user.UserID,
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

                    // Check if the user already exists in the users_backup table
                    var checkUrl = $"{supabaseService._supabaseUrl}/rest/v1/users_backup?userID=eq.{user.UserID}";
                    var checkRequest = new HttpRequestMessage(HttpMethod.Get, checkUrl);

                    checkRequest.Headers.Add("apikey", supabaseService._supabaseServiceRoleKey);
                    checkRequest.Headers.Add("Authorization", $"Bearer {supabaseService._supabaseServiceRoleKey}");
                    checkRequest.Headers.Add("Prefer", "return=representation");

                    var checkResponse = await supabaseService._httpClient.SendAsync(checkRequest);

                    if (checkResponse.IsSuccessStatusCode)
                    {
                        var existingUser = await checkResponse.Content.ReadAsStringAsync();

                        // Check if the user exists by verifying the response is not empty
                        if (!string.IsNullOrEmpty(existingUser) && existingUser != "[]")
                        {
                            // User exists, perform update (PATCH request)
                            var updateUrl = $"{supabaseService._supabaseUrl}/rest/v1/users_backup?userID=eq.{user.UserID}";

                            var updateRequest = new HttpRequestMessage(new HttpMethod("PATCH"), updateUrl)
                            {
                                Content = content  // The updated user data
                            };

                            updateRequest.Headers.Add("apikey", supabaseService._supabaseServiceRoleKey);
                            updateRequest.Headers.Add("Authorization", $"Bearer {supabaseService._supabaseServiceRoleKey}");
                            updateRequest.Headers.Add("Prefer", "return=representation");

                            var updateResponse = await supabaseService._httpClient.SendAsync(updateRequest);

                            if (updateResponse.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"User {user.UserID} updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Error updating user {user.UserID}");
                            }
                        }
                        else
                        {
                            // User does not exist, perform insert (POST request)
                            var insertUrl = $"{supabaseService._supabaseUrl}/rest/v1/users_backup";

                            using (var insertClient = new HttpClient())
                            {
                                insertClient.DefaultRequestHeaders.Add("apikey", supabaseService._supabaseServiceRoleKey);
                                insertClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseService._supabaseServiceRoleKey}");
                                insertClient.DefaultRequestHeaders.Add("Prefer", "return=minimal");

                                var insertResponse = await insertClient.PostAsync(insertUrl, content);

                                if (insertResponse.IsSuccessStatusCode)
                                {
                                    Console.WriteLine($"User {user.UserID} added successfully.");
                                }
                                else
                                {
                                    Console.WriteLine($"Error inserting user {user.UserID}");
                                }
                            }
                        }
                    }
                    else
                    {
                        // If check failed, it likely means the user doesn't exist, so we insert the new user
                        var insertUrl = $"{supabaseService._supabaseUrl}/rest/v1/users_backup";

                        using (var insertClient = new HttpClient())
                        {
                            insertClient.DefaultRequestHeaders.Add("apikey", supabaseService._supabaseServiceRoleKey);
                            insertClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseService._supabaseServiceRoleKey}");
                            insertClient.DefaultRequestHeaders.Add("Prefer", "return=minimal");

                            var insertResponse = await insertClient.PostAsync(insertUrl, content);

                            if (insertResponse.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"User {user.UserID} added successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Error inserting user {user.UserID}");
                            }
                        }
                    }
                }

                lblStatus.Text = "Backup complete!";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                Console.WriteLine($"Critical error during backup: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                Label lblError = new Label
                {
                    ID = "lblBackupError",
                    Text = $"Backup failed: {ex.Message}",
                    ForeColor = System.Drawing.Color.Red
                };
                this.Controls.Add(lblError);
            }
        }



        protected void btnMarzyBlog_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MarzyBlog.aspx");
        }
    }
}