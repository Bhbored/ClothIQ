using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class TrackingEventRepository : BaseRepository<TrackingEventDto>, ITrackingEventRepository
    {
        public TrackingEventRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "tracking_events", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<TrackingEventDto>> GetByOrderIdAsync(Guid orderId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?order_id=eq.{orderId}&order=display_order.asc,created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<TrackingEventDto>(path);
        }

        public async Task<IEnumerable<TrackingEventDto>> GetActiveEventsByOrderIdAsync(Guid orderId)
        {
            var path = $"/rest/v1/{_tableName}?order_id=eq.{orderId}&is_active=eq.true&order=display_order.asc,created_at.desc";
            return await ExecuteGetListAsync<TrackingEventDto>(path);
        }

        public async Task<TrackingEventDto?> GetLastEventByOrderIdAsync(Guid orderId)
        {
            var path = $"/rest/v1/{_tableName}?order_id=eq.{orderId}&order=display_order.desc,created_at.desc&limit=1";
            var items = await ExecuteGetListAsync<TrackingEventDto>(path);
            return items.FirstOrDefault();
        }

        public async Task<IEnumerable<TrackingEventDto>> GetByEventTypeAsync(string eventType, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?event_type=eq.{eventType}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<TrackingEventDto>(path);
        }

        public async Task<bool> SetEventInactiveAsync(Guid eventId)
        {
            var trackingEvent = await GetByIdAsync(eventId);
            if (trackingEvent == null) return false;

            trackingEvent.IsActive = false;
            await UpdateAsync(trackingEvent);
            return true;
        }
    }
}
