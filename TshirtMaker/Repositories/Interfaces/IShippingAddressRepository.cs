using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IShippingAddressRepository : IBaseRepository<ShippingAddressDto>
    {
        Task<IEnumerable<ShippingAddressDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<ShippingAddressDto?> GetDefaultAddressAsync(Guid userId);
        Task<IEnumerable<ShippingAddressDto>> GetByCountryAsync(string countryCode, int pageNumber = 1, int pageSize = 10);
        Task<bool> SetDefaultAddressAsync(Guid addressId, Guid userId);
    }
}