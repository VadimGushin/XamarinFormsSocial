using System;
using Xamarin.Auth;
using Xamarin.Forms;
using XamarinSocial.Constants;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class OAuth2AuthenticatorGoogleService : IOAuth2AuthenticatorGoogleService
    {
        private  OAuth2Authenticator _oAuth2Authenticator;
        private string _clientId;
        public OAuth2AuthenticatorGoogleService()
        {
        }
        
        public OAuth2Authenticator AuthenticatorInstance => _oAuth2Authenticator;

        public void CreateInstans()
        {
            _clientId = (Device.RuntimePlatform == Device.Android) ? Constant.AuthConfig.GoogleAndroidClientId : Constant.AuthConfig.GoogleIOSClientId;

            _oAuth2Authenticator = new OAuth2Authenticator(
                _clientId,
                String.Empty,
                Constant.AuthConfig.GoogleScope,
                new Uri(Constant.AuthConfig.GoogleAuthorizeUrl),
                new Uri(Constant.AuthConfig.GoogleRedirectUrl),
                new Uri(Constant.AuthConfig.GoogleKeyForToken),
                null,
                Constant.AuthConfig.GoogleIsUsingNativeUI);

            _oAuth2Authenticator.AllowCancel = true;
            _oAuth2Authenticator.ClearCookiesBeforeLogin = true;
            _oAuth2Authenticator.Title = "Google"; 
        }
    }
}
