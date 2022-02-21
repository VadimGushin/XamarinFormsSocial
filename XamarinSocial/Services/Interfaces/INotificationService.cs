using System.Threading.Tasks;
using XamarinSocial.Enums.Notification;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Notification;

namespace XamarinSocial.Services.Interfaces
{
    public interface INotificationService
    {
        Task<ResponseModel<NotificationModel>> GetByIdAsync(NotificationType type, int skip, int take, string id = "");
        Task<ResponseModel<BadgeCountNotificationModel>> GetBadgesCountAsync(string id = "");
    }
}
