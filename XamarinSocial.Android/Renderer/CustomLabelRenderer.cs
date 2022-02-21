using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinSocial.Droid.Renderer;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace XamarinSocial.Droid.Renderer
{
    /// <summary>
    /// Fixes blinking cursor issue on label
    /// </summary>
    public class CustomLabelRenderer : Xamarin.Forms.Platform.Android.FastRenderers.LabelRenderer
    {
        public CustomLabelRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            Control.HorizontalScrollBarEnabled = false;
            Control.VerticalScrollBarEnabled = false;
        }
    }
}