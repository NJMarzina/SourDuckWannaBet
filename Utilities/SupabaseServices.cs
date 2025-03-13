using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            _supabaseUrl = "https://sliykwxeogrnrqgysvrh.supabase.co";
            _supabaseServiceRoleKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InNsaXlrd3hlb2dybnJxZ3lzdnJoIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczNDcyNjIxMiwiZXhwIjoyMDUwMzAyMjEyfQ.ycvakwhbuLIowmE7X_V-AXCB5GB2EWmbr1_ua9JMzgM";
        }

        public SupabaseServices(HttpClient httpClient, string supabaseUrl, string supabaseServiceRoleKey)
        {
            _httpClient = httpClient;
            _supabaseUrl = supabaseUrl;
            _supabaseServiceRoleKey = supabaseServiceRoleKey;
        }

        // Generic method to add any object to any table
        public async Task<int> AddToIndicatedTableAsync<T>(T entity, string tableName)
        {
            // Convert entity to DTO based on its type
            object dto = ConvertToDTO(entity);

            var url = $"{_supabaseUrl}/rest/v1/{tableName}";
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Create an HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            // Set the headers
            request.Headers.Add("apikey", _supabaseServiceRoleKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseServiceRoleKey}");
            request.Headers.Add("Prefer", "return=representation");

            try
            {
                // Send the request asynchronously
                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error adding to {tableName}: {response.StatusCode} - {response.ReasonPhrase}\nDetails: {errorContent}");
                }

                // Handle the response
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Added to {tableName} successfully!");

                // Parse the returned ID from the response
                JArray responseJsonArray = JsonConvert.DeserializeObject<JArray>(responseData);

                // Check if response is an array and contains at least one element
                if (responseJsonArray.Count > 0)
                {
                    // Extract ID from the first element in the array (assuming primary key column name)
                    string idColumnName = GetIdColumnName(tableName);
                    int id = responseJsonArray[0][idColumnName].Value<int>();
                    return id;
                }
                else
                {
                    throw new Exception($"Error: The response does not contain {tableName} data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddToIndicatedTable: {ex.Message}");
                throw;
            }
        }

        // For backward compatibility
        public async Task<int> AddUser(User user)
        {
            return await AddToIndicatedTableAsync(user, "users");
        }

        // Helper method to convert entities to DTOs with proper column naming
        private object ConvertToDTO<T>(T entity)
        {
            if (entity is User user)
            {
                return new
                {
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
            }

            // Add more entity types as needed
            // else if (entity is AnotherModel anotherModel) { ... }

            // Default: return the entity as is (assuming property names match column names)
            return entity;
        }

        // Helper method to get the ID column name for a table
        private string GetIdColumnName(string tableName)
        {
            switch (tableName.ToLower())
            {
                case "users":
                    return "user_id";
                // Add more cases for other tables
                default:
                    return "id";
            }
        }
    }
}