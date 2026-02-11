using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IDesignRepository : IBaseRepository<DesignDto>
    {
        Task<IEnumerable<DesignDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<DesignDto>> GetByClothingTypeAsync(int clothingType, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<DesignDto>> GetByMaterialAsync(string material, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<DesignDto>> GetByStylePresetAsync(int stylePreset, int pageNumber = 1, int pageSize = 10);
    }
}