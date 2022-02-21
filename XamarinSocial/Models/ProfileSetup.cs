using XamarinSocial.Enums;

namespace XamarinSocial.Models
{
    public class ProfileSetup
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string GoogleLocationId { get; set; }
        public Gender UserGender { get; set; }
        public Gender LookingForGender { get; set; }
        public int MinLookingForAge { get; set; }
        public int MaxLookingForAge { get; set; }
    }
}
