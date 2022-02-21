using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.SignUp;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryCode : BaseContentPage<CountryCodeViewModel>
    {
        public CountryCode()
        {
            InitializeComponent();
        }
    }
}