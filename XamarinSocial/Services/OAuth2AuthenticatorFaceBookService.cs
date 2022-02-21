using System;
using Xamarin.Auth;
using XamarinSocial.Constants;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class OAuth2AuthenticatorFaceBookService : IOAuthAuthenticatorFaceBookService
    {
        private OAuth2Authenticator _oAuth2Authenticator;
        public OAuth2AuthenticatorFaceBookService()
        {

        }
        public OAuth2Authenticator AuthenticatorInstance => _oAuth2Authenticator;

        public void CreateInstance()
        {
            _oAuth2Authenticator = new OAuth2Authenticator(
           Constant.AuthConfig.FacebookClientId,
           Constant.AuthConfig.FacebookScope,
          new Uri(Constant.AuthConfig.FacebookAuthorizeUrl),
          new Uri(Constant.AuthConfig.FacebookRedirectUrl),
          null,
          Constant.AuthConfig.FacebookIsUsingNativeUI);

            _oAuth2Authenticator.AllowCancel = true;
            _oAuth2Authenticator.ClearCookiesBeforeLogin = true;
            _oAuth2Authenticator.Title = "Facebook";
        }
    }

}

