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
                // Log the bet object being sent
                Console.WriteLine("Bet object being sent:");
                Console.WriteLine($"UserID_Sender: {bet.UserID_Sender}");
                Console.WriteLine($"UserID_Receiver: {bet.UserID_Receiver}");
                Console.WriteLine($"BetA_Amount: {bet.BetA_Amount}");
                Console.WriteLine($"BetB_Amount: {bet.BetB_Amount}");
                Console.WriteLine($"Pending_Bet: {bet.Pending_Bet}");
                Console.WriteLine($"Description: {bet.Description}");
                Console.WriteLine($"Status: {bet.Status}");
                Console.WriteLine($"Sender_Result: {bet.Sender_Result}");
                Console.WriteLine($"Receiver_Result: {bet.Receiver_Result}");
                Console.WriteLine($"Sender_Balance_Change: {bet.Sender_Balance_Change}");
                Console.WriteLine($"Receiver_Balance_Change: {bet.Receiver_Balance_Change}");
                Console.WriteLine($"UserID_Mediator: {bet.UserID_Mediator}");
                Console.WriteLine($"UpdatedAt: {bet.UpdatedAt}");

                // Add bet to the database
                var tableName = "bets";

                // Create a DTO that excludes BetID and Created_at
                var betDto = new
                {
                    user_id_sender = bet.UserID_Sender,
                    user_id_receiver = bet.UserID_Receiver,
                    beta_amount = bet.BetA_Amount,
                    betb_amount = bet.BetB_Amount,
                    pending_bet = bet.Pending_Bet,
                    description = bet.Description,
                    status = bet.Status,
                    sender_result = bet.Sender_Result,
                    receiver_result = bet.Receiver_Result,
                    sender_balance_change = bet.Sender_Balance_Change,
                    receiver_balance_change = bet.Receiver_Balance_Change,
                    user_id_mediator = bet.UserID_Mediator,
                    updated_at = bet.UpdatedAt
                };

                int betId = await _supabaseService.AddToIndicatedTableAsync(betDto, tableName);
                return Ok($"Bet added successfully with ID: {betId}");
            }
            catch (Exception ex)
            {
                // Log the full error message
                Console.WriteLine($"Error adding bet: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return BadRequest($"Failed to add bet: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<List<Bet>> GetBetsByUserIDAsync(int userId)
        {
            try
            {
                // This would require a custom method in SupabaseServices to filter by userID
                // For now, we'll get all bets and filter in memory
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
