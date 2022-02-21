using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.iOS.Services
{
    public class LocalMediaService : ILocalMediaService
    {
        public ImageSource GenerateThumbImage(string url, long usecond)
        {
            var imageGenerator = new AVAssetImageGenerator(AVAsset.FromUrl((new Foundation.NSUrl(url))));
            imageGenerator.AppliesPreferredTrackTransform = true;
            CMTime actualTime;
            NSError error;
            CGImage cgImage = imageGenerator.CopyCGImageAtTime(new CMTime(usecond, 1000000), out actualTime, out error);
            return ImageSource.FromStream(() => new UIImage(cgImage).AsPNG().AsStream());
        }
    }
}