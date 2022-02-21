using System.ComponentModel;

namespace XamarinSocial.Enums.Settings
{
    public enum FriendRequestType
    {
        None = 0,
        All = 1,
        Features = 2,

        [Description("None")]
        NoFriendRequest = 3
    }
}
