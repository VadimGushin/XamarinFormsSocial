using Plugin.Media.Abstractions;
using Xamarin.Forms;
using XamarinSocial.Enums;

namespace XamarinSocial.Models.Common
{
    public class LocalMediaModel
    {
        public ImageSource ImageSource { get; set; }

        public MediaFile MediaFile { get; set; }

        public ContentType ContentType { get; set; }
    }
}
