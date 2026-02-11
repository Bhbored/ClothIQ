using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface ITrackingEventRepository : IBaseRepository<TrackingEventDto>
    {
        Task<IEnumerable<TrackingEventDto>> GetByOrderIdAsync(Guid orderId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<TrackingEventDto>> GetActiveEventsByOrderIdAsync(Guid orderId);
        Task<TrackingEventDto?> GetLastEventByOrderIdAsync(Guid orderId);
        Task<IEnumerable<TrackingEventDto>> GetByEventTypeAsync(string eventType, int pageNumber = 1, int pageSize = 10);
        Task<bool> SetEventInactiveAsync(Guid eventId);
    }
}