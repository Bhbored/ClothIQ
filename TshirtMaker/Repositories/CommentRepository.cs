using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class CommentRepository : BaseRepository<CommentDto>, ICommentRepository
    {
        public CommentRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "comments", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<CommentDto>> GetByPostIdAsync(Guid postId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?post_id=eq.{postId}&order=created_at.asc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<CommentDto>(path);
        }

        public async Task<IEnumerable<CommentDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<CommentDto>(path);
        }

        public async Task<int> GetPostCommentsCountAsync(Guid postId)
        {
            var path = $"/rest/v1/{_tableName}?post_id=eq.{postId}";
            var items = await ExecuteGetListAsync<CommentDto>(path);
            return items.Count;
        }
    }
}
