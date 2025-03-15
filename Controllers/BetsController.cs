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
                return Ok(new { Message = "Bet added successfully!", BetId = betId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to add bet: {ex.Message}" });
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

        // Add this method to the existing BetsController class
        [HttpPut]
        public async Task<IActionResult> UpdateBetAsync(Bet bet)
        {
            try
            {
                // Get all bets to find the one we want to update
                string tableName = "bets";
                var bets = await _supabaseService.GetAllFromTableAsync<Bet>(tableName);
                var existingBet = bets.FirstOrDefault(b => b.BetID == bet.BetID);

                if (existingBet == null)
                {
                    return NotFound($"Bet with ID {bet.BetID} not found.");
                }

                // Update the bet in the database
                bet.UpdatedAt = DateTime.Now;
                await _supabaseService.UpdateInTableAsync(bet, tableName, "bet_id", bet.BetID);

                return Ok(new { Message = "Bet updated successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to update bet: {ex.Message}" });
            }
        }
    }
}