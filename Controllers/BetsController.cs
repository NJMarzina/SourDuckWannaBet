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
                // Log the request information
                Console.WriteLine($"Updating bet {betId} to status: {newStatus}");

                // Prepare the update data
                var updateData = new
                {
                    status = newStatus,
                    updated_at = DateTime.Now
                };

                // Prepare the URL for updating the specific bet
                // Notice the change from bet_id to betID to match your database column
                var url = $"{_supabaseService._supabaseUrl}/rest/v1/bets?betID=eq.{betId}";
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
                request.Headers.Add("Prefer", "return=representation");

                // Log the request
                Console.WriteLine($"Sending PATCH request to: {url}");
                Console.WriteLine($"Request Body: {json}");

                // Send the request asynchronously
                var response = await _supabaseService._httpClient.SendAsync(request);

                // Log the response
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Content: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error updating bet: {response.StatusCode} - {response.ReasonPhrase}");
                    return false;
                }

                Console.WriteLine("Bet updated successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AcceptOrDenyBetAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AcceptOrDenyBetAsync(long betId, string newStatus, double pendingBet)
        {
            try
            {
                // Log the request information
                Console.WriteLine($"Updating bet {betId} to status: {newStatus}");

                // Prepare the update data
                var updateData = new
                {
                    status = newStatus,
                    pending_Bet = pendingBet,
                    updated_at = DateTime.Now
                };

                // Prepare the URL for updating the specific bet
                // Notice the change from bet_id to betID to match your database column
                var url = $"{_supabaseService._supabaseUrl}/rest/v1/bets?betID=eq.{betId}";
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
                request.Headers.Add("Prefer", "return=representation");

                // Log the request
                Console.WriteLine($"Sending PATCH request to: {url}");
                Console.WriteLine($"Request Body: {json}");

                // Send the request asynchronously
                var response = await _supabaseService._httpClient.SendAsync(request);

                // Log the response
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Content: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error updating bet: {response.StatusCode} - {response.ReasonPhrase}");
                    return false;
                }

                Console.WriteLine("Bet updated successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AcceptOrDenyBetAsync: {ex.Message}");
                return false;
            }
        }

        [HttpGet]
        public async Task<List<Bet>> GetAllBetsAsync()
        {
            try
            {
                // Get all bets from the database
                var tableName = "bets";
                return await _supabaseService.GetAllFromTableAsync<Bet>(tableName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get bets: {ex.Message}");
            }
        }
    }
}