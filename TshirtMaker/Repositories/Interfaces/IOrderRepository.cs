using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<OrderDto>
    {
        Task<IEnumerable<OrderDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<OrderDto>> GetByStatusAsync(int status, int pageNumber = 1, int pageSize = 10);
        Task<OrderDto?> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<OrderDto>> GetRecentOrdersAsync(int count = 10);
        Task<int> GetUserOrderCountAsync(Guid userId);
    }
}