using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Models.Chat;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModels.Chat;

namespace XamarinSocial.ViewModels.TabsView
{
    public class ChatViewModel : BaseViewModel
    {
        #region Services

        private readonly IChatService _chatService;
        private readonly IUserDialogs _userDialogs;

        #endregion

        public ChatViewModel(IMvxNavigationService navigationService,
                             IChatService chatService,
                             IUserDialogs userDialogs)
                            : base(navigationService)
        {
            _chatService = chatService;
            _userDialogs = userDialogs;
            InitControls();
        }

        #region Properties

        private MvxObservableCollection<ChatItemModel> _items;
        public MvxObservableCollection<ChatItemModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
            }
        }

        private bool _isChatsVisible;
        public bool IsChatsVisible
        {
            get => _isChatsVisible;
            set
            {
                SetProperty(ref _isChatsVisible, value);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand OpenMenuCommand { get; private set; }
        public IMvxCommand SearchCommand { get; private set; }
        public IMvxCommand InviteFriendsCommand { get; private set; }

        #endregion

        #region Overrides

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            Items.Clear();
            await SetItemsAsync();
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
            Items.Clear();
        }

        protected async override Task RefreshAsync()
        {
            Items.Clear();
            await SetItemsAsync(true);
            await base.RefreshAsync();
        }

        public async override Task LoadMoreAsync()
        {
            if (Items.Count == 0)
            {
                return;
            }
            await SetItemsAsync();
            await base.LoadMoreAsync();
        }

        public async override Task OnItemSelectedAsync(string id)
        {
            //todo: add logic
            var selectedItem = Items.Where(item => item.Id.Equals(id)).FirstOrDefault();
            if (selectedItem != null)
            {
                await NavigationService.Navigate<ChatConsersationViewModel, ChatItemModel>(selectedItem);
            }
            //_userDialogs.Alert($"Id={id} was selected.");
            await base.OnItemSelectedAsync(id);
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            OpenMenuCommand = new MvxAsyncCommand(OpenMenuAsync);
            SearchCommand = new MvxAsyncCommand(OnSearchAsync);
            InviteFriendsCommand = new MvxAsyncCommand(InviteFriendsAsync);

            Items = new MvxObservableCollection<ChatItemModel>();

            IsChatsVisible = false;
        }

        private async Task OpenMenuAsync()
        {
            //todo: add logic
            _userDialogs.Toast("On menu button clicked!");
        }

        private async Task OnSearchAsync()
        {
            //todo: add logic
            _userDialogs.Toast("On search button clicked!");
        }

        private async Task SetItemsAsync(bool isRefreshed = false)
        {
            if (!isRefreshed)
            {
                _userDialogs.ShowLoading();
            }
            await Task.Delay(200); //for UI testing only
            var result = await _chatService.GetByIdAsync(Items.Count, Configuration.TakeChatItemsCount);
            if (result.Errors.Any())
            {
                _userDialogs.Toast(result.Errors.FirstOrDefault());
                return;
            }
            Items.AddRange(result.Data.Items);
            IsChatsVisible = true;
            _userDialogs.HideLoading();
        }

        private async Task InviteFriendsAsync()
        {
            _userDialogs.Toast("On Invite friends clicked!");
        }

        #endregion
    }
}
