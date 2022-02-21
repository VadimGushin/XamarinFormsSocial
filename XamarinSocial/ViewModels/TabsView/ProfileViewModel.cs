using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Enums;
using XamarinSocial.Extensions;
using XamarinSocial.Models;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModels.ProfileSet;
using XamarinSocial.ViewModels.Settings;

namespace XamarinSocial.ViewModels.TabsView
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IFeedService _feedService;
        private readonly IProfileService _profileService;


        public ProfileViewModel(IMvxNavigationService navigationService,
                                IFeedService feedService,
                                IProfileService profileService) : base(navigationService)
        
        {
            _feedService = feedService;
            _profileService = profileService;
            SetupUI();
            Profile = _profileService.GetByIdAsync().Result;
            //Profile = new UserProfile();
            //GetProfile();
            Feeds = new ObservableCollection<ProfileFeed>();
            Photos = new ObservableCollection<PhotoModel>();
        }

        #region Properties

        private UserProfile _profile;
        public UserProfile Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        private ObservableCollection<ProfileFeed> _feeds;
        public ObservableCollection<ProfileFeed> Feeds
        {
            get => _feeds;
            set
            {
                SetProperty(ref _feeds, value);
            }
        }

        private ObservableCollection<PhotoModel> _photos;
        public ObservableCollection<PhotoModel> Photos
        {
            get => _photos;
            set
            {
                SetProperty(ref _photos, value);
            }
        }

        private bool _isFirstCustomTabSelected;
        public bool IsFirstCustomTabSelected
        {
            get => _isFirstCustomTabSelected;
            set
            {
                SetProperty(ref _isFirstCustomTabSelected, value);
            }
        }

        private bool _isSecondCustomTabSelected;
        public bool IsSecondCustomTabSelected
        {
            get => _isSecondCustomTabSelected;
            set
            {
                SetProperty(ref _isSecondCustomTabSelected, value);
            }
        }

        private bool _isThirdCustomTabSelected;
        public bool IsThirdCustomTabSelected
        {
            get => _isThirdCustomTabSelected;
            set
            {
                SetProperty(ref _isThirdCustomTabSelected, value);
            }
        }

        private bool _isShowTabsHeader;
        public bool IsShowTabsHeader
        {
            get => _isShowTabsHeader;
            set
            {
                SetProperty(ref _isShowTabsHeader, value);
            }
        }

        private int _feedItemTreshold;
        public int FeedItemTreshold
        {
            get => _feedItemTreshold;
            set => SetProperty(ref _feedItemTreshold, value);
        }

        private int _photoItemTreshold;
        public int PhotoItemTreshold
        {
            get => _photoItemTreshold;
            set => SetProperty(ref _photoItemTreshold, value);
        }

        #endregion

        #region Overrides

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            //await GetProfileAsync();
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();
            //todo: show error message
            bool isPostsSuccessfullyFetched = await LoadFeedList(Configuration.CountTakeProfileFeedItems, 0);
            bool isPhotosSuccessfullyFetched = await LoadPhotos(Configuration.CountTakeProfilePhotos, 0);
            //IsFeedEmpty = !isPostsSuccessfullyFetched;
        }
        #endregion

        #region Commands

        private IMvxCommand<CustomTabState> _customTabClickedCommand;
        public IMvxCommand<CustomTabState> CustomTabClickedCommand
        {
            get
            {
                _customTabClickedCommand = _customTabClickedCommand ?? new MvxCommand<CustomTabState>(OnCustomTabClicked);

                return _customTabClickedCommand;
            }
        }

        public IMvxAsyncCommand LoadMoreFeedCommandAsync => new MvxAsyncCommand(LoadMoreFeedAsync);
        public IMvxAsyncCommand LoadMorePhotosCommandAsync => new MvxAsyncCommand(LoadMorePhotosAsync);
        public IMvxAsyncCommand EditProfileCommand => new MvxAsyncCommand(async () => { await NavigationService.Navigate<EditProfileViewModel>(); });
        public IMvxAsyncCommand SettingsCommand => new MvxAsyncCommand(async () => { await NavigationService.Navigate<SettingsViewModel>(); });
        #endregion

        #region Functions
        private async Task LoadMorePhotosAsync()
        {
            int offset = Photos.Count;
            await LoadPhotos(Configuration.CountTakeSearchUsersItems, offset);
        }

        private async Task LoadMoreFeedAsync()
        {
            int offset = Feeds.Count;
            await LoadFeedList(Configuration.CountTakeProfilePhotos, offset);
        }

        private void SetupUI()
        {
            IsShowTabsHeader = true;
            IsFirstCustomTabSelected = true;
            IsSecondCustomTabSelected = false;
            IsThirdCustomTabSelected = false;
        }
        //private async Task GetProfileAsync()
        //{
        //    Profile = await _profileService.GetProfileByIdAsync();
        //}

        private void OnCustomTabClicked(CustomTabState currentTab)
        {
            InvokeOnMainThread(() =>
            {
                if (currentTab == CustomTabState.FirstTabSelected && !IsFirstCustomTabSelected)
                {
                    IsFirstCustomTabSelected = true;
                    IsSecondCustomTabSelected = false;
                    IsThirdCustomTabSelected = false;
                    IsShowTabsHeader = true;
                }

                if (currentTab == CustomTabState.SecondTabSelected && !IsSecondCustomTabSelected)
                {
                    IsFirstCustomTabSelected = false;
                    IsSecondCustomTabSelected = true;
                    IsThirdCustomTabSelected = false;
                    IsShowTabsHeader = true;
                }
                if (currentTab == CustomTabState.ThirdTabSelected && !IsThirdCustomTabSelected)
                {
                    IsFirstCustomTabSelected = false;
                    IsSecondCustomTabSelected = false;
                    IsThirdCustomTabSelected = true;
                    IsShowTabsHeader = true;
                }
            });
        }

        private async Task<bool> LoadFeedList(int take, int offset)
        {
            ResponseModel<GetProfileFeedListModel> postsResponse = await _feedService.GetProfileFeedList(take, offset);
            if (!postsResponse.IsSucceed || !postsResponse.Data.ProfileFeeds.Any())
            {
                return false;
            }

            Feeds.AddRange(postsResponse.Data.ProfileFeeds);
            return true;
        }

        private async Task<bool> LoadPhotos(int take, int offset)
        {
            ResponseModel<GetProfilePhotosModel> photosResponse = await _feedService.GetProfilePhotos(take, offset);
            if (!photosResponse.IsSucceed || !photosResponse.Data.Photos.Any())
            {
                return false;
            }

            //Photos.AddRange(photosResponse.Data.Photos);
             return true; 
        }

        #endregion
    }
}
