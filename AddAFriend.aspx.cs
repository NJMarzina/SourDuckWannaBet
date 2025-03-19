using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;

namespace SourDuckWannaBet
{
    public partial class AddAFriend : System.Web.UI.Page
    {
        private FriendsController _friendsController;
        private UsersController _usersController;
        private long _currentUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Create HttpClient and controllers
            var httpClient = new HttpClient();
            _friendsController = new FriendsController(httpClient);
            _usersController = new UsersController(httpClient);

            // Hardcode the user ID to 1 (bypassing the login check)
            _currentUserId = 1;

            if (!IsPostBack)
            {
                // Load friend requests and friends
                LoadPendingRequests();
                LoadFriends();
            }
        }


        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected async void btnSendRequest_Click(object sender, EventArgs e)
        {
            // Validate username input
            if (string.IsNullOrEmpty(txtFriendUsername.Text))
            {
                lblStatus.Text = "Please enter a username.";
                return;
            }

            try
            {
                // Get user ID from username
                var users = await _usersController.GetAllUsersAsync();
                var targetUser = users.FirstOrDefault(u => u.Username == txtFriendUsername.Text);

                if (targetUser == null)
                {
                    lblStatus.Text = "User not found.";
                    return;
                }

                // Check if trying to add self
                if (targetUser.UserID == _currentUserId)
                {
                    lblStatus.Text = "You cannot add yourself as a friend.";
                    return;
                }

                // Check if friend request already exists
                var existingRequests = await _friendsController.GetFriendRequestsAsync(_currentUserId);
                var alreadyRequested = existingRequests.Any(f =>
                    (f.UserID_1 == _currentUserId && f.UserID_2 == targetUser.UserID) ||
                    (f.UserID_1 == targetUser.UserID && f.UserID_2 == _currentUserId));

                if (alreadyRequested)
                {
                    lblStatus.Text = "A friend request already exists between you and this user.";
                    return;
                }

                // Create new friend request
                var friend = new Friend
                {
                    UserID_1 = _currentUserId,
                    UserID_2 = targetUser.UserID,
                    Status = "pending",
                    CreatedAt = DateTime.Now,
                    AcceptDate = DateTime.Now
                };

                // Send the friend request
                var result = await _friendsController.AddFriendRequestAsync(friend);

                lblStatus.Text = "Friend request sent successfully!";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                // Clear the form
                txtFriendUsername.Text = "";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error sending friend request: " + ex.Message;
            }
        }

        protected async void LoadPendingRequests()
        {
            try
            {
                // Get pending friend requests
                var pendingRequests = await _friendsController.GetPendingReceivedRequestsAsync(_currentUserId);

                // Get all users to display usernames
                var users = await _usersController.GetAllUsersAsync();

                // Bind data to repeater
                rptPendingRequests.DataSource = pendingRequests.Select(f => new {
                    FriendID = f.FriendID,
                    SenderUsername = users.FirstOrDefault(u => u.UserID == f.UserID_1)?.Username ?? "Unknown User",
                    CreatedAt = f.CreatedAt.ToString("MMM dd, yyyy")
                }).ToList();
                rptPendingRequests.DataBind();
            }
            catch (Exception ex)
            {
                lblPendingError.Text = "Error loading pending requests: " + ex.Message;
            }
        }

        protected async void LoadFriends()
        {
            try
            {
                // Get accepted friends
                var friends = await _friendsController.GetAcceptedFriendsAsync(_currentUserId);

                // Get all users to display usernames
                var users = await _usersController.GetAllUsersAsync();

                // Bind data to repeater
                rptFriends.DataSource = friends.Select(f => new {
                    FriendID = f.FriendID,
                    FriendUsername = f.UserID_1 == _currentUserId ?
                        users.FirstOrDefault(u => u.UserID == f.UserID_2)?.Username ?? "Unknown User" :
                        users.FirstOrDefault(u => u.UserID == f.UserID_1)?.Username ?? "Unknown User",
                    AcceptDate = f.AcceptDate?.ToString("MMM dd, yyyy") ?? "Unknown"
                }).ToList();
                rptFriends.DataBind();
            }
            catch (Exception ex)
            {
                lblFriendsError.Text = "Error loading friends: " + ex.Message;
            }
        }

        protected async void btnAccept_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int friendId = Convert.ToInt32(e.CommandArgument);

                // Get the friend request
                var friendRequests = await _friendsController.GetFriendRequestsAsync(_currentUserId);
                var request = friendRequests.FirstOrDefault(f => f.FriendID == friendId);

                if (request != null)
                {
                    // Update the request
                    request.Status = "accepted";
                    request.AcceptDate = DateTime.Now;

                    bool success = await _friendsController.UpdateFriendRequestAsync(request);

                    if (success)
                    {
                        // Reload the data
                        LoadPendingRequests();
                        LoadFriends();
                    }
                    else
                    {
                        lblStatus.Text = "Failed to accept friend request.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error accepting friend request: " + ex.Message;
            }
        }

        protected async void btnReject_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int friendId = Convert.ToInt32(e.CommandArgument);

                // Get the friend request
                var friendRequests = await _friendsController.GetFriendRequestsAsync(_currentUserId);
                var request = friendRequests.FirstOrDefault(f => f.FriendID == friendId);

                if (request != null)
                {
                    // Update the request
                    request.Status = "rejected";

                    bool success = await _friendsController.UpdateFriendRequestAsync(request);

                    if (success)
                    {
                        // Reload the data
                        LoadPendingRequests();
                    }
                    else
                    {
                        lblStatus.Text = "Failed to reject friend request.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error rejecting friend request: " + ex.Message;
            }
        }
    }
}