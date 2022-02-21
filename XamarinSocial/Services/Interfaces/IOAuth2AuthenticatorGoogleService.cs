using Xamarin.Auth;

namespace XamarinSocial.Services.Interfaces
{
    public interface IOAuth2AuthenticatorGoogleService
    {
        OAuth2Authenticator AuthenticatorInstance { get; }

        void CreateInstans();
    }
}
