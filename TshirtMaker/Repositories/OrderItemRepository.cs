using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItemDto>, IOrderItemRepository
    {
        public OrderItemRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "order_items", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(Guid orderId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?order_id=eq.{orderId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<OrderItemDto>(path);
        }

        public async Task<IEnumerable<OrderItemDto>> GetByDesignIdAsync(Guid designId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?design_id=eq.{designId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<OrderItemDto>(path);
        }

        public async Task<decimal> GetOrderTotalAsync(Guid orderId)
        {
            var path = $"/rest/v1/{_tableName}?order_id=eq.{orderId}";
            var items = await ExecuteGetListAsync<OrderItemDto>(path);
            return items.Sum(item => item.TotalPrice);
        }

        public async Task<int> GetOrderItemCountAsync(Guid orderId)
        {
            var path = $"/rest/v1/{_tableName}?order_id=eq.{orderId}";
            var items = await ExecuteGetListAsync<OrderItemDto>(path);
            return items.Count;
        }
    }
}
