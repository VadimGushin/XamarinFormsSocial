using System.ComponentModel;

namespace XamarinSocial.Enums.Settings
{
    public enum CommentType
    {
        None = 0,
        All = 1,
        Features = 2,

        [Description("None")]
        NoComments = 3
    }
}
