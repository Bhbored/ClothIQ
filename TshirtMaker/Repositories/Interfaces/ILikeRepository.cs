using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface ILikeRepository : IBaseRepository<LikeDto>
    {
        Task<bool> LikeExistsAsync(Guid userId, Guid postId);
        Task<IEnumerable<LikeDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<LikeDto>> GetByPostIdAsync(Guid postId, int pageNumber = 1, int pageSize = 10);
        Task<int> GetPostLikesCountAsync(Guid postId);
        Task<bool> DeleteByUserAndPostAsync(Guid userId, Guid postId);
    }
}