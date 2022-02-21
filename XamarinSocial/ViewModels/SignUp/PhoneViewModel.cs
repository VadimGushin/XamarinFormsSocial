using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.SignUp
{
    public class PhoneViewModel : BaseViewModel
    {
        public PhoneViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            CountryCode = "+380";
            IsVerifyBySMS = true;
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

        private string _countryCode;
        public string CountryCode
        {
            get => _countryCode;
            set
            {
                SetProperty(ref _countryCode, value);
                PhoneNumberWithCode = CountryCode + PhoneNumber;
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

        private bool _isValidPhoneNumber;
        public bool IsValidPhoneNumber
        {
            get => _isValidPhoneNumber;
            set
            {
                SetProperty(ref _isValidPhoneNumber, value);
            }
        }
        #endregion

        #region Commands
        public IMvxAsyncCommand NavigateToCountryCodeCommandAsync => new MvxAsyncCommand(NavigateToCountryCodeAsync);
        public IMvxAsyncCommand VerifyMeCommandAsync => new MvxAsyncCommand(VerifyMeAsync);
        #endregion

        #region Functions

        private async Task NavigateToCountryCodeAsync()
        {
            var result = await NavigationService.Navigate<CountryCodeViewModel, string, DestructionResult<string>>(string.Empty);
            if (result.Destroyed)
            {
                CountryCode = result.Entity;
            }
        }

        private async Task VerifyMeAsync()
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
        #endregion
    }
}
