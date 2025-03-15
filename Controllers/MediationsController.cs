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
    public class MediationsController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public MediationsController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpGet]
        public async Task<List<Mediation>> GetAllMediations()
        {
            return await _supabaseService.GetAllFromTableAsync<Mediation>("mediations");
        }

        [HttpPost]
        public async Task<IActionResult> AddMediation(Mediation mediation)
        {
            try
            {
                int mediationId = await _supabaseService.AddToIndicatedTableAsync(mediation, "mediations");
                return Ok(new { Message = "Mediation added successfully!", MediationId = mediationId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to add mediation: {ex.Message}" });
            }
        }
    }
}