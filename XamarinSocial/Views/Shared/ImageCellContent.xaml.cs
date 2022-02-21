using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageCellContent : ContentView
    {
        public ImageCellContent()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var post = BindingContext as Post;
            if (post == null || post.Content == null)
            {
                return;
            }

            if (post.Content.FirstOrDefault(content => content.ContentType == Enums.ContentType.Video) != null)
            {
                imagePlay.IsVisible = true;
                return;
            }

            imagePlay.IsVisible = false;
        }
    }
}