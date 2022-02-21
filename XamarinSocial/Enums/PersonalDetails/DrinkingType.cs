using System.ComponentModel;

namespace XamarinSocial.Enums.PersonalDetails
{
    public enum DrinkingType
    {
        None = 0,
        Beer = 1,
        Wine = 2,
        Cider = 3,

        [Description("Fermented tea")]
        FermentedTea = 4,

        Mead = 5,
        Pulque = 6,

        [Description("Rice wine")]
        RiceWine = 7,

        Others = 8
    }
}
