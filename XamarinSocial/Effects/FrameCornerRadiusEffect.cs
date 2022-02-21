using Xamarin.Forms;

namespace XamarinSocial.Effects
{
    public class FrameCornerRadiusEffect : RoutingEffect
    {
        public FrameCornerRadiusEffect()
          : base($"XamarinSocial.{nameof(FrameCornerRadiusEffect)}")
        {
        }

        public int CornerRadius { get; set; }
    }
}
