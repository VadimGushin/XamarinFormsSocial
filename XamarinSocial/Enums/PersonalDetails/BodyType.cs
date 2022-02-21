using System.ComponentModel;

namespace XamarinSocial.Enums.PersonalDetails
{
    public enum BodyType
    {
        None = 0,

        [Description("Ectomorph")]
        Ectomorph = 1,

        [Description("Ecto-Mesomorph")]
        EctoMesomorph = 2,

        [Description("Meso-Ectomorph")]
        MesoEctomorph = 3,

        [Description("Mesomorph")]
        Mesomorph = 4,

        [Description("Meso-Endomorph")]
        MesoEndomorph = 5,

        [Description("Endo-Mesomorph")]
        EndoMesomorph = 6,

        [Description("Endomorph")]
        Endomorph = 7,
    }
}
