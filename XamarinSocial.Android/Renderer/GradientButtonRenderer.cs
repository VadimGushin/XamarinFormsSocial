using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinSocial.CustomElements;
using XamarinSocial.Droid.Renderer;

[assembly: ExportRenderer(typeof(GradientButton), typeof(GradientButtonRenderer))]
namespace XamarinSocial.Droid.Renderer
{
    public class GradientButtonRenderer : ButtonRenderer
    {
        #region instances 
        GradientDrawable _gradient;
        #endregion

        #region constructor
        public GradientButtonRenderer(Context context) : base(context) { }
        #endregion

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var button = this.Control; 
            button.SetAllCaps(false);

            if (e.PropertyName == GradientButton.StartColorProperty
            .PropertyName || e.PropertyName == GradientButton.EndColorProperty
            .PropertyName)
            {
                Control.SetBackground(DrawGradient(sender as GradientButton));
            }
        }

        #region overridable
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {


            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                Control.Touch -= ButtonTouched;
            }

            if (Control != null)
            {
                try
                {
                    Control.Touch += ButtonTouched;
                    Control.StateListAnimator = new Android.Animation.StateListAnimator();
                    var button = e.NewElement as GradientButton;
                    Control.SetBackground(DrawGradient(button));
                }
                catch (Exception ex)
                {
                    // handle exception
                }
            }
        }
        #endregion

        #region EventHandler
        /// <summary>
        /// Draw the gradient with the correct oppacity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTouched(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            if (e.Event.Action == MotionEventActions.Down)
            {
                _gradient.Alpha = 200;
                Control.SetBackground(_gradient);
            }
            else if (e.Event.Action == MotionEventActions.Up)
            {
                _gradient.Alpha = 255;
                Control.SetBackground(_gradient);
            }
        }
        #endregion

        #region privates
        /// <summary>
        /// Create the gradient for the button background
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private GradientDrawable DrawGradient(GradientButton button)
        {

            var orientation = button.GradientOrientation == GradientButton.GradientOrientationStates.Horizontal ?
                GradientDrawable.Orientation.LeftRight : GradientDrawable.Orientation.TopBottom;

            _gradient = new GradientDrawable(orientation, new[] {
                button.StartColor.ToAndroid().ToArgb(),
                button.EndColor.ToAndroid().ToArgb(),
            });

            _gradient.SetCornerRadius(button.CornerRadius * 10);
            _gradient.SetStroke(0, button.StartColor.ToAndroid());

            return _gradient;
        }
        #endregion
    }
}
