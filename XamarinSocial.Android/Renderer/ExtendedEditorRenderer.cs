using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinSocial.CustomElements;
using XamarinSocial.Droid.Renderer;

[assembly: ExportRenderer(typeof(Editor), typeof(ExtendedEditorRenderer))]
namespace XamarinSocial.Droid.Renderer
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        public ExtendedEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.Background = gd;
                Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}