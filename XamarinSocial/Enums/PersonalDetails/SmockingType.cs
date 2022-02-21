using System.ComponentModel;

namespace XamarinSocial.Enums.PersonalDetails
{
    public enum SmockingType
    {
        None = 0,

        [Description("No Smocking")]
        NoSmocking = 1,
        Cigarettes = 2,

        [Description("Light or Menthol Cigarettes")]
        LightOrMentholCigarettes = 3,

        Cigars = 4,
        Pipes = 5,
        Hookahs = 6,

        [Description("Bidis or Clove Cigarettes")]
        BidisOrCloveCigarettes = 7,

        [Description("Electronic Cigarettes")]
        ElectronicCigarettes = 8,

        [Description("Dissolvable Products")]
        DissolvableProducts = 9
    }
}
