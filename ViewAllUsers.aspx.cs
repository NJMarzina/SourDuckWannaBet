using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class ViewAllUsers : System.Web.UI.Page
    {
        private UsersController _usersController;

        public ViewAllUsers()
        {
            _usersController = new UsersController(new HttpClient());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // Get all users from the controller
                    var users = await _usersController.GetAllUsersAsync();

                    // Bind the users to the GridView
                    if (users != null && users.Count > 0)
                    {
                        gvUsers.DataSource = users;
                        gvUsers.DataBind();
                        lblNoUsers.Visible = false;
                    }
                    else
                    {
                        gvUsers.Visible = false;
                        lblNoUsers.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    // Display error message
                    Response.Write($"Error loading users: {ex.Message}");
                }
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}