using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class OrderRepository : BaseRepository<OrderDto>, IOrderRepository
    {
        public OrderRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "orders", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<OrderDto>(path);
        }

        public async Task<IEnumerable<OrderDto>> GetByStatusAsync(int status, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?status=eq.{status}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<OrderDto>(path);
        }

        public async Task<OrderDto?> GetByOrderNumberAsync(string orderNumber)
        {
            var path = $"/rest/v1/{_tableName}?order_number=eq.{orderNumber}";
            var items = await ExecuteGetListAsync<OrderDto>(path);
            return items.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderDto>> GetRecentOrdersAsync(int count = 10)
        {
            var path = $"/rest/v1/{_tableName}?order=created_at.desc&limit={count}";
            return await ExecuteGetListAsync<OrderDto>(path);
        }

        public async Task<int> GetUserOrderCountAsync(Guid userId)
        {
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}";
            var items = await ExecuteGetListAsync<OrderDto>(path);
            return items.Count;
        }
    }
}
