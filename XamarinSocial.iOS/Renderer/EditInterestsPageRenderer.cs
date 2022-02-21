using MyProject.iOS.Renderers;
using Xamarin.Forms;
using XamarinSocial.iOS.Renderer.Base;
using XamarinSocial.Views.ProfileSet;

[assembly: ExportRenderer(typeof(EditInterestsPage), typeof(EditInterestsPageRenderer))]

namespace MyProject.iOS.Renderers
{
    public class EditInterestsPageRenderer : KeyboardResizedPageRenderer
    {
    }
}