using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.iOS.Effects;
using FrameCornerRadius = XamarinSocial.Effects.FrameCornerRadiusEffect;

[assembly: ExportEffect(typeof(FrameCornerRadiusEffect), nameof(FrameCornerRadiusEffect))]
namespace XamarinSocial.iOS.Effects
{
    public class FrameCornerRadiusEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (Element.Effects.FirstOrDefault(e => e is FrameCornerRadius)) as FrameCornerRadius;
                if (effect != null)
                {
                    Container.Layer.CornerRadius = effect.CornerRadius;
                    Container.Layer.MasksToBounds = true;
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Cannot set property on attached control. Error: {ex.Message}");
            }
        }

        protected override void OnDetached()
        {
        }
    }
}