using MvvmCross;
using MvvmCross.Plugin.Messenger;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Messages;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Views.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostFeedCell : ContentView
    {
        private readonly IMvxMessenger _messenger;
        private MvxSubscriptionToken _playVideoSubscriptionToken;
        private MvxSubscriptionToken _updateStateSubscriptionToken;

        public PostFeedCell()
        {
            InitializeComponent();
            _messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();

            _updateStateSubscriptionToken = _messenger.Subscribe<UpdateFeedItemMessage>(OnUpdateStateReceived);

            videoContainer.OnPlayVideoPressed = OnPlayVideoPressed;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var post = BindingContext as Post;
            if (post == null || post.Content == null)
            {
                return;
            }

            if (post.Content.FirstOrDefault(content => content.ContentType == Enums.ContentType.Video) == null)
            {
                imagePlay.IsVisible = false;
                videoContainer.IsVisible = false;
                return;
            }

            imagePlay.IsVisible = (post.Content.Count() > 1);
            videoContainer.IsVisible = (post.Content.Count() == 1);
        }

        private void OnPlayVideoPressed(bool isPlayingFromPause)
        {
            if (isPlayingFromPause)
            {
                return;
            }

            _messenger.Publish(new PlayVideoMessage(this));
            _playVideoSubscriptionToken = _messenger.Subscribe<PlayVideoMessage>(OnPlayVideoMessageReceived);
        }

        private void OnPlayVideoMessageReceived(PlayVideoMessage message)
        {
            if (message.Sender == this)
            {
                return;
            }

            RemoveVideoPlayer(videoContainer);
        }

        private void OnUpdateStateReceived(UpdateFeedItemMessage message)
        {
            if (message.CollectionItemEventType != Enums.CollectionItemEventType.Disappeared)
            {
                return;
            }

            RemoveVideoPlayer(videoContainer);
        }

        private void RemoveVideoPlayer(Shared.VideoCellContent content)
        {
            content.StopPlayer();

            if (_playVideoSubscriptionToken != null)
            {
                _messenger.Unsubscribe<PlayVideoMessage>(_playVideoSubscriptionToken);
                _playVideoSubscriptionToken = null;
            }
        }
    }
}