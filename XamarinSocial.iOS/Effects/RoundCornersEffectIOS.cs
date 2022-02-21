using System;
using System.ComponentModel;
using System.Diagnostics;
using CoreAnimation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.Effects;
using XamarinSocial.iOS.Effects;

[assembly: ExportEffect(typeof(RoundCornersEffectIOS), nameof(RoundCornersEffect))]
namespace XamarinSocial.iOS.Effects
{
    public class RoundCornersEffectIOS : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                PrepareContainer();
                SetCornerRadius();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected override void OnDetached()
        {
            try
            {
                Container.Layer.CornerRadius = new nfloat(0);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == RoundCornersEffect.CornerRadiusProperty.PropertyName)
            {
                SetCornerRadius();
            }

        }

        private void PrepareContainer()
        {
            Container.ClipsToBounds = true;
            Container.Layer.AllowsEdgeAntialiasing = true;
            Container.Layer.EdgeAntialiasingMask = CAEdgeAntialiasingMask.All;
        }

        private void SetCornerRadius()
        {
            var cornerRadius = RoundCornersEffect.GetCornerRadius(Element);
            Container.Layer.CornerRadius = new nfloat(cornerRadius);
        }
    }
}