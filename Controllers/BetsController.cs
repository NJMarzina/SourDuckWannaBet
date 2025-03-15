using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utilities;

namespace SourDuckWannaBet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BetsController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public BetsController(HttpClient httpClient)
        {
            _supabaseService = new SupabaseServices(httpClient);
        }

        [HttpGet]
        public async Task<List<Bet>> GetBetsByUserIDAsync(int userId)
        {
            try
            {
                // Get all bets from the database
                var bets = await _supabaseService.GetAllFromTableAsync<Bet>("bets");

                // Filter bets where the user is either sender or receiver
                return bets.Where(b => b.UserID_Sender == userId || b.UserID_Receiver == userId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get bets: {ex.Message}");
            }
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateBetStatusAsync(int betId, string status)
        {
            try
            {
                // Fetch the bet from the database
                var bets = await _supabaseService.GetAllFromTableAsync<Bet>("bets");
                var bet = bets.FirstOrDefault(b => b.BetID == betId);

                if (bet != null)
                {
                    // Update the status
                    bet.Status = status;

                    // Update the bet in the database
                    await _supabaseService.AddToIndicatedTableAsync(bet, "bets");
                    return Ok(new { Message = "Bet status updated successfully!" });
                }
                else
                {
                    return NotFound(new { Message = "Bet not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to update bet status: {ex.Message}" });
            }
        }

        [HttpPost("add-bet")]
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
    }
}