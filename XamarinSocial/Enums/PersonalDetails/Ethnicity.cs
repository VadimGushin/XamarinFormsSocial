using System.ComponentModel;

namespace XamarinSocial.Enums.PersonalDetails
{
    public enum Ethnicity
    {
        None = 0,

        [Description("Pacific Islander")]
        PacificIslander = 1,

        [Description("Latino/Hispanic")]
        LatinoHispanic = 2,

        [Description("East Indian")]
        EastIndian = 3,

        [Description("White/Caucasian")]
        WhiteCaucasian = 4,

        [Description("Middle Eastern")]
        MiddleEastern = 5,

        [Description("Black/African Decent")]
        BlackAfricanDecent = 6,

        Asian = 7,

        [Description("Native American")]
        NativeAmerican = 8,

        Other = 9
    }
}
