using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace SourDuckWannaBet.Controllers
{
    public class BetsController : ControllerBase
    {
        public readonly SupabaseServices _supabaseService;

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

        [HttpPut]
        public async Task<IActionResult> UpdateBetAsync(Bet bet)
        {
            try
            {
                // Make sure we have the required fields
                if (bet == null || bet.BetID <= 0)
                {
                    return BadRequest("Invalid bet data");
                }

                // Update the bet in the database
                bet.UpdatedAt = DateTime.Now;
                bool success = await _supabaseService.UpdateInTableAsync(bet, "bets", "betID", bet.BetID);

                if (success)
                {
                    return Ok(new { Message = "Bet updated successfully!" });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to update bet." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to update bet: {ex.Message}" });
            }
        }

        public async Task<bool> AcceptOrDenyBetAsync(long betId, string newStatus)
        {
            try
            {
                // Get all bets
                var tableName = "bets";
                var bets = await _supabaseService.GetAllFromTableAsync<Bet>(tableName);

                // Find the specific bet
                var bet = bets.FirstOrDefault(b => b.BetID == betId);

                if (bet == null)
                {
                    return false;
                }

                // Update bet status
                bet.Status = newStatus;
                bet.UpdatedAt = DateTime.Now;

                // Use a direct update approach with minimal properties
                var updateData = new
                {
                    status = newStatus,
                    updated_at = bet.UpdatedAt
                };

                // Prepare the URL for updating the specific bet
                var url = $"{_supabaseService._supabaseUrl}/rest/v1/bets?bet_id=eq.{betId}";
                var json = JsonConvert.SerializeObject(updateData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Create an HttpRequestMessage for PATCH request
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
                {
                    Content = content
                };

                // Set the headers
                request.Headers.Add("apikey", _supabaseService._supabaseServiceRoleKey);
                request.Headers.Add("Authorization", $"Bearer {_supabaseService._supabaseServiceRoleKey}");

                // Send the request asynchronously
                var response = await _supabaseService._httpClient.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AcceptOrDenyBetAsync: {ex.Message}");
                return false;
            }
        }
    }
}