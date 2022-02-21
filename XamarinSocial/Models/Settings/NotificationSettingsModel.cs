using MvvmCross.ViewModels;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XamarinSocial.Enums.Settings;
using XamarinSocial.Helpers;

namespace XamarinSocial.Models.Settings
{
    public class NotificationSettingsModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Enum Properties
        public PrivateMessageType PrivateMessageType { get; set; }
        public LikeType LikeType { get; set; }
        public CommentType CommentType { get; set; }
        public InterestingPostType InterestingPostType { get; set; }
        public FriendRequestType FriendRequestType { get; set; }
        public PhotoTagType PhotoTagType { get; set; }

        #endregion

        #region String Properties

        public string PrivateMessageString { get; set; }
        public string LikeString { get; set; }
        public string CommentString { get; set; }
        public string InterestingPostString { get; set; }
        public string FriendRequestString { get; set; }
        public string PhotoTagString { get; set; }

        #endregion

        #region Notifications TurnOn Properties

        public bool IsPrivateMessagesNotificationTurnedOn { get; set; }
        public bool IsLikesNotificationTurnedOn { get; set; }
        public bool IsCommentsNotificationTurnedOn { get; set; }
        public bool IsInterestingPostsNotificationTurnedOn { get; set; }
        public bool IsFriendRequestsNotificationTurnedOn { get; set; }
        public bool IsPhotoTagsNotificationTurnedOn { get; set; }

        #endregion

        #region Constructors

        public NotificationSettingsModel()
        {
            PrivateMessageType = PrivateMessageType.None;
            LikeType = LikeType.None;
            CommentType = CommentType.None;
            InterestingPostType = InterestingPostType.None;
            FriendRequestType = FriendRequestType.None;
            PhotoTagType = PhotoTagType.None;

            IsPrivateMessagesNotificationTurnedOn = false;
            IsLikesNotificationTurnedOn = false;
            IsCommentsNotificationTurnedOn = false;
            IsInterestingPostsNotificationTurnedOn = false;
            IsFriendRequestsNotificationTurnedOn = false;
            IsPhotoTagsNotificationTurnedOn = false;
        }

        #endregion

        #region Public Methods

        public void SetStringValues()
        {
            PrivateMessageString = EnumHelper.GetEnumDescription(Enum.Parse(typeof(PrivateMessageType), PrivateMessageType.ToString()));
            LikeString = EnumHelper.GetEnumDescription(Enum.Parse(typeof(LikeType), LikeType.ToString()));
            CommentString = EnumHelper.GetEnumDescription(Enum.Parse(typeof(CommentType), CommentType.ToString()));
            InterestingPostString = EnumHelper.GetEnumDescription(Enum.Parse(typeof(InterestingPostType), InterestingPostType.ToString()));
            FriendRequestString = EnumHelper.GetEnumDescription(Enum.Parse(typeof(FriendRequestType), FriendRequestType.ToString()));
            PhotoTagString = EnumHelper.GetEnumDescription(Enum.Parse(typeof(PhotoTagType), PhotoTagType.ToString()));
        }

        public void NotifiPropertyChanges()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(bool))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name));
                }
            }

            
        }

        #endregion
    }
}
