using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Models;
using Utilities;
using System.Net.Http;

namespace SourDuckWannaBet.Controllers
{
    public class MessagesController
    {
        private readonly SupabaseServices _supabaseServices;

        public MessagesController()
        {
            _supabaseServices = new SupabaseServices(new HttpClient());
        }

        public async Task<int> AddMessageAsync(Message message)
        {
            // Set default values if not provided
            if (message.UserID == 0)
            {
                message.UserID = 1; // Hardcoded UserID as requested
            }

            if (message.CreatedAt == DateTime.MinValue)
            {
                message.CreatedAt = DateTime.Now;
            }

            return await _supabaseServices.AddToIndicatedTableAsync(message, "messages");
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _supabaseServices.GetAllFromTableAsync<Message>("messages");
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            return await _supabaseServices.UpdateInTableAsync(message, "messages", "message_id", message.MessageID);
        }
    }
}