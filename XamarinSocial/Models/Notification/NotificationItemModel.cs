using System.Collections.Generic;
using XamarinSocial.Enums.Notification;
using XamarinSocial.Models.Base;

namespace XamarinSocial.Models.Notification
{
    public class NotificationModel : BaseApiModel
    {
        public List<NotificationItemModel> Items { get; set; }

        #region Constructors

        public NotificationModel()
        {
            Items = new List<NotificationItemModel>();
        }

        #endregion
    }

    public class NotificationItemModel : BaseApiModel
    {
        public NotificationType Type { get; set; }
        public string ImageSource { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public NotificationActionType ActionType { get; set; }

        #region Constructors

        public NotificationItemModel()
        {
            
        }

        #endregion
    }
}
