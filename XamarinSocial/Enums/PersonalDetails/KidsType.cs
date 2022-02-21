using System.ComponentModel;

namespace XamarinSocial.Enums.PersonalDetails
{
    public enum KidsType
    {
        None = 1,

        [Description("No Kids")]
        NoKids = 1,

        One = 2,
        Two = 3,
        Three = 4,
        Four = 5,

        [Description("More than four")]
        MoreThanFour = 6
    }
}
