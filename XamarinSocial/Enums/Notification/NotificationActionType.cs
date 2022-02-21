using System.ComponentModel;

namespace XamarinSocial.Enums.Notification
{
    public enum NotificationActionType
    {
        None = 0,

        [Description("liked your post:")]
        LikedPost = 1,

        [Description("liked your comment:")]
        LikedComment = 2,

        [Description("liked your profile:")]
        LikedProfile = 3,

        [Description("viewed your profile:")]
        ViewedProfile = 4,

        [Description("added a new post:")]
        AddedPost = 5,

        [Description("leave you a comment:")]
        LeavedComment = 6,

        [Description("sent you a message:")]
        SendedMessage = 7,

        [Description("replied:")]
        Replied = 8
    }
}
