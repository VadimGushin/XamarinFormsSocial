using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.TabsView;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.TabsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class NotificationPage : BaseContentPage<NotificationViewModel>
    {
        public NotificationPage()
        {
            InitializeComponent();
        }
    }
}