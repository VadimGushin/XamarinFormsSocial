using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using XamarinSocial.Constants;

namespace XamarinSocial.ViewModels.SignUp
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        public ResetPasswordViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {

            SetupUI();
        }

        #region Properties

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);

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

        private bool _isSecondViewVisible;
        public bool IsSecondViewVisible
        {
            get => _isSecondViewVisible;
            set => SetProperty(ref _isSecondViewVisible, value);
        } 
        
        private bool _isFirstViewVisible;
        public bool IsFirstViewVisible
        {
            get => _isFirstViewVisible;
            set => SetProperty(ref _isFirstViewVisible, value);
        }

        #endregion

        #region Command
        public IMvxAsyncCommand SendInstructionsCommandAsync => new MvxAsyncCommand(SendInstructonsAsync);
        public IMvxAsyncCommand SetNewPasswordCommandAsync => new MvxAsyncCommand(SetNewPasswordAsync);
        public IMvxCommand CheckEmailCommand => new MvxCommand(ValidateEmail);
        public IMvxCommand CheckPasswordCommand => new MvxCommand(ValidatePassword);
        public IMvxCommand CheckPasswordConfirmCommand => new MvxCommand(ValidatePasswordConfirm);
        #endregion

        #region Methods

        private async Task SendInstructonsAsync()
        {
            ValidateEmail();
            if (IsValidEmail)
            {
                IsFirstViewVisible = false;
                IsSecondViewVisible = true;
               //TODO Send Instructions in async mode?
            }
        }

        private async Task SetNewPasswordAsync()
        {
            ValidatePassword();
            ValidatePasswordConfirm();
            if (IsValidPassword && IsValidConfirmPassword)
            {
                //TODO Send new password to API
                await NavigationService.Navigate<SignUpViewModel>();
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

        private void SetupUI()
        {
            IsFirstViewVisible = true;
            IsSecondViewVisible = true;
            IsValidEmail = true;
            IsValidConfirmPassword = true;
            IsValidPassword = true;
        }

        #endregion
    }
}
