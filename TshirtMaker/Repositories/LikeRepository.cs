using System.Text;
using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class LikeRepository : BaseRepository<LikeDto>, ILikeRepository
    {
        public LikeRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "likes", apiKey, tokenProvider)
        {
        }

        public async Task<bool> LikeExistsAsync(Guid userId, Guid postId)
        {
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&post_id=eq.{postId}";
            var items = await ExecuteGetListAsync<LikeDto>(path);
            return items.Any();
        }

        public async Task<IEnumerable<LikeDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<LikeDto>(path);
        }

        public async Task<IEnumerable<LikeDto>> GetByPostIdAsync(Guid postId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?post_id=eq.{postId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<LikeDto>(path);
        }

        public async Task<int> GetPostLikesCountAsync(Guid postId)
        {
            var path = $"/rest/v1/{_tableName}?post_id=eq.{postId}";
            var items = await ExecuteGetListAsync<LikeDto>(path);
            return items.Count;
        }

        public async Task<bool> DeleteByUserAndPostAsync(Guid userId, Guid postId)
        {
            try
            {
                var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&post_id=eq.{postId}";
                return await ExecuteDeleteAsync(path);
            }
            catch
            {
                return false;
            }
        }
    }
}
