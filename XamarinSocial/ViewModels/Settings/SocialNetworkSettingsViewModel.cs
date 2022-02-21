using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using Localization = XamarinSocial.Resources.Strings.Strings;

namespace XamarinSocial.ViewModels.Settings
{
    public class SocialNetworkSettingsViewModel : BaseViewModel
    {
        public SocialNetworkSettingsViewModel(IMvxNavigationService navigationService)
                                             : base(navigationService)
        {
            InitControls();
        }

        #region Properties

        private string _updatesBadgeText;
        public string UpdatesBadgeText
        {
            get => _updatesBadgeText;
            set
            {
                SetProperty(ref _updatesBadgeText, value);
            }
        }

        private string _facebookFriendsCount;
        public string FacebookFriendsCount
        {
            get => _facebookFriendsCount;
            set
            {
                SetProperty(ref _facebookFriendsCount, value);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand OpenFacebookFriendsCommand { get; private set; }
        public IMvxCommand OpenBlockedUsersCommand { get; private set; }
        public IMvxCommand OpenUpdatesCommand { get; private set; }

        #endregion

        #region Overrides

        #endregion

        #region Private Methods

        private void InitControls()
        {
            OpenFacebookFriendsCommand = new MvxAsyncCommand(OpenFacebookFriendsAsync);
            OpenBlockedUsersCommand = new MvxAsyncCommand(OpenBlockedUsersAsync);
            OpenUpdatesCommand = new MvxAsyncCommand(OpenUpdatesAsync);

            //todo: change hardcode
            UpdatesBadgeText = "12";
            int count = 145;
            FacebookFriendsCount = $"{count.ToString()} {Localization.FacebookFriends}";
        }

        private async Task OpenFacebookFriendsAsync()
        {

        }

        private async Task OpenBlockedUsersAsync()
        {

        }

        private async Task OpenUpdatesAsync()
        {

        }

        #endregion
    }
}
