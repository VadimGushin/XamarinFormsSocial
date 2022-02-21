using MvvmCross.Forms.Presenters.Attributes;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.TabsView;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.TabsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class SearchUsersView : BaseContentPage<SearchUsersViewModel>
    {
        private double _previousScrolly;
        public bool _isLoadingMore = false;
        public SearchUsersView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            _previousScrolly = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BoxViewForTabs.HeightRequest = TabsHeader.Height;
        }

        #region Events

        private void UsersCollection_Scrolled(object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            if (e.VerticalDelta > 0 && e.VerticalOffset > 90)
            {
                //scrolled down
                TabsHeader.IsVisible = false;
            }
            if (e.VerticalDelta < -0)
            {
                //scrolled up
                TabsHeader.IsVisible = true;
            }

        }
        #endregion

        private void ScrollView_Scrolled(object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            var scrollView = sender as Xamarin.Forms.ScrollView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (_previousScrolly < e.ScrollY)
            {
                TabsHeader.IsVisible = false;
            }

            if (_previousScrolly > e.ScrollY)
            {
                TabsHeader.IsVisible = true;
            }
            _previousScrolly = e.ScrollY;

            if (scrollingSpace <= e.ScrollY && !_isLoadingMore)
            {
                Task.Run(async () =>
                {
                    _isLoadingMore = true;
                    await ViewModel.OnLoadMoreAsync();
                    _isLoadingMore = false;
                });
            }
        }
    }
}