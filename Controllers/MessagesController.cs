using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using Utilities;

namespace SourDuckWannaBet.Controllers
{
    public class MessagesController
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl;
        private readonly string _supabaseKey;
        private SupabaseServices supabaseServices;

        public MessagesController(SupabaseServices supabaseServices)
        {
            this.supabaseServices = supabaseServices;
        }

        public MessagesController(HttpClient httpClient, string supabaseUrl, string supabaseKey)
        {
            _httpClient = httpClient;
            _supabaseUrl = supabaseUrl;
            _supabaseKey = supabaseKey;
        }

        // Method to add a message to the database
        public async Task<bool> AddMessageAsync(Message message)
        {
            var url = $"{_supabaseUrl}/rest/v1/messages";
            var messageDTO = new
            {
                userID = message.UserID,
                header = message.Header,
                body = message.Body,
                imageUrl = message.ImageUrl,
                created_at = message.CreatedAt
            };

            var json = JsonConvert.SerializeObject(messageDTO);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            request.Headers.Add("apikey", _supabaseKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseKey}");
            request.Headers.Add("Prefer", "return=representation");

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        // Method to fetch all messages from the database
        public async Task<List<Message>> GetAllMessagesAsync()
        {
            var url = $"{_supabaseUrl}/rest/v1/messages?select=*";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("apikey", _supabaseKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseKey}");

            var response = await _httpClient.SendAsync(request);        /////

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Message>>(json);
            }

            return new List<Message>();
        }
    }
}