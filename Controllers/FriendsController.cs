using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Utilities;

namespace SourDuckWannaBet.Controllers
{
    public class FriendsController
    {
        private readonly HttpClient _httpClient;
        private readonly SupabaseServices _supabaseService;

        public FriendsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _supabaseService = new SupabaseServices(_httpClient);
        }

        // Add a new friend request
        public async Task<object> AddFriendRequestAsync(Friend friend)
        {
            try
            {
                var tableName = "friends";
                int friendId = await _supabaseService.AddToIndicatedTableAsync(friend, tableName);
                return new { Message = "Friend request sent successfully!", FriendId = friendId };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending friend request: {ex.Message}");
                return new { Message = $"Failed to send friend request: {ex.Message}" };
            }
        }


        // Get all friend requests for a user (both sent and received)
        public async Task<List<Friend>> GetFriendRequestsAsync(long userId)
        {
            try
            {
                var allFriends = await _supabaseService.GetAllFromTableAsync<Friend>("friends");
                return allFriends.Where(f => f.UserID_1 == userId || f.UserID_2 == userId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting friend requests: {ex.Message}");
                return new List<Friend>();
            }
        }

        // Get pending friend requests received by a user
        public async Task<List<Friend>> GetPendingReceivedRequestsAsync(long userId)
        {
            try
            {
                var allFriends = await _supabaseService.GetAllFromTableAsync<Friend>("friends");
                return allFriends.Where(f => f.UserID_2 == userId && f.Status == "pending").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting pending friend requests: {ex.Message}");
                return new List<Friend>();
            }
        }

        // Update friend request status (accept/reject)
        public async Task<bool> UpdateFriendRequestAsync(Friend friend)
        {
            try
            {
                var tableName = "friends";
                return await _supabaseService.UpdateInTableAsync(friend, tableName, "friendID", friend.FriendID);   //changed friend_id to friendID
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating friend request: {ex.Message}");
                return false;
            }
        }

        // Get all accepted friends for a user
        public async Task<List<Friend>> GetAcceptedFriendsAsync(long userId)
        {
            try
            {
                var allFriends = await _supabaseService.GetAllFromTableAsync<Friend>("friends");
                return allFriends.Where(f => (f.UserID_1 == userId || f.UserID_2 == userId) && f.Status == "accepted").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting accepted friends: {ex.Message}");
                return new List<Friend>();
            }
        }
    }
}