using System.ComponentModel;

namespace XamarinSocial.Enums.PersonalDetails
{
    public enum HairColor
    {
        None = 0,

        [Description("LightBrown")]
        LightBrown = 1,

        [Description("Auburn/Red")]
        AuburnRed = 2,

        Blonde = 3,

        White = 4,

        [Description("Dark Blonde")]
        DarkBlonde = 5,

        [Description("Dark Brown")]
        DarkBrown = 6,

        Black = 7,
        Grey = 8
    }
}
