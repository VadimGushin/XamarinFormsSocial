using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.Feed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePost : BaseContentPage<CreatePostViewModel>
    {
        public CreatePost()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            entryDescription.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }

        private async void CreatePostHorizontalImageList_AddContentPressed()
        {
            string action = await DisplayActionSheet("Add content", "Cancel", null, "Photo", "Video");
            if (string.IsNullOrWhiteSpace(action))
            {
                return;
            }
            if (action == "Photo")
            {
                await AddPhotoContent();
            }
            if (action == "Video")
            {
                await AddVideoContent();
            }
        }

        private async Task AddPhotoContent()
        {
            string action = await DisplayActionSheet("Get Photo from:", "Cancel", null, "Camera", "Library");
            if (string.IsNullOrWhiteSpace(action))
            {
                return;
            }
            if (action == "Camera")
            {
                await ViewModel.GetImageFromCameraCommandAsync.ExecuteAsync();
            }
            if (action == "Library")
            {
                await ViewModel.GetImageFromLibraryCommandAsync.ExecuteAsync();
            }
        }

        private async Task AddVideoContent()
        {
            string action = await DisplayActionSheet("Get Video from:", "Cancel", null, "Camera", "Library");
            if (string.IsNullOrWhiteSpace(action))
            {
                return;
            }
            if (action == "Camera")
            {
                await ViewModel.GetVideoFromCameraCommandAsync.ExecuteAsync();
            }
            if (action == "Library")
            {
                await ViewModel.GetVideoFromLibraryCommandAsync.ExecuteAsync();
            }
        }
    }
}