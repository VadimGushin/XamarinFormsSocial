using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSocial.Constants;

namespace XamarinSocial.ViewModels.SignUp
{
    public class EmailSignInViewModel : BaseViewModel
    {
        public EmailSignInViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            IsValidEmail = true;
            IsValidPassword = true;
        }

        #region Properties
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
            }
        }

        private bool _isValidEmail;
        public bool IsValidEmail
        {
            get => _isValidEmail;
            set
            {
                SetProperty(ref _isValidEmail, value);
            }
        }

        private string _emailError;
        public string EmailError
        {
            get => _emailError;
            set
            {
                SetProperty(ref _emailError, value);
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

        private bool _isValidPassword;
        public bool IsValidPassword
        {
            get => _isValidPassword;
            set
            {
                SetProperty(ref _isValidPassword, value);
            }
        }

        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set
            {
                SetProperty(ref _passwordError, value);
            }
        }

        #endregion

        #region Commands
        public IMvxAsyncCommand NavigateToEmailSignUpCommandAsync => new MvxAsyncCommand(NavigateToEmailSignUpAsync);
        public IMvxAsyncCommand SignInCommandAsync => new MvxAsyncCommand(SignInAsync);
        public IMvxCommand CheckEmailCommand => new MvxCommand(ValidateEmail);
        public IMvxCommand CheckPasswordCommand => new MvxCommand(ValidatePassword);
        #endregion Commands

        #region Functions
        private async Task SignInAsync() {
            if (IsValidEmail && IsValidPassword) 
            {
                await NavigationService.Navigate<SignUpViewModel>();
            }
        }
        private void ValidateEmail()
        {
            if (!CheckEmail())
            {
                EmailError = "Email wrong";
                IsValidEmail = false;
                return;
            }
            if (CheckEmail())
            {
                EmailError = "";
                IsValidEmail = true;
            }
        }

        private void ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < Constant.Numeric.PasswordLengthMin)
            {
                IsValidPassword = false;
                PasswordError = "Password length must be more than 6 characters";
                return;
            }
            if (Password.Length > Constant.Numeric.PasswordLengthMax)
            {
                IsValidPassword = false;
                PasswordError = "Password length must be less than 16 characters";
                return;
            }

            if (Password.Length >= Constant.Numeric.PasswordLengthMin)
            {
                IsValidPassword = true;
                PasswordError = "";
            }
        }

        private async Task NavigateToEmailSignUpAsync()
        {
            await NavigationService.Navigate<EmailSignUpViewModel> ();
        }
        private bool CheckEmail()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(Email);
                return addr.Address == Email;
            }
            catch
            {
                return false;
            }
        }
        #endregion Functions
    }
}
