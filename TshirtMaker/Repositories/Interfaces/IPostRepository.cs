using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface IPostRepository : IBaseRepository<PostDto>
    {
        /// <summary>Fetches all posts from the posts table (no pagination), ordered by created_at desc.</summary>
        Task<IEnumerable<PostDto>> GetAllPostsAsync();

        Task<IEnumerable<PostDto>> GetByPosterIdAsync(Guid posterId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<PostDto>> GetByDesignIdAsync(Guid designId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<PostDto>> GetFeedAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<PostDto>> GetTrendingAsync(int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<PostDto>> GetLatestAsync(int count = 12, int startIndex = 0);
        Task<int> GetTotalPostsCountAsync();
        Task<bool> ToggleAllowRemixAsync(Guid postId, bool allowRemix);
    }
}