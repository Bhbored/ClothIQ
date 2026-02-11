using System.Text;
using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class FollowerRepository : BaseRepository<FollowerDto>, IFollowerRepository
    {
        public FollowerRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "followers", apiKey, tokenProvider)
        {
        }

        public async Task<bool> FollowExistsAsync(Guid followerId, Guid followingId)
        {
            var path = $"/rest/v1/{_tableName}?follower_id=eq.{followerId}&following_id=eq.{followingId}";
            var items = await ExecuteGetListAsync<FollowerDto>(path);
            return items.Any();
        }

        public async Task<IEnumerable<FollowerDto>> GetFollowersAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?following_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<FollowerDto>(path);
        }

        public async Task<IEnumerable<FollowerDto>> GetFollowingAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?follower_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<FollowerDto>(path);
        }

        public async Task<int> GetFollowerCountAsync(Guid userId)
        {
            var path = $"/rest/v1/{_tableName}?following_id=eq.{userId}";
            var items = await ExecuteGetListAsync<FollowerDto>(path);
            return items.Count;
        }

        public async Task<int> GetFollowingCountAsync(Guid userId)
        {
            var path = $"/rest/v1/{_tableName}?follower_id=eq.{userId}";
            var items = await ExecuteGetListAsync<FollowerDto>(path);
            return items.Count;
        }

        public async Task<bool> DeleteByFollowerAndFollowingAsync(Guid followerId, Guid followingId)
        {
            try
            {
                var path = $"/rest/v1/{_tableName}?follower_id=eq.{followerId}&following_id=eq.{followingId}";
                return await ExecuteDeleteAsync(path);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateMutualStatusAsync(Guid followerId, Guid followingId, bool isMutual)
        {
            var updateData = new { is_mutual = isMutual };

            var path = $"/rest/v1/{_tableName}?follower_id=eq.{followerId}&following_id=eq.{followingId}";
            var result = await ExecutePatchAsync<FollowerDto, object>(path, updateData, returnRepresentation: false);

            if (isMutual)
            {
                var reversePath = $"/rest/v1/{_tableName}?follower_id=eq.{followingId}&following_id=eq.{followerId}";
                await ExecutePatchAsync<FollowerDto, object>(reversePath, updateData, returnRepresentation: false);
            }

            return true;
        }
    }
}
