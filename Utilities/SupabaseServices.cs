using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Models;

namespace Utilities
{
    public class SupabaseServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl;
        private readonly string _supabaseServiceRoleKey;

        public SupabaseServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _supabaseUrl = "https://sliykwxeogrnrqgysvrh.supabase.co";  // Your Supabase URL
            _supabaseServiceRoleKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InNsaXlrd3hlb2dybnJxZ3lzdnJoIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczNDcyNjIxMiwiZXhwIjoyMDUwMzAyMjEyfQ.ycvakwhbuLIowmE7X_V-AXCB5GB2EWmbr1_ua9JMzgM";  // Use service_role key
        }

        public SupabaseServices(HttpClient httpClient, string supabaseUrl, string supabaseServiceRoleKey)
        {
            _httpClient = httpClient;
            _supabaseUrl = supabaseUrl;
            _supabaseServiceRoleKey = supabaseServiceRoleKey;
        }

        public async Task AddUserAsync(User user)
        {
            // Map C# properties to match PostgreSQL column names
            var userDto = new
            {
                userID = user.UserID,
                username = user.Username,
                password = user.Password,
                first_name = user.FirstName,
                last_name = user.LastName,
                email = user.Email,
                phone_number = user.PhoneNumber,
                balance = user.Balance,
                num_wins = user.NumWins,
                num_loses = user.NumLoses,
                num_bets = user.NumBets,
                created_at = user.CreatedAt,
                user_type = user.UserType,
                subscription = user.Subscription
            };

            var url = $"{_supabaseUrl}/rest/v1/users";  // Make sure "users" matches your table name
            var json = JsonConvert.SerializeObject(userDto);

            // Create the StringContent with Content-Type header
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Create an HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            // Set the headers on the HttpRequestMessage (NOT Content-Type since it's already on the content)
            request.Headers.Add("apikey", _supabaseServiceRoleKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseServiceRoleKey}");
            request.Headers.Add("Prefer", "return=representation");

            try
            {
                // Send the request
                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error adding user to Supabase: {response.StatusCode} - {response.ReasonPhrase}\nDetails: {errorContent}");
                }

                // Optionally, handle the response
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("User added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddUserAsync: {ex.Message}");
                throw;
            }
        }
    }
}