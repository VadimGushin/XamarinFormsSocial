using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.Interfaces;

namespace XamarinSocial.iOS.Renderer.Base
{
    public class KeyboardResizedPageRenderer : PageRenderer
    {
        #region Variables

        protected NSObject _observerHideKeyboard;
        protected NSObject _observerShowKeyboard;
        protected CGRect _startFrame;
        protected CGRect _endFrame;

        #endregion


        #region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var editInterestsPage = Element as IKeyboardResizedContentPage;

            editInterestsPage.EntryUnfocused += EntryView_Unfocused;

            //If needed to change XamarinForms gesture recognizer behavior, set CancelsTouchesInView to false
            if (editInterestsPage != null && !editInterestsPage.CancelsTouchesInView)
            {
                foreach (var gestureRecognizer in View.GestureRecognizers)
                {
                    gestureRecognizer.CancelsTouchesInView = false;
                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //_observerHideKeyboard = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
            _observerShowKeyboard = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            //NSNotificationCenter.DefaultCenter.RemoveObserver(_observerHideKeyboard);
            NSNotificationCenter.DefaultCenter.RemoveObserver(_observerShowKeyboard);
        }
        #endregion

        #region Protected Handlers

        protected void OnKeyboardNotification(NSNotification notification)
        {
            if (!IsViewLoaded)
            {
                return;
            }

            //var frameBegin = UIKeyboard.FrameBeginFromNotification(notification);
            //var frameEnd = UIKeyboard.FrameEndFromNotification(notification);

            _startFrame = UIKeyboard.FrameBeginFromNotification(notification);
            _endFrame = UIKeyboard.FrameEndFromNotification(notification);

            var page = Element as ContentPage;
            if (page != null && !(page.Content is ScrollView))
            {
                var padding = page.Padding;
                page.Padding = new Thickness(padding.Left, padding.Top, padding.Right, padding.Bottom + _startFrame.Top - _endFrame.Top);
            }
        }

        protected async void EntryView_Unfocused(object sender, System.EventArgs e)
        {
            //without delay AddButton click doesn't work (simultaneously with keyboard event)
            await Task.Delay(200);
            var page = Element as ContentPage;
            if (page != null && !(page.Content is ScrollView))
            {
                var padding = page.Padding;
                page.Padding = new Thickness(padding.Left, padding.Top, padding.Right, padding.Bottom + _endFrame.Top - _startFrame.Top);
            }
        }

        #endregion
    }
}