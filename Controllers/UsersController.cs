using Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Utilities;

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
    }
}