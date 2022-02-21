using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.Shared;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageAlbumPage : BaseContentPage<ImageAlbumViewModel>
    {
        public ImageAlbumPage()
        {
            InitializeComponent();
        }
    }
}