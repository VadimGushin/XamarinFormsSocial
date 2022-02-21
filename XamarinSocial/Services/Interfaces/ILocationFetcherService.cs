using System.Drawing;
using Xamarin.Forms;

namespace XamarinSocial.Services.Interfaces
{
    public interface ILocationFetcherService
    {
        PointF GetCoordinates(VisualElement element);
    }
}
