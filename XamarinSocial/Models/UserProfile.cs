using System.Collections.Generic;
using XamarinSocial.Enums.PersonalDetails;

namespace XamarinSocial.Models
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string ProfileImageSource { get; set; }
        public string Location { get; set; }
        public string GoogleLocationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsOnline { get; set; }
        public string About { get; set; }
        public string MaxHeight { get; set; }
        public string MinHeight { get; set; }
        public string Height { get; set; }
        public string BodyType { get; set; }
        public string Ethnicity { get; set; }
        public string Religion { get; set; }
        public string Kids { get; set; }
        public string Smocking { get; set; }
        public string Drinking { get; set; }
        public string EyesColor { get; set; }
        public string HairColor { get; set; }
        public string SexualOrientation { get; set; }
        public List<string> Interests { get; set; }
        public UserProfile()
        {
            Interests = new List<string>();
        }
    }
}
