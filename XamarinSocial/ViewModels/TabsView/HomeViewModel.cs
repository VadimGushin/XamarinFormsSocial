using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Extensions;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.TabsView
{
    public class HomeViewModel : BaseViewModel
    {
        #region Services

        private readonly IUserDialogs _userDialogs;
        private readonly IFeedService _feedService;

        #endregion Services

        #region Constructors
        public HomeViewModel(IMvxNavigationService navigationService,
                             IFeedService feedService, IUserDialogs userDialogs)
            : base(navigationService)
        {
            _feedService = feedService;

            Posts = new MvxObservableCollection<Post>();
            _itemTreshold = Configuration.ItemTreshold;
            _userDialogs = userDialogs;
        }
        #endregion Constructors

        #region Properties

        private bool _isFeedEmpty;
        public bool IsFeedEmpty
        {
            get => _isFeedEmpty;
            set
            {
                SetProperty(ref _isFeedEmpty, value);
            }
        }

        private MvxObservableCollection<Post> _posts;
        public MvxObservableCollection<Post> Posts
        {
            get => _posts;
            set
            {
                SetProperty(ref _posts, value);
            }
        }
        public string UserId { get; private set; }

        private int _itemTreshold;
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        #endregion Properties


        #region Commands

        private IMvxAsyncCommand _navigateToCreatePostCommandAsync;
        public IMvxAsyncCommand NavigateToCreatePostCommandAsync
        {
            get
            {
                _navigateToCreatePostCommandAsync = _navigateToCreatePostCommandAsync ?? new MvxAsyncCommand(OnNavigateToCreatePost);

                return _navigateToCreatePostCommandAsync;
            }
        }

        private IMvxAsyncCommand _loadMoreCommandAsync;
        public IMvxAsyncCommand LoadMoreCommandAsync
        {
            get
            {
                _loadMoreCommandAsync = _loadMoreCommandAsync ?? new MvxAsyncCommand(OnLoadMore);

                return _loadMoreCommandAsync;
            }
        }

        private IMvxAsyncCommand<Post> _reportPostCommandAsync;
        public IMvxAsyncCommand<Post> ReportPostCommandAsync
        {
            get
            {
                _reportPostCommandAsync = _reportPostCommandAsync ?? new MvxAsyncCommand<Post>(OnReportPost);

                return _reportPostCommandAsync;
            }
        }

        private IMvxAsyncCommand<Post> _deletePostCommandAsync;
        public IMvxAsyncCommand<Post> DeletePostCommandAsync
        {
            get
            {
                _deletePostCommandAsync = _deletePostCommandAsync ?? new MvxAsyncCommand<Post>(OnDeletePost);

                return _deletePostCommandAsync;
            }
        }

        private IMvxAsyncCommand<Post> _editPostCommandAsync;
        public IMvxAsyncCommand<Post> EditPostCommandAsync
        {
            get
            {
                _editPostCommandAsync = _editPostCommandAsync ?? new MvxAsyncCommand<Post>(OnEditPost);

                return _editPostCommandAsync;
            }
        }

        private IMvxAsyncCommand<Post> _likePostCommandAsync;
        public IMvxAsyncCommand<Post> LikePostCommandAsync
        {
            get
            {
                _likePostCommandAsync = _likePostCommandAsync ?? new MvxAsyncCommand<Post>(OnLikePost);

                return _likePostCommandAsync;
            }
        }

        private IMvxAsyncCommand<Post> _commentPostCommandAsync;
        public IMvxAsyncCommand<Post> CommentPostCommandAsync
        {
            get
            {
                _commentPostCommandAsync = _commentPostCommandAsync ?? new MvxAsyncCommand<Post>(OnCommentPost);

                return _commentPostCommandAsync;
            }
        }

        #endregion Command

        #region Overrides
        public override async void ViewAppeared()
        {
            base.ViewAppeared();

            //todo: show error message
            bool isPostsSuccessfullyFetched = await LoadFeedList(Configuration.CountTakeFeedItems, 0);
            IsFeedEmpty = !isPostsSuccessfullyFetched;
        }

        #endregion Overrides

        #region Methods
        
        private async Task OnNavigateToCreatePost()
        {
            var createPostResult = await NavigationService.Navigate<CreatePostViewModel, DestructionResult<bool>>();
        }

        public async Task OnLoadMore()
        {
            int offset = Posts.Count;
            await LoadFeedList(Configuration.CountTakeFeedItems, offset);
        }

        private async Task OnReportPost(Post post)
        {
            //todo:add report post functionality
        }

        private async Task OnDeletePost(Post post)
        {
            //todo:add delete post functionality
        }

        private async Task OnEditPost(Post post)
        {
            //todo:add navigation to edit post page
        }

        private async Task OnLikePost(Post post)
        {
            post.CurrentUserMark = (Enums.UserMark.Liked == post.CurrentUserMark) ? Enums.UserMark.WithoutMark : Enums.UserMark.Liked ;
            post.NotifyPropertiesChanged();
            //todo: connnect 'like' func
        }

        private async Task OnCommentPost(Post post)
        {
            //todo: add navigation to post comments
        }

        private async Task<bool> LoadFeedList(int take, int offset)
        {
            _userDialogs.ShowLoading();

            await Task.Delay(100);

            ResponseModel<GetFeedListModel> postsResponse = await _feedService.GetFeedList(take, offset);
            if (!postsResponse.IsSucceed || !postsResponse.Data.Posts.Any())
            {
                _userDialogs.HideLoading();

                return false;
            }

            Posts.AddRange(postsResponse.Data.Posts);
            await Task.Delay(600);
            _userDialogs.HideLoading();

            return true;
        }

        #endregion Methods
    }
}
