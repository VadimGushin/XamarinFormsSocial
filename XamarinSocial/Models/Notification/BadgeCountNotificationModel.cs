
namespace XamarinSocial.Models.Notification
{
    public class BadgeCountNotificationModel
    {
        public int LikesCount { get; set; }
        public int ViewsCount { get; set; }
        public int MoreCount { get; set; }

        #region Constructors

        public BadgeCountNotificationModel()
        { }

        public BadgeCountNotificationModel(int likes, int views, int more)
        {
            LikesCount = likes;
            ViewsCount = views;
            MoreCount = more;
        }

        #endregion
    }
}
