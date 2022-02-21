using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.CustomElements;
using XamarinSocial.iOS.Renderer;

[assembly: ExportRenderer(typeof(Editor), typeof(ExtendedEditorRenderer))]
namespace XamarinSocial.iOS.Renderer
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.InputAccessoryView = null;
            }
        }
    }
}