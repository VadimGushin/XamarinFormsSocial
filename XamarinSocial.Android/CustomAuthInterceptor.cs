using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using MvvmCross;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Droid
{
    [Activity(Label = "CustomegAuthInceptor",
        LaunchMode = LaunchMode.SingleTask,
        NoHistory = false)]

    [IntentFilter(
       actions: new[] { Intent.ActionView },
       Categories = new[]
       {
                    Intent.CategoryDefault,
                    Intent.CategoryBrowsable
       },
       DataSchemes = new[]
       {
                // First part of the redirect url (Package name)
                "com.companyname.xamarinsocial"
       },
       DataPaths = new[]
       {
                // Second part of the redirect url (Path)
                "/oauth2redirect"
       })]
    public class CustomeAuthInceptor : Activity
    {
        private readonly IOAuth2AuthenticatorGoogleService _oAuth2Authenticator = Mvx.IoCProvider.Resolve<IOAuth2AuthenticatorGoogleService>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.Net.Uri uri_android = Intent.Data;

            //Intent.Dispose();

            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            System.Uri nativeUri = new System.Uri(uri_android.ToString());

            // Send the URI to the Authenticator for continuation
            _oAuth2Authenticator.AuthenticatorInstance.OnPageLoading(nativeUri);

            var intent = new Intent(this, typeof(RootActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            Finish();
        }
    }
}