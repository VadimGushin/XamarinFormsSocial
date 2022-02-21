using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.RangeSlider;
using XamarinSocial.iOS.Effects;

//[assembly: ResolutionGroupName("EffectsSlider")]
[assembly: ResolutionGroupName("XamarinSocial")]
[assembly: ExportEffect(typeof(RangeSliderEffect), nameof(RangeSliderEffect))]
namespace XamarinSocial.iOS.Effects
{
    public class RangeSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var ctrl = (RangeSliderControl)Control;

                var activeColor = Xamarin.Forms.Color.FromHex("#0084F4").ToUIColor();
                ctrl.TintColor = activeColor;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot set property on control iOS", e.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}