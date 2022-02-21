using MvvmCross.Forms.Views;
using XamarinSocial.ViewModels;

namespace XamarinSocial.Views
{

    public partial class MainPage : MvxContentPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.ShowFirstViewModelCommand.Execute(null);

            base.OnAppearing();
        }
    }
}
