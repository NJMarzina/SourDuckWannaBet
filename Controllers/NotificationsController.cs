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
    public class NotificationsController : ControllerBase
    {
        private readonly SupabaseServices _supabaseService;

        public NotificationsController(HttpClient httpClient)
        {
            _supabaseService = new SupabaseServices(httpClient);
        }

        public NotificationsController(SupabaseServices supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNotificationAsync(Notification notification)
        {
            try
            {
                // Add notification to the database
                var tableName = "notifications";
                int notificationId = await _supabaseService.AddToIndicatedTableAsync(notification, tableName);
                return Ok(new { Message = "Notification added successfully!", NotificationId = notificationId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to add notification: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<List<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            try
            {
                // Get all notifications from the database
                var tableName = "notifications";
                var notifications = await _supabaseService.GetAllFromTableAsync<Notification>(tableName);

                // Filter notifications for the specified user
                return notifications.Where(n => n.UserID == userId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get notifications: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> MarkNotificationAsReadAsync(int notificationId)
        {
            try
            {
                // Get all notifications
                var tableName = "notifications";
                var notifications = await _supabaseService.GetAllFromTableAsync<Notification>(tableName);
                var notification = notifications.FirstOrDefault(n => n.NotificationID == notificationId);

                if (notification == null)
                {
                    return NotFound($"Notification with ID {notificationId} not found.");
                }

                // Mark as read
                notification.IsRead = true;
                await _supabaseService.UpdateInTableAsync(notification, tableName, "notification_id", notificationId);

                return Ok(new { Message = "Notification marked as read!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to update notification: {ex.Message}" });
            }
        }
    }
}