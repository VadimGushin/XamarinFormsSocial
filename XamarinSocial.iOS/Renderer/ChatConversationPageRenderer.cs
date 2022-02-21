using MyProject.iOS.Renderers;
using Xamarin.Forms;
using XamarinSocial.iOS.Renderer.Base;
using XamarinSocial.Views.Chat;

[assembly: ExportRenderer(typeof(ChatConversationPage), typeof(ChatConversationPageRenderer))]

namespace MyProject.iOS.Renderers
{
    public class ChatConversationPageRenderer : KeyboardResizedPageRenderer
    {
    }
}