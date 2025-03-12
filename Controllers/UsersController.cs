using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;

using Utilities;

namespace SourDuckWannaBet.Controllers
{
    public class UsersController : ControllerBase
    {
        SupabaseServices _supabaseService;

        public UsersController()
        {
            

        }

        public UsersController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<IActionResult> AddUser()
        {
            var newUser = new User
            {
                Username = "john_doe",
                Password = "securePassword123",  // Don't forget to hash passwords!
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = 1234,
                Balance = 1000.50,
                NumWins = 10,
                NumLoses = 5,
                NumBets = 15,
                CreatedAt = DateTime.UtcNow,
                UserType = "basic",
                Subscription = "basic"
            };

            await _supabaseService.AddUserAsync(newUser);
            return Ok("User added successfully.");
        }
    }
}