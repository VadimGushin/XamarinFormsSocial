using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.TabsView;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.TabsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchFilterView : BaseContentPage<SearchFilterViewModel>
    {
        public SearchFilterView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
} 