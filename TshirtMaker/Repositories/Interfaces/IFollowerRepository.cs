using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IFollowerRepository : IBaseRepository<FollowerDto>
    {
        Task<bool> FollowExistsAsync(Guid followerId, Guid followingId);
        Task<IEnumerable<FollowerDto>> GetFollowersAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<FollowerDto>> GetFollowingAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<int> GetFollowerCountAsync(Guid userId);
        Task<int> GetFollowingCountAsync(Guid userId);
        Task<bool> DeleteByFollowerAndFollowingAsync(Guid followerId, Guid followingId);
        Task<bool> UpdateMutualStatusAsync(Guid followerId, Guid followingId, bool isMutual);
    }
}