using Plugin.CurrentActivity;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Droid.Services
{
    public class CloseApplicationService : ICloseApplicationService
    {
        public void CloseApplication()
        {
            var activity = CrossCurrentActivity.Current;
            activity.Activity.FinishAffinity();
        }
    }
}