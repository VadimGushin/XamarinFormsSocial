using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.SignUp;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailSignIn : BaseContentPage<EmailSignInViewModel>
    {
        public EmailSignIn()
        {
            InitializeComponent();
        }
    }
}