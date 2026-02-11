using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IBookmarkRepository : IBaseRepository<BookmarkDto>
    {
        Task<bool> BookmarkExistsAsync(Guid userId, Guid postId);
        Task<IEnumerable<BookmarkDto>> GetByUserIdAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<BookmarkDto>> GetByPostIdAsync(Guid postId, int pageNumber = 1, int pageSize = 10);
        Task<int> GetPostBookmarksCountAsync(Guid postId);
        Task<bool> DeleteByUserAndPostAsync(Guid userId, Guid postId);
    }
}