using MvvmCross.Forms.Presenters.Attributes;
using XamarinSocial.ViewModels.SignUp;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.SignUp
{
   [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class SignUp : BaseContentPage<SignUpViewModel>
    {
        public SignUp()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            ViewModel.CloseApplicationCommand.Execute();
            return true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ForgetPasswordButton.IsVisible = true;//TODO Forget password button disappears after back command from others view
            ForgetPasswordButton.IsEnabled = true;
        }
    }
}