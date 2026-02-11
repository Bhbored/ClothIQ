using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface ICommentRepository : IBaseRepository<CommentDto>
    {
        Task<IEnumerable<CommentDto>> GetByPostIdAsync(Guid postId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<CommentDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<int> GetPostCommentsCountAsync(Guid postId);
    }
}