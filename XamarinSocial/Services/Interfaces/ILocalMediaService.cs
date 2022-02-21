using Xamarin.Forms;

namespace XamarinSocial.Services.Interfaces
{
    public interface ILocalMediaService
    {
        ImageSource GenerateThumbImage(string url, long usecond);
    }
}
