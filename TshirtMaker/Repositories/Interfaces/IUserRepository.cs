using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserDto>
    {
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> SearchUsersAsync(string searchTerm, int pageNumber = 1, int pageSize = 10);
        Task<int> GetFollowersCountAsync(Guid userId);
        Task<int> GetFollowingCountAsync(Guid userId);
    }
}