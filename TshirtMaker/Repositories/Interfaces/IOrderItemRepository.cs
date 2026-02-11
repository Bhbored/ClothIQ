using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItemDto>
    {
        Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(Guid orderId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<OrderItemDto>> GetByDesignIdAsync(Guid designId, int pageNumber = 1, int pageSize = 10);
        Task<decimal> GetOrderTotalAsync(Guid orderId);
        Task<int> GetOrderItemCountAsync(Guid orderId);
    }
}