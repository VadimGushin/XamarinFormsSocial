using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.ProfileSet;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.ProfileSet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : BaseContentPage<UserProfileViewModel>
    {
        public UserProfile()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            FirstViewGrid.IsVisible = true;
            SecondViewGrid.IsVisible = true;
            ThirdGridView.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.IsChangePhotoButtonVisible = false;//On start this button is visible but then ve hide it, need for rendering notVisible Gradient button on iOS
        }

        private void GoToFirstViewCommand(object sender, System.EventArgs e)
        {
            FirstViewGrid.IsVisible = true;
            SecondViewGrid.IsVisible = false;
            ThirdGridView.IsVisible = false;
            this.UpdateChildrenLayout();
        }

        private void GoToSecondViewCommand(object sender, System.EventArgs e)
        {
            FirstViewGrid.IsVisible = false;
            SecondViewGrid.IsVisible = true;
            ThirdGridView.IsVisible = false;
            this.UpdateChildrenLayout();
        }

        private void GoToThirdViewCommand(object sender, System.EventArgs e)
        {
            FirstViewGrid.IsVisible = false;
            SecondViewGrid.IsVisible = false;
            ThirdGridView.IsVisible = true;
            this.UpdateChildrenLayout();
        }

        private async void GradientButton_Clicked(object sender, System.EventArgs e)
        {
            string action = await DisplayActionSheet("Get Photo from:", "Cancel", null, "Camera", "Library");
            if (string.IsNullOrWhiteSpace(action))
            {
                return;
            }
            if (action == "Camera")
            {
                await ViewModel.GetImageFromCameraAsync();
            }
            if (action == "Library")
            {
                await ViewModel.GetImageFromLibraryAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (FirstViewGrid.IsVisible)
            {
                ViewModel.AppCloseCommand.Execute();
            }
            if (SecondViewGrid.IsVisible)
            {
                GoToFirstViewCommand(this, null);
            }
            if (ThirdGridView.IsVisible)
            {
                GoToSecondViewCommand(this, null);
            }
            return true;
        }
    }
}