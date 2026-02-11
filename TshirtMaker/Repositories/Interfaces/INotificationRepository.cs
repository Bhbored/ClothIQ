using TshirtMaker.DTOs;

namespace TshirtMaker.Repositories.Interfaces
{
    public interface INotificationRepository : IBaseRepository<NotificationDto>
    {
        Task<IEnumerable<NotificationDto>> GetByRecipientIdAsync(Guid recipientId, int pageNumber = 1, int pageSize = 10, bool unreadOnly = false);
        Task<int> GetUnreadCountAsync(Guid recipientId);
        Task<bool> MarkAsReadAsync(Guid notificationId);
        Task<bool> MarkAllAsReadAsync(Guid recipientId);
        Task<bool> MarkAsActionTakenAsync(Guid notificationId);
    }
}