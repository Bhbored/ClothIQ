using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class CollectionRepository : BaseRepository<CollectionDto>, ICollectionRepository
    {
        public CollectionRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "collections", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<CollectionDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<CollectionDto>(path);
        }

        public async Task<IEnumerable<CollectionDto>> GetByCopiedDesignIdAsync(Guid copiedDesignId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?copied_design_id=eq.{copiedDesignId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<CollectionDto>(path);
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid copiedDesignId)
        {
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&copied_design_id=eq.{copiedDesignId}";
            var items = await ExecuteGetListAsync<CollectionDto>(path);
            return items.Any();
        }
    }
}
