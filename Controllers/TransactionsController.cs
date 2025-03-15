using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace SourDuckWannaBet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public TransactionsController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpGet]
        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await _supabaseService.GetAllFromTableAsync<Transaction>("transactions");
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(Transaction transaction)
        {
            try
            {
                int transactionId = await _supabaseService.AddToIndicatedTableAsync(transaction, "transactions");
                return Ok(new { Message = "Transaction added successfully!", TransactionId = transactionId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to add transaction: {ex.Message}" });
            }
        }
    }
}