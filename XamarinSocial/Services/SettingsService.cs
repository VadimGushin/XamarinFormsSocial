using MvvmCross;
using Newtonsoft.Json;
using System.Threading.Tasks;
using XamarinSocial.Enums.Settings;
using XamarinSocial.Models.Settings;
using XamarinSocial.Services.Interfaces;
using static XamarinSocial.Constants.Constant;

namespace XamarinSocial.Services
{
    public class SettingsService : ISettingsService
    {
        #region Services

        private readonly ISecureStorageService _secureStorageService = Mvx.IoCProvider.Resolve<ISecureStorageService>();

        #endregion

        public async Task<NotificationSettingsModel> GetNotificationSettingsAsync()
        {
            var data = await _secureStorageService.GetStringByKey(StorageConfig.NotificationSettingsKey);
            if (string.IsNullOrWhiteSpace(data))
            {
                //return null;
                return new NotificationSettingsModel()
                {
                    PrivateMessageType = PrivateMessageType.NameAndText,
                    LikeType = LikeType.All,
                    CommentType = CommentType.Features,
                    InterestingPostType = InterestingPostType.All,
                    FriendRequestType = FriendRequestType.NoFriendRequest,
                    PhotoTagType = PhotoTagType.NoPhotoTags,
                    IsPrivateMessagesNotificationTurnedOn = true,
                    IsLikesNotificationTurnedOn = true,
                    IsFriendRequestsNotificationTurnedOn = true
                };
            }
            return JsonConvert.DeserializeObject<NotificationSettingsModel>(data);
        }

        public async Task SetNotificationSettingsAsync(NotificationSettingsModel data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            await _secureStorageService.AddString(StorageConfig.NotificationSettingsKey, serializedData);
        }
    }
}
