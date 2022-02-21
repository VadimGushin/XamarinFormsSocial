using System.Threading.Tasks;
using XamarinSocial.Models.Settings;

namespace XamarinSocial.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<NotificationSettingsModel> GetNotificationSettingsAsync();
        Task SetNotificationSettingsAsync(NotificationSettingsModel data);
    }
}
