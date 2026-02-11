using System.Text;
using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class ShippingAddressRepository : BaseRepository<ShippingAddressDto>, IShippingAddressRepository
    {
        public ShippingAddressRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "shipping_addresses", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<ShippingAddressDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<ShippingAddressDto>(path);
        }

        public async Task<ShippingAddressDto?> GetDefaultAddressAsync(Guid userId)
        {
            var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}&is_default=eq.true";
            var items = await ExecuteGetListAsync<ShippingAddressDto>(path);
            return items.FirstOrDefault();
        }

        public async Task<IEnumerable<ShippingAddressDto>> GetByCountryAsync(string countryCode, int pageNumber = 1, int pageSize = 10)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = $"/rest/v1/{_tableName}?country_code=eq.{countryCode}&order=created_at.desc&offset={offset}&limit={pageSize}";
            return await ExecuteGetListAsync<ShippingAddressDto>(path);
        }

        public async Task<bool> SetDefaultAddressAsync(Guid addressId, Guid userId)
        {
            try
            {
                var updateData = new { is_default = false };
                var path = $"/rest/v1/{_tableName}?user_id=eq.{userId}";
                await ExecutePatchAsync<ShippingAddressDto, object>(path, updateData, returnRepresentation: false);

                var setDefaultData = new { is_default = true };
                var setDefaultPath = $"/rest/v1/{_tableName}?id=eq.{addressId}&user_id=eq.{userId}";
                await ExecutePatchAsync<ShippingAddressDto, object>(setDefaultPath, setDefaultData, returnRepresentation: false);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
