using Foundation;
using UIKit;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.iOS.Services
{
    public class DisplayAlertService : IDisplayAlertService
    {
        public void ShowToast(string message)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var alert = new UIAlertView()
                {
                    Message = message,
                    Alpha = 1.0f
                };
                alert.Frame = new CoreGraphics.CGRect(alert.Frame.X, UIScreen.MainScreen.Bounds.Y * 0.7f, alert.Frame.Width, alert.Frame.Height);
                NSTimer tmr;
                alert.Show();

                tmr = NSTimer.CreateTimer(1.5, delegate
                {
                    alert.DismissWithClickedButtonIndex(0, true);
                    alert = null;
                });
                NSRunLoop.Main.AddTimer(tmr, NSRunLoopMode.Common);
            });
        }
    }


}