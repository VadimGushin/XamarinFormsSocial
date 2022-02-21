using System;

namespace XamarinSocial.Models.Api.Responses
{
    public class BaseProfileInfo
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageSource { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
