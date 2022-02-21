using System.IO;
using Android.Graphics;
using Android.Media;
using Xamarin.Forms;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Droid.Services
{
    public class LocalMediaService : ILocalMediaService
    {
        public ImageSource GenerateThumbImage(string url, long usecond)
        {
            MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            retriever.SetDataSource(url);
            Bitmap bitmap = retriever.GetFrameAtTime(usecond);
            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();
                return ImageSource.FromStream(() => new MemoryStream(bitmapData));
            }
            return null;
        }
    }
}