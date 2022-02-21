using Newtonsoft.Json.Linq;
using Plugin.CurrentActivity;
using System;
using System.Threading.Tasks;
using Xamarin.Auth;
using XamarinSocial.Constants;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Droid.Services
{
    public class OAuthGoogleAndroidService : IOAuthGoogleService
    {
        #region Variables

        private TaskCompletionSource<LoginResult> _completionSource;
        private readonly IOAuth2AuthenticatorGoogleService _oAuth2Authenticator;

        #endregion Variables

        public OAuthGoogleAndroidService(IOAuth2AuthenticatorGoogleService oAuth2Authenticator)
        {
            _oAuth2Authenticator = oAuth2Authenticator;
        }

        #region Methods

        public Task<LoginResult> Login()
        {
            _oAuth2Authenticator.CreateInstans();
            _completionSource = new TaskCompletionSource<LoginResult>();
            _oAuth2Authenticator.AuthenticatorInstance.Completed += AuthOnCompleted;
            var intent = _oAuth2Authenticator.AuthenticatorInstance.GetUI(CrossCurrentActivity.Current.Activity);
            CrossCurrentActivity.Current.Activity.StartActivity(intent);

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
            if (authCompletedArgs.Account.Properties.ContainsKey("access_token"))
            {
                token = authCompletedArgs.Account.Properties["access_token"];
            }

            await GetUserProfile(authCompletedArgs.Account, token);

        }

        private void SetResult(LoginResult loginResult)
        {
            _oAuth2Authenticator.AuthenticatorInstance.Completed -= AuthOnCompleted;
            _completionSource?.TrySetResult(loginResult);
            _completionSource = null;
        }

        private async Task GetUserProfile(Account account, string token)
        {
            var result = new LoginResult { Token = token };

            var request = new OAuth2Request
            (
                Constant.AuthConfig.GoogleGetMethod,
                new Uri(Constant.AuthConfig.GoogleFields),
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
                    result.FirstName = userObject["given_name"]?.ToString();
                    result.LastName = userObject["family_name"]?.ToString();
                    result.UserId = userObject["id"]?.ToString();
                    result.Email = userObject["email"]?.ToString();
                    result.ImageUrl = userObject["picture"]?.ToString();
                    result.Locale = userObject["locale"]?.ToString();
                }
                else
                {
                    result.LoginState = LoginState.Failed;
                    result.ErrorString = $"Error: Response={response}, StatusCode={response?.StatusCode}";
                }

                SetResult(result);
            }
        }

        #endregion
    }
}