using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MediaManager;
using MvvmCross.Forms.Platforms.Android.Views;
using Plugin.CurrentActivity;
using System;
using System.Threading.Tasks;

namespace XamarinSocial.Droid
{
    [Activity(
        Label = "XamarinSocial",
        Theme = "@style/MainTheme",
        NoHistory = false,
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]

    public class RootActivity : MvxFormsAppCompatActivity
    {
        public static readonly int PickImageId = 1000;
        public static RootActivity Current { private set; get; }
        public TaskCompletionSource<string> PickImageTaskCompletionSource { set; get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Current = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            //Window.AddFlags(WindowManagerFlags.Fullscreen);
            //Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            global::Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            CrossMediaManager.Current.Init(this);
            CrossMediaManager.Current.AutoPlay = true;


            Forms9Patch.Droid.Settings.Initialize(this);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message = e.ExceptionObject.ToString();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
        public override void InitializeForms(Bundle bundle)
        {

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
        //Video Picker native Forms


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    // Set the filename as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(data.DataString);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}