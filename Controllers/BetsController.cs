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
    public class BetsController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public BetsController(HttpClient httpClient)
        {
            _supabaseService = new SupabaseServices(httpClient);
        }

        public BetsController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBetAsync(Bet bet)
        {
            try
            {
                // Add bet to the database
                var tableName = "bets";
                int betId = await _supabaseService.AddToIndicatedTableAsync(bet, tableName);
                return Ok($"Bet added successfully with ID: {betId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add bet: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<List<Bet>> GetBetsByUserIDAsync(int userId)
        {
            try
            {
                // Get all bets from the database
                var tableName = "bets";
                var bets = await _supabaseService.GetAllFromTableAsync<Bet>(tableName);

                // Filter bets where the user is either sender or receiver
                return bets.Where(b => b.UserID_Sender == userId || b.UserID_Receiver == userId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get bets: {ex.Message}");
            }
        }
    }
}