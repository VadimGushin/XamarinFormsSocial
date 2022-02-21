using MvvmCross.Forms.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSocial.iOS.Renderer;

[assembly: ExportRenderer(typeof(MvxTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace XamarinSocial.iOS.Renderer
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        public UIEdgeInsets TabBarItemImageInsets => new UIEdgeInsets(-7, -7, -7, -7);

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if(TabBar?.Items == null)
            {
                return;
            }

            for (int i = 0; i < TabBar.Items.Length; i++)
            {
                TabBar.Items[i].ImageInsets = TabBarItemImageInsets;
            }
        }
    }
}