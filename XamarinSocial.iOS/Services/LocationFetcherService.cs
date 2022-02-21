using System;
using System.Drawing;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.iOS.Services
{
    public class LocationFetcherService : ILocationFetcherService
    {
        public PointF GetCoordinates(VisualElement element)
        {
            if (element == null)
            {
                return PointF.Empty;
            }

            IVisualElementRenderer renderer = Platform.GetRenderer(element);
            UIView nativeView = renderer.NativeView;

            CGPoint rect = nativeView.Superview.ConvertPointToView(nativeView.Frame.Location, null);
            var point = new PointF((int)Math.Round(rect.X), (int)Math.Round(rect.Y));

            return point;
        }
    }
}