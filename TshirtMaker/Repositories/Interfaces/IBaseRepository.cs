using System.Collections.Generic;
using System.Threading.Tasks;
using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntityDto, new()
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<T?> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}