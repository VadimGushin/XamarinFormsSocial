using MvvmCross;
using MvvmCross.Binding.Extensions;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Plugin.Messenger;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XamarinSocial.Enums;
using XamarinSocial.Messages;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.ViewModels.TabsView;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.TabsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class Home : BaseContentPage<HomeViewModel>
    {
        private readonly IMvxMessenger _messenger;
        public bool _isLoadingMore = false;

        public Home()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            _messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }

        private void FeedList_ItemDisappeared(object sender, System.EventArgs e)
        {
            //var post = feedList.ItemsSource.ElementAt((int)sender) as Post;

            //if (post == null)
            //{
            //    return;
            //}

            //var message = new UpdateFeedItemMessage(this, post, CollectionItemEventType.Disappeared);
            //_messenger.Publish<UpdateFeedItemMessage>(message);
        }

        private void OnCollectionScrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as Xamarin.Forms.ScrollView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace <= e.ScrollY && !_isLoadingMore)
            {
                Task.Run(async () =>
                {
                    _isLoadingMore = true;
                    await ViewModel.OnLoadMore();
                    _isLoadingMore = false;
                });
            }

            var message = new UpdateFeedItemMessage(this, null, CollectionItemEventType.Disappeared);
            _messenger.Publish<UpdateFeedItemMessage>(message);
        }
    }
}