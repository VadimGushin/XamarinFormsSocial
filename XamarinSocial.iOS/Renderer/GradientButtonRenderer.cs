using CoreAnimation;
using CoreGraphics;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.CustomElements;
using XamarinSocial.iOS.Renderer;

[assembly: ExportRenderer(typeof(GradientButton), typeof(GradientButtonRenderer))]
namespace XamarinSocial.iOS.Renderer
{
    public class GradientButtonRenderer : ButtonRenderer
    {
        #region overrides
        /// <summary>
        /// Draw the gradient background button
        /// </summary>
        /// <param name="rect"></param>
        /// 

        protected override void OnElementChanged(ElementChangedEventArgs<Button> elem)
        {
            base.OnElementChanged(elem);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == GradientButton.StartColorProperty
           .PropertyName || e.PropertyName == GradientButton.EndColorProperty
           .PropertyName)
            {
                SetNeedsDisplay();
            }
        }

        public override void Draw(CGRect rect)
         {  
            base.Draw(rect);

            if (Element != null)
            {
                if (Element is GradientButton)
                {
                    var gradientLayer = new CAGradientLayer();
                    var button = Element as GradientButton;
                    gradientLayer.Frame = rect;
                    gradientLayer.Colors = new CGColor[] {
                        button.StartColor.ToCGColor(),
                        button.EndColor.ToCGColor()
                    };

                    // horizontal gradient
                    if (button.GradientOrientation == GradientButton.GradientOrientationStates.Horizontal)
                    {
                        gradientLayer.StartPoint = new CGPoint(0.0, 0.5);
                        gradientLayer.EndPoint = new CGPoint(1.0, 0.5);
                    }
                    // vertical gradient
                    else if (button.GradientOrientation == GradientButton.GradientOrientationStates.Vertical)
                    {
                        gradientLayer.StartPoint = new CGPoint(0.5, 0.0);
                        gradientLayer.EndPoint = new CGPoint(0.5, 1.0);
                    }


                    gradientLayer.CornerRadius = 3*button.CornerRadius;

                    NativeView.Layer.InsertSublayerBelow(gradientLayer, Layer.Sublayers.LastOrDefault());
                    Layer.MasksToBounds = true;
                }
            }
        }
        #endregion
    }
}