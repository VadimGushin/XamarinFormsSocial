using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Enums;
using XamarinSocial.Enums.Notification;
using XamarinSocial.Models.Notification;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.ViewModels.TabsView
{
    public class NotificationViewModel : BaseViewModel
    {
        #region Services

        private readonly INotificationService _notificationService;
        private readonly IUserDialogs _userDialogs;

        #endregion

        #region Variables

        private CustomTabState _currentTab;

        #endregion

        public NotificationViewModel(IMvxNavigationService navigationService,
                                     INotificationService notificationService,
                                     IUserDialogs userDialogs)
                                     : base(navigationService)
        {
            _notificationService = notificationService;
            _userDialogs = userDialogs;
            _currentTab = CustomTabState.FirstTabSelected;
            CustomTabClickedCommand = new MvxAsyncCommand<CustomTabState>(OnTabClickedAsync);
        }

        #region Properties

        private MvxObservableCollection<NotificationItemModel> _items;
        public MvxObservableCollection<NotificationItemModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
            }
        }

        private string _firstBadgeValue;
        public string FirstBadgeValue
        {
            get => _firstBadgeValue;
            set
            {
                SetProperty(ref _firstBadgeValue, value);
            }
        }

        private string _secondBadgeValue;
        public string SecondBadgeValue
        {
            get => _secondBadgeValue;
            set
            {
                SetProperty(ref _secondBadgeValue, value);
            }
        }

        private string _thirdBadgeValue;
        public string ThirdBadgeValue
        {
            get => _thirdBadgeValue;
            set
            {
                SetProperty(ref _thirdBadgeValue, value);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand CustomTabClickedCommand { get; private set; }

        #endregion

        #region Overrides

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            await InitControls();
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
            Items.Clear();
        }

        protected async override Task RefreshAsync()
        {
            Items.Clear();
            await ChangeCollectionAsync(_currentTab, true);
            await base.RefreshAsync();
        }

        public async override Task LoadMoreAsync()
        {
            if (Items.Count == 0)
            {
                return;
            }
            await ChangeCollectionAsync(_currentTab);
            await base.LoadMoreAsync();
        }

        public async override Task OnItemSelectedAsync(string id)
        {
            //todo:add logic
            _userDialogs.Toast($"Id={id} was selected.");
            await base.OnItemSelectedAsync(id);
        }

        #endregion

        #region Private Methods

        private async Task InitControls()
        {
            Items = new MvxObservableCollection<NotificationItemModel>();
            await SetItemsAsync(NotificationType.Likes);
        }
        private async Task OnTabClickedAsync(CustomTabState tabState)
        {
            Items.Clear();
            _currentTab = tabState;
            await ChangeCollectionAsync(tabState);
        }

        private async Task ChangeCollectionAsync(CustomTabState tabState, bool isRefresed = false)
        {           
            var state = NotificationType.None;
            if (tabState == CustomTabState.FirstTabSelected)
            {
                state = NotificationType.Likes;
            }
            if (tabState == CustomTabState.SecondTabSelected)
            {
                state = NotificationType.Views;
            }
            if (tabState == CustomTabState.ThirdTabSelected)
            {
                state = NotificationType.More;
            }
            await SetItemsAsync(state, isRefresed);
        }

        private async Task InitBadgesAsync()
        {
            var badgesModel = await _notificationService.GetBadgesCountAsync();
            FirstBadgeValue = badgesModel.Data.LikesCount.ToString();
            SecondBadgeValue = badgesModel.Data.ViewsCount.ToString();
            ThirdBadgeValue = badgesModel.Data.MoreCount.ToString();
        }

        private async Task SetItemsAsync(NotificationType type, bool isRefreshed = false)
        {
            if (type == NotificationType.None)
            {
                return;
            }
            if (!isRefreshed)
            {
                _userDialogs.ShowLoading();
            }
            await Task.Delay(200); //for UI testing
            var result = await _notificationService.GetByIdAsync(type, Items.Count, Configuration.TakeNotificationsCount);
            if (result.Errors.Any())
            {
                _userDialogs.Toast(result.Errors.FirstOrDefault());
                return;
            }
            Items.AddRange(result.Data.Items);
            await InitBadgesAsync();
            _userDialogs.HideLoading();
        }

        #endregion
    }
}
