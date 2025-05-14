using Models;
using SourDuckWannaBet.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Utilities;

namespace SourDuckWannaBet
{
    public partial class WBProfile : System.Web.UI.Page
    {
        private User _user;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve user ID from the cookie
                if (Request.Cookies["UserID"] != null)
                {
                    long currentUserId = Convert.ToInt64(Request.Cookies["UserID"].Value);
                    await LoadUserProfileAsync(currentUserId);
                }
                else
                {
                    // Redirect to login if user is not authenticated
                    Response.Redirect("WBDashboard.aspx");
                }
            }
        }

        private async Task LoadUserProfileAsync(long userId)
        {
            var usersController = new UsersController(new HttpClient());
            _user = await usersController.GetUserByUserIDAsync(userId);

            if (_user != null)
            {
                txtFirstName.Text = _user.FirstName;
                txtLastName.Text = _user.LastName;
                txtEmail.Text = _user.Email;
                txtPhoneNumber.Text = _user.PhoneNumber.ToString();
            }
            else
            {
                // Handle user not found (optional)
                Response.Write("<script>alert('User not found.');</script>");
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            var usersController = new UsersController(new HttpClient());
            _user = await usersController.GetUserByUserIDAsync(Convert.ToInt64(Request.Cookies["UserID"].Value));
            //if (_user != null)
            //{
            // Update the user's profile with the modified values
            //_user.UserID = Convert.ToInt64(Request.Cookies["UserID"].Value);
                _user.FirstName = txtFirstName.Text;
                _user.LastName = txtLastName.Text;
                _user.Email = txtEmail.Text;
                _user.PhoneNumber = long.Parse(txtPhoneNumber.Text);

                // If a new password is provided, update it (should hash before storing)
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                //_user.Password = txtPassword.Text; // Ideally, hash the password before saving
                    _user.Password = PasswordHasher.HashPassword(txtPassword.Text);
                }

                //var usersController = new UsersController(new HttpClient());

                // Update user in the database
                bool success = await usersController.UpdateUserAsync(_user);

                if (success)
                {
                    // Redirect to the dashboard after saving the changes
                    Response.Redirect("WBDashboard.aspx");
                }
                else
                {
                    // Handle error (optional)
                    Response.Write("<script>alert('Failed to save profile changes.');</script>");
                }
            //}
        }

        protected void btnDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("WBDashboard.aspx");
        }
    }
}
