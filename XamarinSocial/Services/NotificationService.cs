using System;
using System.Threading.Tasks;
using XamarinSocial.Enums.Notification;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Notification;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class NotificationService : BaseApiService, INotificationService
    {
        public async Task<ResponseModel<NotificationModel>> GetByIdAsync(NotificationType type, int skip, int take, string id = "")
        {
            var result = new ResponseModel<NotificationModel>();
            result.Data = new NotificationModel();
            if (string.IsNullOrWhiteSpace(id))
            {
                //hardcode
                for (int i = 0; i < take; i++)
                {
                    result.Data.Items.Add(InitNotificationModel(type));
                }
            }

            //todo: use ExecuteGetAsync() to get collection from API
            return result;
        }

        public async Task<ResponseModel<BadgeCountNotificationModel>> GetBadgesCountAsync(string id = "")
        {
            var result = new ResponseModel<BadgeCountNotificationModel>();
            result.Data = new BadgeCountNotificationModel(0,0,0);
            //hardcode
            if (string.IsNullOrWhiteSpace(id))
            {
                var random = new Random();
                result.Data = new BadgeCountNotificationModel(random.Next(0,30), random.Next(8, 55), random.Next(25,169));
            }
            //todo: use ExecuteGetAsync() to get collection from API
            return result;
        }

        #region Private Methods

        //Hardcode
        private NotificationItemModel InitNotificationModel(NotificationType type)
        {
            NotificationActionType[] likesActionType = { NotificationActionType.LikedComment, NotificationActionType.LikedPost, NotificationActionType.LikedProfile };
            NotificationActionType[] moreActionType = { NotificationActionType.AddedPost, NotificationActionType.LeavedComment, NotificationActionType.SendedMessage, NotificationActionType.Replied };

            NotificationActionType actionType = NotificationActionType.None;
            var random = new Random();
            if (type == NotificationType.Likes)
            {
                actionType = likesActionType[random.Next(0, likesActionType.Length)];
            }
            if (type == NotificationType.Views)
            {
                actionType = NotificationActionType.ViewedProfile;
            }
            if (type == NotificationType.More)
            {
                actionType = moreActionType[random.Next(0, moreActionType.Length)];
            }

            return new NotificationItemModel()
            {
                Type = type,
                ImageSource = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg",
                UserName = "UserName",
                CreationDate = DateTime.Now,
                Message = actionType == NotificationActionType.ViewedProfile 
                          || actionType == NotificationActionType.LikedProfile
                          ? string.Empty
                          : "Test message for user test data is added from service",
                ActionType = actionType
            };
        }

        #endregion
    }
}
