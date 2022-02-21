using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Enums;
using XamarinSocial.Models;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.TabsView
{
    public class SearchUsersViewModel : BaseViewModel
    {
        private readonly IUserSearchService _userSearchService;
        private readonly IUserDialogs _userDialogs;
        private SearchUserFilter _userFilter;
        public SearchUsersViewModel(IMvxNavigationService navigationService, IUserSearchService userSearchService, IUserDialogs userDialogs) : base(navigationService)
        {
            _userSearchService = userSearchService;
            Items = new MvxObservableCollection<SearchUserModel>();
            IsFirstCustomTabSelected = true;
            _userDialogs = userDialogs;
        }

        #region Properties
        private bool _isSearchEmpty;
        public bool IsSearchEmpty
        {
            get => _isSearchEmpty;
            set
            {
                SetProperty(ref _isSearchEmpty, value);
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

        private MvxObservableCollection<SearchUserModel> _items;
        public MvxObservableCollection<SearchUserModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
            }
        }
        #endregion

        #region Overrides

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            Items.Clear();

            if (Items.Count == 0)
            {
                bool isUsersSuccessfullyFetched = await OnLoadMoreAsync();
                IsSearchEmpty = !isUsersSuccessfullyFetched;
            }
        }
        public override void ViewDisappearing()
        {
            Items.Clear();
            base.ViewDisappearing();
        }

        #endregion

        #region Commands
        public IMvxAsyncCommand OpenFilterViewCommandAsync => new MvxAsyncCommand(OpenFilterViewAsync);
        public IMvxCommand<CustomTabState> CustomTabClickedCommand => new MvxCommand<CustomTabState>(OnCustomTabClicked);
        public IMvxAsyncCommand LoadMoreCommandAsync => new MvxAsyncCommand(OnLoadMoreAsync);
        public IMvxCommand LikeUserCommandAsync => new MvxCommand<SearchUserModel>(OnLikeUser);
        #endregion

        #region Functions
        private async Task OpenFilterViewAsync()
        {
            var result = await NavigationService.Navigate<SearchFilterViewModel, SearchUserFilter, DestructionResult<SearchUserFilter>>(new SearchUserFilter());
            if (result != null && result.Destroyed)
            {
                _userFilter = result.Entity;
            }
        }

        private void OnCustomTabClicked(CustomTabState currentTab)
        {
            if (currentTab == CustomTabState.FirstTabSelected && !IsFirstCustomTabSelected)
            {
                IsFirstCustomTabSelected = true;
            }

            if (currentTab == CustomTabState.SecondTabSelected && IsFirstCustomTabSelected)
            {
                IsFirstCustomTabSelected = false;
            }
        }

        private void OnLikeUser(SearchUserModel arg)
        {
            InvokeOnMainThread(async () =>
            {
                arg.IsLiked = !arg.IsLiked;
                await RaisePropertyChanged(nameof(Items));
            });
        }

        public async Task<bool> OnLoadMoreAsync()
        {
            _userDialogs.ShowLoading();
            ResponseModel<GetSearchUserModel> postsResponse = await _userSearchService.GetSearchUserList(Configuration.CountTakeSearchUsersItems, Items.Count());
            if (!postsResponse.IsSucceed || !postsResponse.Data.SearchUsers.Any())
            {
                _userDialogs.HideLoading();
                return false;
            }
            Items.AddRange(postsResponse.Data.SearchUsers);
            await Task.Delay(600);
            _userDialogs.HideLoading();

            return true;
        }
        #endregion
    }
}
