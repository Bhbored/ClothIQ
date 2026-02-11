using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    
    public class PostRepository : BaseRepository<PostDto>, IPostRepository
    {
        public PostRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "posts", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var path = $"/rest/v1/{_tableName}?select=*&order=created_at.desc";
            return await ExecuteGetListAsync<PostDto>(path);
        }

        public async Task<IEnumerable<PostDto>> GetByPosterIdAsync(Guid posterId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?poster_id=eq.{posterId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<PostDto>(path);
        }

        public async Task<IEnumerable<PostDto>> GetByDesignIdAsync(Guid designId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?design_id=eq.{designId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<PostDto>(path);
        }

        public async Task<IEnumerable<PostDto>> GetFeedAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;

            var followerPath = $"/rest/v1/followers?follower_id=eq.{userId}&select=following_id";
            var followers = await ExecuteGetListAsync<FollowerDto>(followerPath);

            var followingIds = followers.Select(f => f.FollowingId).ToList();

            if (!followingIds.Any())
            {
                return new List<PostDto>();
            }

            var idsParam = string.Join(",", followingIds.Select(id => $"\"{id}\""));
            var path = $"/rest/v1/{_tableName}?poster_id=in.({idsParam})&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<PostDto>(path);
        }

        public async Task<IEnumerable<PostDto>> GetTrendingAsync(int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?order=likes_count.desc,created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<PostDto>(path);
        }

        public async Task<IEnumerable<PostDto>> GetLatestAsync(int count = 12, int startIndex = 0)
        {
            var path = $"/rest/v1/{_tableName}?order=created_at.desc&offset={startIndex}&limit={count}";
            return await ExecuteGetListAsync<PostDto>(path);
        }

        public async Task<int> GetTotalPostsCountAsync()
        {
            var path = $"/rest/v1/{_tableName}?select=count";
            var items = await ExecuteGetListAsync<PostDto>(path);
            return items.Count;
        }
        public async Task<bool> ToggleAllowRemixAsync(Guid postId, bool allowRemix)
        {
            var post = await GetByIdAsync(postId);
            if (post == null) return false;

            post.AllowRemix = allowRemix;
            await UpdateAsync(post);
            return true;
        }
    }
}
