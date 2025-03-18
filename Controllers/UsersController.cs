using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using System.Linq;

namespace SourDuckWannaBet.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public UsersController(HttpClient httpClient)
        {
            _supabaseService = new SupabaseServices(httpClient);
        }

        public UsersController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<IActionResult> AddUserAsync(User user)
        {
            try
            {
                // Format the command to add a user to the database
                var tableName = "users";
                int userId = await _supabaseService.AddToIndicatedTableAsync(user, tableName);
                return Ok($"User added successfully with ID: {userId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add user: {ex.Message}");
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                // Get all users from the database
                var tableName = "users";
                var users = await _supabaseService.GetAllFromTableAsync<User>(tableName);
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get users: {ex.Message}");
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                // Make sure we have the required fields
                if (user == null || user.UserID <= 0)
                {
                    Console.WriteLine("Invalid user data.");
                    return false;
                }

                // Update the user in the database
                var tableName = "users";
                bool success = await _supabaseService.UpdateInTableAsync(user, tableName, "userID", user.UserID);

                if (success)
                {
                    Console.WriteLine("User updated successfully!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to update user.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateUserAsync: {ex.Message}");
                return false;
            }
        }
        // Method to get the username by userID
        public async Task<string> GetUserNameByUserIDAsync(long userID)
        {
            try
            {
                // Fetch all users (this could be optimized to get only the user you're interested in)
                var users = await GetAllUsersAsync();

                // Find the user based on userID
                var user = users.FirstOrDefault(u => u.UserID == userID);

                return user?.Username; // Return the username or null if not found
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions (e.g., log them)
                throw new Exception($"Error fetching username for userID {userID}: {ex.Message}");
            }
        }
    }
}