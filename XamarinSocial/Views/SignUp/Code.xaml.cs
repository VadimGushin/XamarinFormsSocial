using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.SignUp;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Code : BaseContentPage<CodeViewModel>
    {
        public Code()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            ViewModel.BackCommand.Execute();

            return true;
        }
    }
}