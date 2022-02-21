using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using XamarinSocial.Constants;

namespace XamarinSocial.ViewModels.SignUp
{
    public class EmailSignUpViewModel : BaseViewModel
    {
        public EmailSignUpViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            IsValidConfirmPassword = true;
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

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

        private string _passwordConfirm;

        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set
            {
                SetProperty(ref _passwordConfirm, value);
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

        private bool _isValidPassword;
        public bool IsValidPassword
        {
            get => _isValidPassword;
            set
            {
                SetProperty(ref _isValidPassword, value);
            }
        }

        private bool _isValidConfirmPassword;
        public bool IsValidConfirmPassword
        {
            get => _isValidConfirmPassword;
            set
            {
                SetProperty(ref _isValidConfirmPassword, value);
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

        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set
            {
                SetProperty(ref _passwordError, value);
            }
        }

        private string _passwordConfirmError;
        public string PasswordConfirmError
        {
            get => _passwordConfirmError;
            set
            {
                SetProperty(ref _passwordConfirmError, value);
            }
        }
        #endregion Properties

        #region Commands
        public IMvxAsyncCommand NavigateToSignInViewCommandAsync => new MvxAsyncCommand(NavigateToSignUpViewAsync);
        public IMvxAsyncCommand SignInCommandAsync => new MvxAsyncCommand(CheckValidEmailAndPassword);
        public IMvxCommand CheckEmailCommand => new MvxCommand(ValidateEmail);
        public IMvxCommand CheckPasswordCommand => new MvxCommand(ValidatePassword);
        public IMvxCommand CheckPasswordConfirmCommand => new MvxCommand(ValidatePasswordConfirm);

        #endregion Commands

        #region Functions

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
            if (String.IsNullOrWhiteSpace(Password) || Password.Length < Constant.Numeric.PasswordLengthMin)
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

        private void ValidatePasswordConfirm()
        {
            if (string.IsNullOrWhiteSpace(PasswordConfirm))
            {
                IsValidConfirmPassword = false;
                PasswordConfirmError = "Password length must be more than 6 characters";
                return;
            }
            if (Password != PasswordConfirm)
            {
                IsValidConfirmPassword = false;
                PasswordConfirmError = "Password mismatch";
                return;
            }
            if (Password == PasswordConfirm)
            {
                IsValidConfirmPassword = true;
                PasswordConfirmError = "";
            }
        }

        private async Task NavigateToSignUpViewAsync()
        {
            await NavigationService.Navigate<SignUpViewModel>();
        }

        private async Task CheckValidEmailAndPassword()
        {
            ValidateEmail();
            ValidatePassword();
            ValidatePasswordConfirm();
            if (IsValidEmail && IsValidPassword && IsValidConfirmPassword)
            {
                await NavigationService.Navigate<PhoneViewModel>();
            }
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
