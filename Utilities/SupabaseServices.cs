using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models;
using System.Collections.Generic;

namespace Utilities
{
    public class SupabaseServices
    {
        public readonly HttpClient _httpClient;
        public readonly string _supabaseUrl;
        public readonly string _supabaseServiceRoleKey;

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
            else if (entity is Bet bet)
            {
                return new
                {
                    userID_Sender = bet.UserID_Sender,
                    userID_Receiver = bet.UserID_Receiver,
                    betA_Amount = bet.BetA_Amount,
                    betB_Amount = bet.BetB_Amount,
                    pending_Bet = bet.Pending_Bet,
                    description = bet.Description,
                    status = bet.Status,
                    sender_Result = bet.Sender_Result,
                    receiver_Result = bet.Receiver_Result,
                    sender_Balance_Change = bet.Sender_Balance_Change,
                    receiver_Balance_Change = bet.Receiver_Balance_Change,
                    userID_Mediator = bet.UserID_Mediator,
                    updated_at = bet.UpdatedAt
                };
            }
            else if (entity is Transaction transaction)
            {
                return new
                {
                    userID = transaction.UserID,
                    betID = transaction.BetID,
                    amount = transaction.Amount,
                    transaction_type = transaction.TransactionType,
                    transaction_date = transaction.TransactionDate,
                    senderID = transaction.SenderID,
                    receiverID = transaction.ReceiverID,
                    status = transaction.Status
                };
            }

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
                case "bets":
                    return "bet_id";
                case "transactions":
                    return "transaction_id"; // Assuming the primary key column name for transactions
                // Add more cases for other tables
                default:
                    return "id";
            }
        }

        // Add this method to your existing SupabaseServices class
        public async Task<List<T>> GetAllFromTableAsync<T>(string tableName) where T : class, new()
        {
            var url = $"{_supabaseUrl}/rest/v1/{tableName}?select=*";

            // Create an HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            // Set the headers
            request.Headers.Add("apikey", _supabaseServiceRoleKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseServiceRoleKey}");

            try
            {
                // Send the request asynchronously
                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error getting data from {tableName}: {response.StatusCode} - {response.ReasonPhrase}\nDetails: {errorContent}");
                }

                // Handle the response
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Retrieved data from {tableName} successfully!");

                // Parse the returned data from the response
                var items = JsonConvert.DeserializeObject<List<JObject>>(responseData);

                // Convert JObjects to the requested type
                var result = new List<T>();
                foreach (var item in items)
                {
                    var obj = new T();

                    if (typeof(T) == typeof(User))
                    {
                        var user = obj as User;

                        // Map database columns to C# properties
                        user.UserID = item["userID"]?.Value<int>() ?? 0;
                        user.Username = item["username"]?.Value<string>();
                        user.Password = item["password"]?.Value<string>();
                        user.FirstName = item["first_name"]?.Value<string>();
                        user.LastName = item["last_name"]?.Value<string>();
                        user.Email = item["email"]?.Value<string>();
                        user.PhoneNumber = item["phone_number"]?.Value<long>() ?? 0;
                        user.Balance = item["balance"]?.Value<double>() ?? 0;
                        user.NumWins = item["num_wins"]?.Value<long>() ?? 0;
                        user.NumLoses = item["num_loses"]?.Value<long>() ?? 0;
                        user.NumBets = item["num_bets"]?.Value<long>() ?? 0;
                        user.CreatedAt = item["created_at"]?.Value<DateTime>() ?? DateTime.MinValue;
                        user.UserType = item["user_type"]?.Value<string>();
                        user.Subscription = item["subscription"]?.Value<string>();
                    }

                    // Add handling for other types as needed
                    if (typeof(T) == typeof(Bet))
                    {
                        var bet = obj as Bet;

                        // Map database columns to C# properties
                        bet.BetID = item["betID"]?.Value<long>() ?? 0;
                        bet.UserID_Sender = item["userID_Sender"]?.Value<long>() ?? 0;
                        bet.UserID_Receiver = item["userID_Receiver"]?.Value<long>() ?? 0;
                        bet.Created_at = item["created_at"]?.Value<DateTime>() ?? DateTime.MinValue;
                        bet.BetA_Amount = item["betA_Amount"]?.Value<double>() ?? 0;
                        bet.BetB_Amount = item["betB_Amount"]?.Value<double>() ?? 0;
                        bet.Pending_Bet = item["pending_Bet"]?.Value<double>() ?? 0;
                        bet.Description = item["description"]?.Value<string>();
                        bet.Status = item["status"]?.Value<string>();
                        bet.Sender_Result = item["sender_Result"]?.Value<string>();
                        bet.Receiver_Result = item["receiver_Result"]?.Value<string>();
                        bet.Sender_Balance_Change = item["sender_Balance_Change"]?.Value<double>() ?? 0;
                        bet.Receiver_Balance_Change = item["receiver_Balance_Change"]?.Value<double>() ?? 0;
                        bet.UserID_Mediator = item["userID_Mediator"]?.Value<long>() ?? 0;
                        bet.UpdatedAt = item["updated_at"]?.Value<DateTime>();
                    }

                    result.Add(obj);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetAllFromTableAsync: {ex.Message}");
                throw;
            }
        }

        // Add this method to the existing SupabaseServices class
        public async Task<bool> UpdateInTableAsync<T>(T entity, string tableName, string primaryKeyColumn, object primaryKeyValue)
        {
            // Convert entity to DTO based on its type
            object dto = ConvertToDTO(entity);

            var url = $"{_supabaseUrl}/rest/v1/{tableName}?{primaryKeyColumn}=eq.{primaryKeyValue}";
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Create an HttpRequestMessage for PATCH request
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
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
                    throw new Exception($"Error updating {tableName}: {response.StatusCode} - {response.ReasonPhrase}\nDetails: {errorContent}");
                }

                // Handle the response
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Updated {tableName} successfully!");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateInTableAsync: {ex.Message}");
                throw;
            }
        }
    }
}