using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using Models;

namespace Utilities
{
    public class SupabaseServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl;
        private readonly string _supabaseKey;

        public SupabaseServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _supabaseUrl = "https://sliykwxeogrnrqgysvrh.supabase.co";  // Your Supabase URL
            _supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InNsaXlrd3hlb2dybnJxZ3lzdnJoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzQ3MjYyMTIsImV4cCI6MjA1MDMwMjIxMn0.kOenFFc8qe6DPCFs4mLBcRxmsAiS5JimaOKPumTXNTo";  // Your Supabase anon public key
        }

        public async Task AddUserAsync(User user)
        {
            var url = $"{_supabaseUrl}/rest/v1/user";  // The endpoint for your user table
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Set the headers
            content.Headers.Add("Authorization", $"Bearer {_supabaseKey}");
            content.Headers.Add("Accept", "application/json");
            content.Headers.Add("Prefer", "return=representation");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error adding user to Supabase: {response.ReasonPhrase}");
            }

            // Optionally, handle the response (e.g., read the returned JSON)
            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine("User added successfully!");
        }
    }
}
