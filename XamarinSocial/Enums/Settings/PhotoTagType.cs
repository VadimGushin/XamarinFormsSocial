using System.ComponentModel;

namespace XamarinSocial.Enums.Settings
{
    public enum PhotoTagType
    {
        None = 0,
        All = 1,
        Features = 2,

        [Description("None")]
        NoPhotoTags = 3
    }
}
