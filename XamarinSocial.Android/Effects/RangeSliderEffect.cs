using Android.Graphics;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.RangeSlider;

//[assembly: ResolutionGroupName("EffectsSlider")]
[assembly: ResolutionGroupName("XamarinSocial")]
[assembly: ExportEffect(typeof(XamarinSocial.Droid.Effects.RangeSliderEffect), nameof(XamarinSocial.Droid.Effects.RangeSliderEffect))]
namespace XamarinSocial.Droid.Effects
{
    public class RangeSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var ctrl = (RangeSliderControl)Control;
            
            var activeColor = Xamarin.Forms.Color.FromHex("#0084F4").ToAndroid();
            var defaultColor = Xamarin.Forms.Color.FromHex("#D5E6F4").ToAndroid();
            ctrl.ActiveColor = activeColor;
            ctrl.DefaultColor = defaultColor;

            var context = Xamarin.Forms.Forms.Context;
            var thumbImage = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.slider_icon);
            ctrl.ThumbImage = thumbImage;
            ctrl.ThumbPressedImage = thumbImage;
        }

        protected override void OnDetached()
        {
        }

        private static Bitmap ConvertToBitmap(Drawable drawable, int widthPixels, int heightPixels)
        {
            var mutableBitmap = Bitmap.CreateBitmap(widthPixels, heightPixels, Bitmap.Config.Argb8888);
            var canvas = new Canvas(mutableBitmap);
            drawable.SetBounds(0, 0, widthPixels, heightPixels);
            drawable.Draw(canvas);
            return mutableBitmap;
        }
    }
}