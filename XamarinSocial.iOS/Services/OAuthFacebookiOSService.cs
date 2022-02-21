using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Auth;
using XamarinSocial.Constants;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.iOS.Services
{
    public class OAuthFacebookiOSService : IOAuthFacebookService
    {
        #region Properties

        private readonly IOAuthAuthenticatorFaceBookService _oAuthAuthenticatorFaceBookService;
        private TaskCompletionSource<LoginResult> _completionSource;
        private UIViewController _viewController;

        #endregion Properties

        public OAuthFacebookiOSService(IOAuthAuthenticatorFaceBookService oAuthAuthenticatorFaceBookService)
        {
            _viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            _oAuthAuthenticatorFaceBookService = oAuthAuthenticatorFaceBookService;
        }

        #region Methods

        public Task<LoginResult> Login()
        {
            _completionSource = new TaskCompletionSource<LoginResult>();

            _oAuthAuthenticatorFaceBookService.CreateInstance();
            _oAuthAuthenticatorFaceBookService.AuthenticatorInstance.Completed += AuthOnCompleted;

            var ui = _oAuthAuthenticatorFaceBookService.AuthenticatorInstance.GetUI();
            while (_viewController.PresentedViewController != null)
            {
                _viewController = _viewController.PresentedViewController;
            }
            _viewController.PresentViewController(ui, true, null);

            return _completionSource.Task;
        }

        public void Logout()
        {
            _completionSource = null;
        }

        private async void AuthOnCompleted(object sender, AuthenticatorCompletedEventArgs authCompletedArgs)
        {
            if (!authCompletedArgs.IsAuthenticated || authCompletedArgs.Account is null)
            {
                SetResult(new LoginResult { LoginState = LoginState.Canceled });
                return;
            }

            string token = null;
            if (authCompletedArgs.Account.Properties.ContainsKey(Constant.AuthConfig.FacebookKeyForToken))
            {
                token = authCompletedArgs.Account.Properties[Constant.AuthConfig.FacebookKeyForToken];
            }

            await GetUserProfile(authCompletedArgs.Account, token);
            _viewController.DismissViewController(true, null);
        }

        private void SetResult(LoginResult loginResult)
        {
            _oAuthAuthenticatorFaceBookService.AuthenticatorInstance.Completed -= AuthOnCompleted;
            _completionSource?.TrySetResult(loginResult);
            _completionSource = null;
        }

        private async Task GetUserProfile(Account account, string token)
        {
            var result = new LoginResult { Token = token };

            var request = new OAuth2Request
            (
                Constant.AuthConfig.FacebookGetMethod,
                new Uri(Constant.AuthConfig.FacebookFields),
                null,
                account
            );

            using (var response = await request.GetResponseAsync())
            {
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var user = response.GetResponseText();
                    var userObject = JObject.Parse(user);
                    result.LoginState = LoginState.Success;
                    result.FirstName = userObject["first_name"]?.ToString();
                    result.LastName = userObject["last_name"]?.ToString();
                    result.UserId = userObject["id"]?.ToString();
                    result.Email = userObject["email"]?.ToString();
                }
                else
                {
                    result.LoginState = LoginState.Failed;
                    result.ErrorString = $"Error: Response={response}, StatusCode={response?.StatusCode}";
                }

                SetResult(result);
            }
        }

        #endregion Methods
    }
}