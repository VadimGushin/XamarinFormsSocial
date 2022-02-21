using Android.App;
using Android.Widget;
using MvvmCross;
using MvvmCross.Platforms.Android;
using System.Threading.Tasks;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Droid.Services
{
    public class DisplayAlertService : IDisplayAlertService
    {

        public void ShowToast(string message)
        {
            var activity = GetCurrentActivity();
            activity.RunOnUiThread(() =>
            {
                Toast.MakeText(activity, message, ToastLength.Long).Show();
            });
        }

        private Activity GetCurrentActivity()
        {
            var top = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
            return top.Activity;
        }

    }
}