using MvvmCross;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinSocial.Enums.PersonalDetails;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;
using static XamarinSocial.Constants.Constant;

namespace XamarinSocial.Services
{
    public class ProfileService : IProfileService
    {
        #region Services

        private readonly ISecureStorageService _secureStorage = Mvx.IoCProvider.Resolve<ISecureStorageService>();

        #endregion

        public async Task<UserProfile> GetByIdAsync(string id = "")
        {
            var profie = await _secureStorage.GetStringByKey(StorageConfig.UserProfileKey);
            if (!string.IsNullOrWhiteSpace(profie))
            {
                return JsonConvert.DeserializeObject<UserProfile>(profie);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return GetHardcodeProfile();
            }

            //todo: add server logic if needed

            return new UserProfile();
        }

        public async Task SaveAsync(UserProfile profile)
        {
            if (profile == null)
            {
                return;
            }
            var profileString = JsonConvert.SerializeObject(profile);
            await _secureStorage.AddString(StorageConfig.UserProfileKey, profileString);

            //todo: add server logic if needed
        }
        private UserProfile GetHardcodeProfile()
        {
            var profile = new UserProfile()
            {
                Id = "someUserId",
                FirstName = "Sarah",
                Age = "34",
                MinHeight = "4'7\"(140cm)",
                MaxHeight = "5'11\"(180cm)",
                Height = "10",
                City = "New-York",
                BodyType = BodyType.Ectomorph.ToString(),
                Ethnicity = Ethnicity.MiddleEastern.ToString(),
                Religion = Religion.Christianity.ToString(),
                Kids = KidsType.Two.ToString(),
                Smocking = SmockingType.NoSmocking.ToString(),
                Drinking = DrinkingType.Others.ToString(),
                EyesColor = EyesColor.Blue.ToString(),
                HairColor = HairColor.Black.ToString(),
                SexualOrientation = SexualOrientation.Male.ToString(),
                Interests = new List<string>() { "New", "Cinema", "Karate", "Travel", "Fashion", "Fishing", "New", "Cinema", "Karate", "Travel", "Fashion", "Fishing",
                    "New", "Cinema", "Karate", "Travel", "Fashion", "Fishing", "New", "Cinema", "Karate", "Travel", "Fashion", "Fishing" },
                Country = "USA",
                ProfileImageSource = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg",
                IsOnline = true,
                About = "Sapien diam cras ac risus consectetur donec dignissim tempor gravida. Donec dui, natoque sagittis, nunc in integer nunc ultrices."
            };
            profile.Location = $"{profile.Country}, {profile.City}";
            return profile;
        }
    }
}
