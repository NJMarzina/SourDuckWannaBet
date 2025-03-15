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
    public class NotificationsController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public NotificationsController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpGet]
        public async Task<List<Notification>> GetAllNotifications()
        {
            return await _supabaseService.GetAllFromTableAsync<Notification>("notifications");
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification(Notification notification)
        {
            try
            {
                int notificationId = await _supabaseService.AddToIndicatedTableAsync(notification, "notifications");
                return Ok(new { Message = "Notification added successfully!", NotificationId = notificationId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to add notification: {ex.Message}" });
            }
        }
    }
}