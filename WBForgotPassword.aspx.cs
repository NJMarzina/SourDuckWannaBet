using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class WBForgotPassword : System.Web.UI.Page
    {
        private UsersController _usersController;
        private string _supabaseUrl;
        private string _supabaseServiceRoleKey;

        protected void Page_Load(object sender, EventArgs e)
        {
            _usersController = new UsersController(new HttpClient());
            LoadConfig();
        }

        protected async void btnRecover_Click(object sender, EventArgs e)
        {
            string email = txtEmailTO.Value.Trim();

            if (string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Please enter your email address.";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                var users = await _usersController.GetAllUsersAsync();
                User matchingUser = null;

                foreach (User user in users)
                {
                    if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        matchingUser = user;
                        break;
                    }
                }

                if (matchingUser != null)
                {
                    bool emailSent = await SendRecoveryEmail(email);

                    if (emailSent)
                    {
                        lblMessage.Text = "Password recovery instructions have been sent to your email address.";
                        lblMessage.CssClass = "success-message";
                        lblMessage.Visible = true;

                        ClientScript.RegisterStartupScript(this.GetType(), "redirect",
                            "setTimeout(function(){ window.location = 'WBLogin.aspx'; }, 3000);", true);
                    }
                    else
                    {
                        lblMessage.Text = "Failed to send recovery email. Please try again later.";
                        lblMessage.CssClass = "error-message";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Text = "No account found with this email address.";
                    lblMessage.CssClass = "error-message";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"An error occurred: {ex.Message}";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
            }
        }

        private async Task<bool> SendRecoveryEmail(string email)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("apikey", _supabaseServiceRoleKey);

                    var payload = new { email = email };
                    string json = JsonSerializer.Serialize(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_supabaseUrl}/auth/v1/recover", content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Supabase email error: {ex.Message}");
                return false;
            }
        }

        private void LoadConfig()
        {
            var configFilePath = @"C:\Users\njmar\Desktop\SourDuckWannaBet\config.json";

            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            var jsonString = File.ReadAllText(configFilePath);
            var config = JsonSerializer.Deserialize<JsonElement>(jsonString);

            _supabaseUrl = config.GetProperty("supabaseUrl").GetString();
            _supabaseServiceRoleKey = config.GetProperty("supabaseServiceRoleKey").GetString();
        }

        protected void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("WBLogin.aspx");
        }
    }
}
