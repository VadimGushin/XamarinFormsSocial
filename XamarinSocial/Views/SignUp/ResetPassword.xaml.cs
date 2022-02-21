using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.SignUp;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassword : BaseContentPage<ResetPasswordViewModel>
    {
        public ResetPassword()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.IsSecondViewVisible = false;
            base.OnAppearing();
        }

        private void GoToFirstGridView(object sender, System.EventArgs e)
        {
            ViewModel.IsFirstViewVisible = true;
            ViewModel.IsSecondViewVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.IsFirstViewVisible)
            {
                ViewModel.BackCommand.Execute();
            }
            if (ViewModel.IsSecondViewVisible)
            {
                GoToFirstGridView(this, null);
            }
            return true;
        }
    }
}