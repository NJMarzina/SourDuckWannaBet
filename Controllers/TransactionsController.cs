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
    public class TransactionsController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public TransactionsController(HttpClient httpClient)
        {
            _supabaseService = new SupabaseServices(httpClient);
        }

        public TransactionsController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                // Add transaction to the database
                var tableName = "transactions";
                int transactionId = await _supabaseService.AddToIndicatedTableAsync(transaction, tableName);
                return Ok(new { Message = "Transaction added successfully!", TransactionId = transactionId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to add transaction: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId)
        {
            try
            {
                // Get all transactions from the database
                var tableName = "transactions";
                var transactions = await _supabaseService.GetAllFromTableAsync<Transaction>(tableName);

                // Filter transactions for the specified user
                return transactions.Where(t => t.UserID == userId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get transactions: {ex.Message}");
            }
        }
    }
}