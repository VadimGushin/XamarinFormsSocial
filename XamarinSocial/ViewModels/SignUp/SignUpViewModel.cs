using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModels.ProfileSet;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.SignUp
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IOAuthGoogleService _oAuthGoogleService;
        private readonly IOAuthFacebookService _oAuthFacebookService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly ICloseApplicationService _closeApplicationService;

        public SignUpViewModel(IMvxNavigationService navigationService, IOAuthGoogleService oAuthGoogleService,
            IOAuthFacebookService oAuthFacebookService, ISecureStorageService secureStorageService, 
            ICloseApplicationService closeApplicationService) : base(navigationService)
        {
            _oAuthGoogleService = oAuthGoogleService;
            _oAuthFacebookService = oAuthFacebookService;
            CountryCode = "+380";
            IsVerifyBySMS = true;
            _secureStorageService = secureStorageService;
            _closeApplicationService = closeApplicationService;
        }

        #region Properties

        private string _phoneNumberWithCode;
        public string PhoneNumberWithCode
        {
            get => _phoneNumberWithCode;
            set
            {
                SetProperty(ref _phoneNumberWithCode, value);
            }
        }

        private string _countryCode;
        public string CountryCode
        {
            get => _countryCode;
            set
            {

                SetProperty(ref _countryCode, value);
                PhoneNumberWithCode = $" {CountryCode}{PhoneNumber}";
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                SetProperty(ref _phoneNumber, value);
                IsValidPhoneNumber = IsPhoneNumber(_phoneNumber);
                PhoneNumberWithCode = CountryCode + PhoneNumber;
            }
        }

        private bool _isValidPhoneNumber;
        public bool IsValidPhoneNumber
        {
            get => _isValidPhoneNumber;
            set
            {
                SetProperty(ref _isValidPhoneNumber, value);
            }
        }

        private bool _isVerifyBySMS;

        public bool IsVerifyBySMS
        {
            get => _isVerifyBySMS;
            set
            {
                SetProperty(ref _isVerifyBySMS, value);
            }
        }

        private bool _isVerifyByPhoneCall;

        public bool IsVerifyByPhoneCall
        {
            get => _isVerifyByPhoneCall;
            set
            {
                SetProperty(ref _isVerifyByPhoneCall, value);
            }
        }

        #endregion Properties

        #region Commands

        public IMvxAsyncCommand NavigateToCountryCodeCommandAsync => new MvxAsyncCommand(NavigateToCountryCodeAsync);
        public IMvxAsyncCommand GetStartedCommandAsync => new MvxAsyncCommand(GetStartedAsync);
        public IMvxAsyncCommand EmailSignInCommandAsync => new MvxAsyncCommand(EmailSignInAsync);
        public IMvxAsyncCommand FacebookSignInCommandAsync => new MvxAsyncCommand(FacebookSignInAsync);
        public IMvxAsyncCommand GoogleSignInCommandAsync => new MvxAsyncCommand(GoogleSignInAsync);
        public IMvxAsyncCommand ResetPasswordCommandAsync => new MvxAsyncCommand(ResetPasswordAsync);
        public IMvxCommand CloseApplicationCommand => new MvxCommand(CloseApplication);

        #endregion Commands

        #region Functions

        private void CloseApplication()
        {
            _closeApplicationService.CloseApplication();
        }

        private async Task NavigateToCountryCodeAsync()
        {
            var result = await NavigationService.Navigate<CountryCodeViewModel, string, DestructionResult<string>>(string.Empty);
            if (result != null && result.Destroyed)
            {
                CountryCode = result.Entity;
            }
        }

        private async Task EmailSignInAsync()
        {
            await NavigationService.Navigate<EmailSignInViewModel>();
        } 
        
        private async Task ResetPasswordAsync()
        {
            await NavigationService.Navigate<ResetPasswordViewModel>();
        }

        private async Task GoogleSignInAsync()
        {
            LoginResult result = await _oAuthGoogleService.Login();
            if (result == null || !string.IsNullOrEmpty(result.ErrorString) || string.IsNullOrEmpty(result.Email))
            {
                //TODO some error
                return;
            }
            await AddProfileToSecureStorage(result);

            await RedirectToUserProfileViewModel(result);
        }

        private async Task FacebookSignInAsync()
        {
            LoginResult result = await _oAuthFacebookService.Login();
            if (result == null || !string.IsNullOrEmpty(result.ErrorString) || string.IsNullOrEmpty(result.Email))
            {
                //TODO some error
                return;
            }
            await AddProfileToSecureStorage(result);

            await RedirectToUserProfileViewModel(result);
        }

        private async Task AddProfileToSecureStorage(LoginResult user)
        {
            await _secureStorageService.AddString(Constant.StorageConfig.SequreKeyForLoged, Constant.StorageConfig.SecureValueLogin);
            await _secureStorageService.AddString(Constant.StorageConfig.UserFirstName, user.FirstName);
            await _secureStorageService.AddString(Constant.StorageConfig.UserLastName, user.LastName);
            await _secureStorageService.AddString(Constant.StorageConfig.UserEmail, user.Email);
        }

        private async Task RedirectToUserProfileViewModel(LoginResult authResult)
        {
            var authProfile = new AuthProviderProfile
            {
                FirstName = authResult?.FirstName,
                LastName = authResult?.LastName,
                Email = authResult?.Email
            };
            //TODO Send info to API, Sasve to localStorage, add two factor authoruze
            //TODO: remove hardcode data
            await _secureStorageService.AddString(Constant.StorageConfig.UserProfileImageSource, "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg");
            await NavigationService.Navigate<UserProfileViewModel, AuthProviderProfile>(authProfile);
        }

        private async Task GetStartedAsync()
        {
            if (IsVerifyBySMS)
            {
                await NavigationService.Navigate<CodeViewModel, string>(PhoneNumberWithCode);
            }
            if (IsVerifyByPhoneCall)
            {
                await NavigationService.Navigate<LandLineViewModel, string>(PhoneNumberWithCode);
            }
        }

        private bool IsPhoneNumber(string number)
        {
            Regex phoneNumpattern = new Regex(@"(^\d{3}?-? *\d{3}-? *-?\d{3,4}$)");
            var result = phoneNumpattern.IsMatch(number);
            return result;
        }

        #endregion Functions
    }
}
