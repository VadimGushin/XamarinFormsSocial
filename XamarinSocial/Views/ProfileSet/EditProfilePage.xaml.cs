using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.ProfileSet;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.ProfileSet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : BaseContentPage<EditProfileViewModel>
    {
        public EditProfilePage()
        {
            InitializeComponent();
        }
    }
}