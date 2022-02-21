using Android.Views;
using System;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Droid.Services
{
    public class LocationFetcherService : ILocationFetcherService
    {
        public PointF GetCoordinates(VisualElement element)
        {
            try
            {
                IVisualElementRenderer renderer = Platform.GetRenderer(element);

                float screenCoordinateX = renderer.View.GetX();
                float screenCoordinateY = renderer.View.GetY();

                IViewParent viewParent = renderer.View.Parent;

                while (!(viewParent is null) && viewParent is ViewGroup group)
                {
                    screenCoordinateX += group.GetX();
                    screenCoordinateY += group.GetY();
                    viewParent = group.Parent;
                }

                var point = new PointF(screenCoordinateX, screenCoordinateY);
                return point;
            }
            catch(Exception ex)
            {
                string message = ex.ToString();
                return PointF.Empty;
            }
        }
    }
}