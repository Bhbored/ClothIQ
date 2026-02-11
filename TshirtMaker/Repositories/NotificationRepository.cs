using System.Text;
using System.Text.Json;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class NotificationRepository : BaseRepository<NotificationDto>, INotificationRepository
    {
        public NotificationRepository(HttpClient httpClient, string apiKey, ISupabaseAccessTokenProvider tokenProvider)
            : base(httpClient, "notifications", apiKey, tokenProvider)
        {
        }

        public async Task<IEnumerable<NotificationDto>> GetByRecipientIdAsync(Guid recipientId, int pageNumber = 1, int pageSize = 10, bool unreadOnly = false)
        {
            var offset = (pageNumber - 1) * pageSize;
            var path = unreadOnly
                ? $"/rest/v1/{_tableName}?recipient_id=eq.{recipientId}&is_read=eq.false&order=created_at.desc&offset={offset}&limit={pageSize}"
                : $"/rest/v1/{_tableName}?recipient_id=eq.{recipientId}&order=created_at.desc&offset={offset}&limit={pageSize}";

            return await ExecuteGetListAsync<NotificationDto>(path);
        }

        public async Task<int> GetUnreadCountAsync(Guid recipientId)
        {
            var path = $"/rest/v1/{_tableName}?recipient_id=eq.{recipientId}&is_read=eq.false";
            var items = await ExecuteGetListAsync<NotificationDto>(path);
            return items.Count;
        }

        public async Task<bool> MarkAsReadAsync(Guid notificationId)
        {
            var notification = await GetByIdAsync(notificationId);
            if (notification == null) return false;

            notification.IsRead = true;
            await UpdateAsync(notification);
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(Guid recipientId)
        {
            var updateData = new { is_read = true };

            try
            {
                var path = $"/rest/v1/{_tableName}?recipient_id=eq.{recipientId}&is_read=eq.false";
                await ExecutePatchAsync<NotificationDto, object>(path, updateData, returnRepresentation: false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> MarkAsActionTakenAsync(Guid notificationId)
        {
            var notification = await GetByIdAsync(notificationId);
            if (notification == null) return false;

            notification.ActionTaken = true;
            await UpdateAsync(notification);
            return true;
        }
    }
}
