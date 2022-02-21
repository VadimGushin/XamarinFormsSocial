using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinSocial.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Services

        private readonly IUserDialogs _userDialogs;

        #endregion

        public SettingsViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
                                : base(navigationService)
        {
            InitControls();
            _userDialogs = userDialogs;
        }

        #region Properties

        #endregion

        #region Commnads

        public IMvxCommand OpenProfileInfoCommand { get; private set; }
        public IMvxCommand OpenSocialNetworkCommand { get; private set; }
        public IMvxCommand ChangePasswordCommand { get; private set; }
        public IMvxCommand OpenNotificationsCommand { get; private set; }
        public IMvxCommand RateAppCommand { get; private set; }
        public IMvxCommand SendFeedbackCommand { get; private set; }
        public IMvxCommand OpenPrivacyPolicyCommand { get; private set; }

        #endregion

        #region Overrides

        #endregion

        #region Private Methods

        private void InitControls()
        {
            OpenProfileInfoCommand = new MvxAsyncCommand(OpenProfileInfoAsync);
            OpenSocialNetworkCommand = new MvxAsyncCommand(OpenSocialNetworkAsync);
            ChangePasswordCommand = new MvxAsyncCommand(ChangePasswordAsync);
            OpenNotificationsCommand = new MvxAsyncCommand(OpenNotificationsAsync);
            RateAppCommand = new MvxAsyncCommand(OnRateAppAsync);
            SendFeedbackCommand = new MvxAsyncCommand(SendFeedbackAsync);
            OpenPrivacyPolicyCommand = new MvxAsyncCommand(OpenPrivacyPolicyAsync);
        }

        private async Task OpenProfileInfoAsync()
        {
            _userDialogs.Toast("Open Profile Info Clicked");
        }

        private async Task OpenSocialNetworkAsync()
        {
            await NavigationService.Navigate<SocialNetworkSettingsViewModel>();
        }

        private async Task ChangePasswordAsync()
        {
            _userDialogs.Toast("Change Password Clicked");
        }

        private async Task OpenNotificationsAsync()
        {
            await NavigationService.Navigate<NotificationSettingsViewModel>();
        }

        private async Task OnRateAppAsync()
        {
            _userDialogs.Toast("Rate App Clicked");
        }

        private async Task SendFeedbackAsync()
        {
            _userDialogs.Toast("Send Feedback Clicked");
        }

        private async Task OpenPrivacyPolicyAsync()
        {
            _userDialogs.Toast("Open Privacy Policy Clicked");
        }

        #endregion
    }
}
