using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface ICollectionRepository : IBaseRepository<CollectionDto>
    {
        Task<IEnumerable<CollectionDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<CollectionDto>> GetByCopiedDesignIdAsync(Guid copiedDesignId, int pageNumber = 1, int pageSize = 10);
        Task<bool> ExistsAsync(Guid userId, Guid copiedDesignId);
    }
}