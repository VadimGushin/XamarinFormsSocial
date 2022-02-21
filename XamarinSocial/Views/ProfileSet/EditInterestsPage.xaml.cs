using Xamarin.Forms.Xaml;
using XamarinSocial.Interfaces;
using XamarinSocial.ViewModels.ProfileSet;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.ProfileSet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditInterestsPage : BaseContentPage<EditInterestsViewModel>, IKeyboardResizedContentPage
    {
        public EditInterestsPage()
        {
            InitializeComponent();
        }
    }
}