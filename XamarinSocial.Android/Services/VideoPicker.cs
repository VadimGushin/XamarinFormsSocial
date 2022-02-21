using Android.Content;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XamarinSocial.VideoHelper.Interfaces;
using XamarinSocial.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinSocial.Droid.Services.VideoPicker))]

namespace XamarinSocial.Droid.Services
{
    public class VideoPicker : IVideoPicker
    {
        public Task<string> GetVideoFileAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("video/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            RootActivity activity = RootActivity.Current;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Video"),
                RootActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickImageTaskCompletionSource = new TaskCompletionSource<string>();

            // Return Task object
            return activity.PickImageTaskCompletionSource.Task;
        }
    }
}