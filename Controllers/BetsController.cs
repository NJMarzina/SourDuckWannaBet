﻿using Models;
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

        /*
        [HttpPost]
        public async Task<IActionResult> AddBetAsync(Bet bet)
        {
            try
            {
                // Add bet to the database
                var tableName = "bets";
                int betId = await _supabaseService.AddToIndicatedTableAsync(bet, tableName);
                // Return a successful response with the bet ID
                return Ok(new { Message = "Bet added successfully!", BetId = betId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddBetAsync: {ex.Message}");
                // Return a bad request response with the error message
                return BadRequest(new { Message = $"Failed to add bet: {ex.Message}" });
            }
        }
        */
        [HttpPost]
        public async Task<IActionResult> AddBetAsync(Bet bet)
        {
            try
            {
                // Format the command to add a bet to the database
                var tableName = "bets";
                int betId = await _supabaseService.AddToIndicatedTableAsync(bet, tableName);
                return Ok($"Bet added successfully with ID: {betId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add bet: {ex.Message}");
            }
        }



        /*
         * public async Task<IActionResult> AddUserAsync(User user)
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
        */

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
                // Fetch the existing bet details
                var bets = await GetAllBetsAsync();
                var existingBet = bets.FirstOrDefault(b => b.BetID == betId);

                if (existingBet == null)
                {
                    Console.WriteLine($"Bet with ID {betId} not found.");
                    return false;
                }

                // Fetch sender and receiver details
                using (var httpClient = new HttpClient())
                {
                    var usersController = new UsersController(httpClient);
                    var sender = (await usersController.GetAllUsersAsync()).FirstOrDefault(u => u.UserID == existingBet.UserID_Sender);
                    var receiver = (await usersController.GetAllUsersAsync()).FirstOrDefault(u => u.UserID == existingBet.UserID_Receiver);

                    if (sender == null || receiver == null)
                    {
                        Console.WriteLine("Sender or receiver not found.");
                        return false;
                    }

                    // Handle bet acceptance
                    if (newStatus == "Accepted")
                    {
                        // Check if receiver has enough balance
                        if (receiver.Balance < existingBet.BetB_Amount)
                        {
                            Console.WriteLine("Receiver does not have enough balance to accept the bet.");
                            return false;
                        }

                        // Deduct BetB_Amount from receiver's balance
                        receiver.Balance -= existingBet.BetB_Amount;
                        await usersController.UpdateUserAsync(receiver);

                        // Increment num_bets for both sender and receiver
                        sender.NumBets++;
                        receiver.NumBets++;
                        await usersController.UpdateUserAsync(sender);
                        await usersController.UpdateUserAsync(receiver);

                        // Record the transaction
                        var transaction = new Transaction
                        {
                            BetID = (int)betId,
                            Amount = (decimal)existingBet.BetA_Amount + (decimal)existingBet.BetB_Amount,
                            TransactionType = "bet",
                            TransactionDate = DateTime.Now,
                            SenderID = (int)existingBet.UserID_Sender,
                            ReceiverID = (int)existingBet.UserID_Receiver,
                            Status = "completed"
                        };

                        var transactionsController = new TransactionsController(httpClient);
                        await transactionsController.AddTransactionAsync(transaction);
                    }
                    // Handle bet denial
                    else if (newStatus == "Denied")
                    {
                        // Refund BetA_Amount to sender's balance
                        sender.Balance += existingBet.BetA_Amount;
                        await usersController.UpdateUserAsync(sender);

                        // Record the transaction
                        var transaction = new Transaction
                        {
                            BetID = (int)betId,
                            Amount = (decimal)existingBet.BetA_Amount,
                            TransactionType = "refund",
                            TransactionDate = DateTime.Now,
                            SenderID = (int)existingBet.UserID_Sender,
                            ReceiverID = (int)existingBet.UserID_Receiver,
                            Status = "refunded"
                        };

                        var transactionsController = new TransactionsController(httpClient);
                        await transactionsController.AddTransactionAsync(transaction);
                    }

                    // Update the bet status and pending bet
                    var updateData = new
                    {
                        status = newStatus,
                        pending_Bet = pendingBet,
                        updated_at = DateTime.Now
                    };

                    var url = $"{_supabaseService._supabaseUrl}/rest/v1/bets?betID=eq.{betId}";
                    var json = JsonConvert.SerializeObject(updateData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
                    {
                        Content = content
                    };

                    request.Headers.Add("apikey", _supabaseService._supabaseServiceRoleKey);
                    request.Headers.Add("Authorization", $"Bearer {_supabaseService._supabaseServiceRoleKey}");
                    request.Headers.Add("Prefer", "return=representation");

                    var response = await _supabaseService._httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error updating bet: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }

                    Console.WriteLine("Bet updated successfully!");
                    return true;
                }
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

        [HttpGet]
        public async Task<List<Bet>> GetAcceptedBetsByUserIDAsync(int userId)
        {
            try
            {
                // Get all bets from the database
                var tableName = "bets";
                var bets = await _supabaseService.GetAllFromTableAsync<Bet>(tableName);

                // Filter bets where the user is either sender or receiver and status is "Accepted"
                return bets.Where(b => (b.UserID_Sender == userId || b.UserID_Receiver == userId) && b.Status == "Accepted").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get accepted bets: {ex.Message}");
            }
        }
        public async Task<bool> UpdateBetWinnerAsync(long betId, string winner)
        {
            try
            {
                var bet = await GetBetByIdAsync(betId);

                var usersController = new UsersController(new HttpClient());

                var sender = await usersController.GetUserByUserIDAsync(bet.UserID_Sender);
                var receiver = await usersController.GetUserByUserIDAsync(bet.UserID_Receiver);

                var transactionsController = new TransactionsController(new HttpClient());
                Transaction transaction1 = new Transaction();
                Transaction transaction2 = new Transaction();

                if (bet != null)
                {
                    if (winner == "Sender")
                    {
                        bet.Sender_Result = "Win - " + bet.Sender_Result;
                        bet.Receiver_Result = "Lose - " + bet.Receiver_Result;
                        bet.Status = "Completed";
                        bet.Sender_Balance_Change = bet.Pending_Bet;
                        bet.Receiver_Balance_Change = -bet.Pending_Bet;

                        await UpdateBetAsync(bet);

                        sender.Balance += double.Parse(bet.Pending_Bet.ToString());
                        sender.NumWins += 1;
                        receiver.NumLoses += 1;

                        sender.NumBets += 1;
                        receiver.NumBets += 1;

                        await usersController.UpdateUserAsync(sender);
                        await usersController.UpdateUserAsync(receiver);

                        transaction1.Status = "completed_win_sender";
                        transaction2.Status = "completed_loss_receiver";
                    }
                    else if (winner == "Receiver")
                    {
                        bet.Sender_Result = "Lose - " + bet.Sender_Result;
                        bet.Receiver_Result = "Win - " + bet.Receiver_Result;
                        bet.Status = "Completed";
                        bet.Sender_Balance_Change = -bet.Pending_Bet;
                        bet.Receiver_Balance_Change = bet.Pending_Bet;

                        await UpdateBetAsync(bet);

                        receiver.Balance += double.Parse(bet.Pending_Bet.ToString());
                        receiver.NumWins += 1;
                        sender.NumLoses += 1;

                        sender.NumBets += 1;
                        receiver.NumBets += 1;

                        await usersController.UpdateUserAsync(receiver);
                        await usersController.UpdateUserAsync(sender);

                        transaction1.Status = "completed_win_receiver";
                        transaction2.Status = "completed_loss_sender";
                    }

                    transaction1.BetID = int.Parse(bet.BetID.ToString());
                    transaction1.Amount = int.Parse(bet.Pending_Bet.ToString());
                    transaction1.TransactionType = "win";
                    transaction1.TransactionDate = DateTime.Now;
                    transaction1.SenderID = int.Parse(bet.UserID_Sender.ToString());
                    transaction1.ReceiverID = int.Parse(bet.UserID_Receiver.ToString());

                    //transaction2 starts here
                    transaction2.BetID = int.Parse(bet.BetID.ToString());
                    transaction2.Amount = (int.Parse(bet.Pending_Bet.ToString())) * -1;
                    transaction2.TransactionType = "loss";
                    transaction2.TransactionDate = DateTime.Now;
                    transaction2.SenderID = int.Parse(bet.UserID_Sender.ToString());
                    transaction2.ReceiverID = int.Parse(bet.UserID_Receiver.ToString());

                    var result = await transactionsController.AddTransactionAsync(transaction1);
                    var result2 = await transactionsController.AddTransactionAsync(transaction2);

                    //await UpdateBetAsync(bet);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }
        public async Task<Bet> GetBetByIdAsync(long betID)
        {
            try
            {
                // Fetch all users (this could be optimized to get only the user you're interested in)
                var bets = await GetAllBetsAsync();

                // Find the user based on userID
                var bet = bets.FirstOrDefault(b => b.BetID == betID);

                return bet;
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions (e.g., log them)
                throw new Exception($"Error fetching username for betID {betID}: {ex.Message}");
            }
        }

        public async Task<bool> UpdateBetStatusAsync(long betId, string status)
        {
            try
            {
                // Get the bet by ID
                var bet = await GetBetByIdAsync(betId);

                if (bet != null)
                {
                    // Update only the status field
                    if (status == "Accepted" || status == "Declined")
                    {
                        bet.Status = status;

                        // Save the updated bet
                        await UpdateBetAsync(bet);
                        return true;
                    }
                    else
                    {
                        // Invalid status provided
                        return false;
                    }
                }
                return false; // Bet not found
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }
    }
}