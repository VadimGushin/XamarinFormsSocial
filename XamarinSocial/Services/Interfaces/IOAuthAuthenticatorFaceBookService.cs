using Xamarin.Auth;

namespace XamarinSocial.Services.Interfaces
{
    public interface IOAuthAuthenticatorFaceBookService
    {
        OAuth2Authenticator AuthenticatorInstance { get; }

        void CreateInstance();
    }
}
