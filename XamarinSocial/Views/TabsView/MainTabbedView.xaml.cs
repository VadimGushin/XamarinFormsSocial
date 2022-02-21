using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.TabsView;

namespace XamarinSocial.Views.TabsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    public partial class MainTabbedView : MvxTabbedPage<MainTabbedViewModel>
    {
        public MainTabbedView()
        {
            InitializeComponent();
        }
    }
}