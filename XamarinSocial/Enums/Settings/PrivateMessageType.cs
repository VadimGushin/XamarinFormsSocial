using System.ComponentModel;

namespace XamarinSocial.Enums.Settings
{
    public enum PrivateMessageType
    {
        None = 0,

        [Description("Name and text")]
        NameAndText = 1,

        [Description("Name only")]
        NameOnly = 2
    }
}
