using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class UserRepository : BaseRepository<UserDto>, IUserRepository
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public UserRepository(
            HttpClient httpClient,
            string apiKey,
            ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "users", apiKey, tokenProvider)
        {
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var path = $"/rest/v1/{_tableName}?username=eq.{Uri.EscapeDataString(username)}&select=*";
            return await ExecuteGetSingleAsync<UserDto>(path);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var path = $"/rest/v1/{_tableName}?email=eq.{Uri.EscapeDataString(email)}&select=*";
            return await ExecuteGetSingleAsync<UserDto>(path);
        }

        public async Task<IEnumerable<UserDto>> SearchUsersAsync(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var esc = Uri.EscapeDataString(searchTerm);
            var path = $"/rest/v1/{_tableName}?or=(username.ilike.%25{esc}%25,bio.ilike.%25{esc}%25)&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<UserDto>(path);
        }

        public async Task<int> GetFollowersCountAsync(Guid userId)
        {
            var path = $"/rest/v1/followers?following_user=eq.{userId}&select=count";
            var list = await ExecuteGetListAsync<FollowerDto>(path);
            if (list.Count > 0)
            {
                var first = list.First();
                var countProp = typeof(FollowerDto).GetProperty("count", System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (countProp != null)
                {
                    var val = countProp.GetValue(first);
                    if (val != null && int.TryParse(val.ToString(), out var parsed))
                        return parsed;
                }
            }

            return list.Count;
        }

        public async Task<int> GetFollowingCountAsync(Guid userId)
        {
            var path = $"/rest/v1/followers?follower_id=eq.{userId}&select=count";
            var list = await ExecuteGetListAsync<FollowerDto>(path);
            if (list.Count > 0)
            {
                var first = list.First();
                var countProp = typeof(FollowerDto).GetProperty("count", System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (countProp != null)
                {
                    var val = countProp.GetValue(first);
                    if (val != null && int.TryParse(val.ToString(), out var parsed))
                        return parsed;
                }
            }

            return list.Count;
        }
    }
}