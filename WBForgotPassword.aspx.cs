using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Web.UI;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class WBForgotPassword : System.Web.UI.Page
    {
        private UsersController _usersController;

        protected void Page_Load(object sender, EventArgs e)
        {
            _usersController = new UsersController(new HttpClient());
        }

        protected async void btnRecover_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Value.Trim();

            if (string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Please enter your email address.";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                // Get all users and find the one with the matching email
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
                    // User found with the provided email
                    // Send password recovery email
                    bool emailSent = SendRecoveryEmail(email, matchingUser.Username, matchingUser.Password);

                    if (emailSent)
                    {
                        lblMessage.Text = "Password recovery information has been sent to your email address.";
                        lblMessage.CssClass = "success-message";
                        lblMessage.Visible = true;

                        // Redirect to login page after a delay
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
                    // No user found with the provided email
                    lblMessage.Text = "No account found with this email address.";
                    lblMessage.CssClass = "error-message";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                lblMessage.Text = $"An error occurred: {ex.Message}";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
            }
        }

        private bool SendRecoveryEmail(string email, string username, string hashedPassword)
        {
            try
            {
                // Note: Since the password is hashed, we can't send the actual password
                // Instead, we'll send a message indicating that they should reset their password

                SmtpClient smtpClient = new SmtpClient("YourSMTPServer");
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress("noreply@wannabet.com", "Wanna Bet");
                mailMessage.To.Add(email);
                mailMessage.Subject = "Password Recovery";
                mailMessage.Body = $"Hello {username},\n\n" +
                                  $"You recently requested to recover your password for your Wanna Bet account.\n\n" +
                                  $"Your username: {username}\n\n" +
                                  $"For security reasons, we cannot send your current password as it is stored securely.\n" +
                                  $"Please use the 'Reset Password' feature on our website to set a new password.\n\n" +
                                  $"If you did not request this recovery, please ignore this email.\n\n" +
                                  $"Regards,\nWanna Bet Team";

                // Comment this line out for testing - you would uncomment in production
                //smtpClient.Send(mailMessage);

                // For testing purposes, we'll just return true
                return true;
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Debug.WriteLine($"Email sending error: {ex.Message}");
                return false;
            }
        }

        protected void btnBackToLogin_Click(object sender, EventArgs e)
        {
            // Redirect back to the login page
            Response.Redirect("WBLogin.aspx");
        }
    }
}