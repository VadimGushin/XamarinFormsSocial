using Foundation;
using MediaManager;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using System;
using UIKit;
using XamarinSocial.Forms;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.iOS
{
    [Register("AppDelegate")]

    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, CoreApp, App>
    {

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            CrossMediaManager.Current.Init();
            Forms9Patch.iOS.Settings.Initialize(this);
            Rg.Plugins.Popup.Popup.Init();

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            IOAuth2AuthenticatorGoogleService _oAuth2Authenticator = Mvx.IoCProvider.Resolve<IOAuth2AuthenticatorGoogleService>();
            var nativeUri = new Uri(url.AbsoluteString);
            _oAuth2Authenticator.AuthenticatorInstance.OnPageLoading(nativeUri);

            return true;
        }
    }
}
