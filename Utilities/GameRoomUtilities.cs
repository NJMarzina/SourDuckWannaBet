using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Utilities
{
    public class GameRoomUtilities
    {
        public readonly HttpClient _httpClient;
        public readonly string _supabaseUrl;
        public readonly string _supabaseServiceRoleKey;

        public GameRoomUtilities(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Load the configuration from the JSON file
            var config = LoadConfig();

            _supabaseUrl = config.GetProperty("supabaseUrl").GetString();
            _supabaseServiceRoleKey = config.GetProperty("supabaseServiceRoleKey").GetString();
        }

        private JsonElement LoadConfig()
        {
            var configFilePath = @"C:\Users\njmar\Desktop\SourDuckWannaBet\config.json";

            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            var jsonString = File.ReadAllText(configFilePath);
            return System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonString);
        }

        public GameRoomUtilities(HttpClient httpClient, string supabaseUrl, string supabaseServiceRoleKey)
        {
            _httpClient = httpClient;
            _supabaseUrl = supabaseUrl;
            _supabaseServiceRoleKey = supabaseServiceRoleKey;
        }

        public async Task<DiceGame> CreateDiceGameAsync(long userId, int sides, int guess, decimal betAmount)
        {
            var game = new DiceGame
            {
                UserID = Convert.ToInt64(userId.ToString()),
                Sides = sides,
                Guess = guess,
                BetAmount = (double)betAmount,
                CreatedAt = DateTime.UtcNow
                // Add other necessary properties
            };

            int gameId = await AddToIndicatedTableAsync(game, "dice_games");
            game.DiceID = gameId;
            return game;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            if (!long.TryParse(userId, out long userIdLong))
            {
                throw new ArgumentException("Invalid user ID format");
            }

            var url = $"{_supabaseUrl}/rest/v1/users?user_id=eq.{userIdLong}&select=*";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("apikey", _supabaseServiceRoleKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseServiceRoleKey}");

            try
            {
                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error getting user: {response.StatusCode} - {response.ReasonPhrase}\nDetails: {errorContent}");
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<JObject>>(responseData);

                if (users.Count == 0)
                {
                    return null;
                }

                var item = users[0];
                var user = new User
                {
                    UserID = item["user_id"]?.Value<int>() ?? 0,
                    Username = item["username"]?.Value<string>(),
                    Password = item["password"]?.Value<string>(),
                    FirstName = item["first_name"]?.Value<string>(),
                    LastName = item["last_name"]?.Value<string>(),
                    Email = item["email"]?.Value<string>(),
                    PhoneNumber = item["phone_number"]?.Value<long>() ?? 0,
                    Balance = item["balance"]?.Value<double>() ?? 0,
                    NumWins = item["num_wins"]?.Value<long>() ?? 0,
                    NumLoses = item["num_loses"]?.Value<long>() ?? 0,
                    NumBets = item["num_bets"]?.Value<long>() ?? 0,
                    CreatedAt = item["created_at"]?.Value<DateTime>() ?? DateTime.MinValue,
                    UserType = item["user_type"]?.Value<string>(),
                    Subscription = item["subscription"]?.Value<string>()
                };

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetUserByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateUserBalanceAsync(string userId, decimal balanceChange)
        {
            if (!long.TryParse(userId, out long userIdLong))
            {
                throw new ArgumentException("Invalid user ID format");
            }

            // First get the user
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found");
            }

            // Update balance
            user.Balance += (double)balanceChange;

            // Update user in database
            await UpdateInTableAsync(user, "users", "user_id", userIdLong);
        }

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
                    if (responseJsonArray[0][idColumnName] != null)
                    {
                        int id = responseJsonArray[0][idColumnName].Value<int>();
                        return id;
                    }
                    else
                    {
                        throw new Exception($"Error: The response does not contain {tableName} data.");
                    }
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

        private object ConvertToDTO<T>(T entity)
        {
            if (entity is DiceGame diceGame)
            {
                return new
                {
                    user_id = diceGame.UserID,
                    sides = diceGame.Sides,
                    guess = diceGame.Guess,
                    bet_amount = diceGame.BetAmount,
                    created_at = diceGame.CreatedAt,
                    result = diceGame.Result
                };
            }
            else if (entity is User user)
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

            // Default: return the entity as is (assuming property names match column names)
            return entity;
        }

        private string GetIdColumnName(string tableName)
        {
            switch (tableName.ToLower())
            {
                case "users":
                    return "user_id";
                case "dice_games":
                    return "game_id";
                // Add cases for other game types as needed
                default:
                    return "id";
            }
        }

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

                    if (typeof(T) == typeof(DiceGame))
                    {
                        var game = obj as DiceGame;

                        // Map database columns to C# properties
                        game.DiceID = item["game_id"]?.Value<int>() ?? 0;
                        game.UserID = item["user_id"]?.Value<long>() ?? 0;
                        game.Sides = item["sides"]?.Value<int>() ?? 0;
                        game.Guess = item["guess"]?.Value<int>() ?? 0;
                        game.BetAmount = item["bet_amount"]?.Value<double>() ?? 0;
                        game.CreatedAt = item["created_at"]?.Value<DateTime>() ?? DateTime.MinValue;
                        game.Result = item["result"]?.Value<int>() ?? 0;
                    }

                    // Add handling for other types as needed
                    // Similar to the SupabaseServices implementation

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
    }
}