namespace XamarinSocial.Constants
{
    public static partial class Constant
    {
        public static class AuthConfig
        {

            #region GoogleAuth
            public const string GoogleAndroidClientId = "138838516294-fai8s657665cbd2ojl3n3edl1f4fvf8t.apps.googleusercontent.com";
            public const string GoogleIOSClientId = "138838516294-mvfcdneq5bd4f40jv81m5ubl0k8nsuei.apps.googleusercontent.com";
            public const string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/v2/auth";
            public const string GoogleRedirectUrl = "com.companyname.xamarinsocial:/oauth2redirect";
            public const string GoogleScope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email";
            public const string GoogleFields = "https://www.googleapis.com/oauth2/v2/userinfo";
            public const string GoogleKeyForToken = "https://www.googleapis.com/oauth2/v4/token";
            public const bool GoogleIsUsingNativeUI = true;
            public const string GoogleGetMethod = "GET";
            #endregion

            #region FacebookAuth

            public const string FacebookClientId = "2706699766255028";
            public const string FacebookAuthorizeUrl = "https://m.facebook.com/dialog/oauth/";
            public const string FacebookRedirectUrl = "https://www.facebook.com/connect/login_success.html";
            public const string FacebookScope = "email";
            public const string FacebookFields = "https://graph.facebook.com/me?fields=email,first_name,last_name";
            public const string FacebookKeyForToken = "access_token";
            public const bool FacebookIsUsingNativeUI = false;
            public const string FacebookGetMethod = "GET";

            #endregion
        }
    }
}
