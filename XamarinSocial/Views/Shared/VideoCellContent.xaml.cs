using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.VideoSources;

namespace XamarinSocial.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCellContent : ContentView
    {
        private bool _isPaused = false;
        public VideoCellContent()
        {
            InitializeComponent();
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;

                Device.BeginInvokeOnMainThread(() =>
                {
                    loadingIndicator.IsRunning = value;
                });
            }
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                Device.BeginInvokeOnMainThread(() =>
                {
                    var element = videoContainer;
                    if (element == null)
                    {
                        return;
                    }
                    element.IsVisible = value;
                });
            }
        }

        public Action<bool> OnPlayVideoPressed;

        public void StopPlayer()
        {
            if (videoContainer == null)
            {
                return;
            }
            _isPaused = false;
            videoContainer.Stop();
            imagePlay.IsVisible = true;
            imagePreview.IsVisible = true;
        }

        public void PausePlayer()
        {
            if (videoContainer == null)
            {
                return;
            }

            videoContainer.Pause();
            _isPaused = true;
            imagePlay.IsVisible = true;
        }

        private void PlayTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            imagePreview.IsVisible = false;
            imagePlay.IsVisible = false;

            OnPlayVideoPressed?.Invoke(false);

            if (videoContainer.Status == Enums.VideoPlayer.VideoStatus.Paused && _isPaused)
            {
                _isPaused = false;
                videoContainer.Play();
                return;
            }
            _isPaused = false;
            string source = (BindingContext as Post).Content.FirstOrDefault().Source;

            videoContainer.Source = VideoSource.FromUri(source);
            videoContainer.Play();
        }

        private void StopTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PausePlayer();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var post = BindingContext as Post;
            if (post == null || post.Content == null || !post.Content.Any())
            {
                return;
            }

            var content = post.Content.FirstOrDefault();
            imagePreview.Source = content.PreviewSource;
        }
    }
}