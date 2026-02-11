using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class BookmarkRepository : BaseRepository<BookmarkDto>, IBookmarkRepository
    {
        private readonly JsonSerializerOptions _jsonOptions;
        public BookmarkRepository(HttpClient httpClient,
            string apiKey,
            ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "bookmarks", apiKey, tokenProvider)
        {
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<bool> BookmarkExistsAsync(Guid userId, Guid postId)
        {
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&post_id=eq.{postId}";
            var items = await ExecuteGetListAsync<BookmarkDto>(path);
            return items.Any();
        }

        public async Task<IEnumerable<BookmarkDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<BookmarkDto>(path);
        }

        public async Task<IEnumerable<BookmarkDto>> GetByPostIdAsync(Guid postId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?post_id=eq.{postId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<BookmarkDto>(path);
        }

        public async Task<int> GetPostBookmarksCountAsync(Guid postId)
        {
            var path = $"/rest/v1/{_tableName}?post_id=eq.{postId}";
            var items = await ExecuteGetListAsync<BookmarkDto>(path);
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
