using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Enums.Chat;
using XamarinSocial.Extensions;
using XamarinSocial.Models.Chat;
using XamarinSocial.Models.Photo;
using XamarinSocial.Services.Interfaces;
using LocalStrings = XamarinSocial.Resources.Strings.Strings;

namespace XamarinSocial.ViewModels.Chat
{
    public class ChatConsersationViewModel : BaseViewModel<ChatItemModel>
    {
        #region Services

        private readonly IChatService _chatService;
        private readonly IUserDialogs _userDialogs;
        private readonly IImageSourceService _imageSourceService;

        #endregion

        public ChatConsersationViewModel(IMvxNavigationService navigationService,
                                         IChatService chatService,
                                         IUserDialogs userDialogs,
                                         IImageSourceService imageSourceService)
                                         : base(navigationService)
        {
            _chatService = chatService;
            _userDialogs = userDialogs;
            _imageSourceService = imageSourceService;
            InitControls();
        }

        #region Properties

        private ChatItemModel _userChatModel;
        public ChatItemModel UserChatModel
        {
            get => _userChatModel;
            set
            {
                SetProperty(ref _userChatModel, value);
            }
        }

        private MvxObservableCollection<ChatConversationItemModel> _items;
        public MvxObservableCollection<ChatConversationItemModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
            }
        }

        private string _newMessageText;
        public string NewMessageText
        {
            get => _newMessageText;
            set
            {
                SetProperty(ref _newMessageText, value);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand OpenMenuCommand { get; private set; }
        public IMvxCommand SendCommand { get; private set; }
        public IMvxCommand AttachCommand { get; private set; }
        public IMvxCommand AddEmojyCommand { get; private set; }
        public IMvxCommand ItemAppearingCommand { get; private set; }
        public IMvxCommand ItemDisappearingCommand { get; private set; }
        public IMvxCommand ItemSelectedCommand { get; private set; }

        #endregion

        #region Iteractions

        public MvxInteraction<ChatConversationActionType> ChatIteraction { get; set; }

        #endregion

        #region Overrides
        public override void Prepare(ChatItemModel parameter)
        {
            if (parameter != null)
            {
                UserChatModel = parameter;
            }
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            Items.Clear();
            await SetItemsAsync();
            ChatIteraction?.Raise(ChatConversationActionType.ScrollToLast);
        }

        public override void ViewDisappearing()
        {
            Items.Clear();
            base.ViewDisappearing();
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            OpenMenuCommand = new MvxAsyncCommand(OpenMenuAsync);
            SendCommand = new MvxAsyncCommand(SendAsync);
            AttachCommand = new MvxAsyncCommand(OnAttachedAsync);
            AddEmojyCommand = new MvxAsyncCommand(OnEmojyAddedAsync);
            ItemAppearingCommand = new MvxAsyncCommand<ChatConversationItemModel>(OnItemAppearingAsync);
            ItemDisappearingCommand = new MvxAsyncCommand<ChatConversationItemModel>(OnItemDisappearingAsync);
            ItemSelectedCommand = new MvxAsyncCommand<ChatConversationItemModel>(OnSelectedAsync);

            ChatIteraction = new MvxInteraction<ChatConversationActionType>();

            Items = new MvxObservableCollection<ChatConversationItemModel>();
        }

        private async Task SetItemsAsync(bool isRefreshed = false)
        {
            if (!isRefreshed)
            {
                _userDialogs.ShowLoading();
            }
            await Task.Delay(200); //for UI testing only
            var result = await _chatService.GetConservationByIdAsync(Items.Count, Configuration.TakeChatConversationItemsCount);
            if (result.Errors.Any())
            {
                _userDialogs.Toast(result.Errors.FirstOrDefault());
                return;
            }
            result.Data.Items.SetTopDateVisibility(null);
            result.Data.Items.Reverse();
            Items.AddRange(result.Data.Items);
            await Task.Delay(200);
            _userDialogs.HideLoading();
        }

        private async Task OpenMenuAsync()
        {
            ChatIteraction?.Raise(ChatConversationActionType.OpenMenu);
        }

        private async Task SendAsync()
        {
            if (string.IsNullOrWhiteSpace(NewMessageText))
            {
                return;
            }

            var newItem =(new ChatConversationItemModel() 
            {
                Message = NewMessageText,
                CreationDate = DateTime.Now,
                IsOwn = true,
                ShortDateString = DateTime.Now.ToString("HH:mm")
            });
            Items[Items.Count-1] = newItem.SetNewItemTopTextVisibility(Items.LastOrDefault());
            Items.Add(newItem);
            NewMessageText = string.Empty;
            ChatIteraction.Raise(ChatConversationActionType.ScrollToLast);
        }

        private async Task OnAttachedAsync()
        {
            var result = await _userDialogs.ActionSheetAsync(LocalStrings.GetPhotoFrom_,
                                     LocalStrings.Cancel, null, null,
                                     new string[] { LocalStrings.Camera, LocalStrings.Library });
            if (result.Equals(LocalStrings.Cancel))
            {
                return;
            }

            var imageSourceResult = new ImagesSourceModel();
            if (result.Equals(LocalStrings.Camera))
            {
                imageSourceResult = await _imageSourceService.GetImageFromCameraAsync();
            }
            if (result.Equals(LocalStrings.Library))
            {
                imageSourceResult = await _imageSourceService.GetImageFromLibraryAsync();
            }
            //using (_userDialogs.Loading())
            //{
            //    //todo: send image to server, insert new item to Items
            //}
            _userDialogs.Toast("Image added");
        }

        private async Task OnEmojyAddedAsync()
        {
            _userDialogs.Toast("On emojy button clicked");
        }

        private async Task OnItemAppearingAsync(ChatConversationItemModel item)
        {
            if (Items.IndexOf(item) == 0)
            {
                _userDialogs.ShowLoading();
                await Task.Delay(300); //for show loader with hardcode only

                var result = await _chatService.GetConservationByIdAsync(Items.Count, Configuration.TakeChatConversationItemsCount);
                if (result.Errors.Any())
                {
                    _userDialogs.Toast(result.Errors.FirstOrDefault());
                    return;
                }
                Items[0] = result.Data.Items.SetTopDateVisibility(Items[0]);
                result.Data.Items.ForEach(x => Items.Insert(0, x));

                ChatIteraction.Raise(ChatConversationActionType.ScrollToFirstVisible);
                _userDialogs.HideLoading();
            }
        }

        private async Task OnItemDisappearingAsync(ChatConversationItemModel item)
        {
            if (Items.IndexOf(item) == 0)
            {
                //todo: add logic if needed
            }
        }

        private async Task OnSelectedAsync(ChatConversationItemModel item)
        {
            //todo: add logic if needed
            _userDialogs.Toast($"Id={item.Id} tepped");
        }

        #endregion

        #region Public Methods

        public void OnMenuActionInvokeAsync(ChatConversationMenuActionType actionType)
        {
            _userDialogs.Toast($"On {actionType.ToString()} clicked!");
        }

        #endregion
    }
}
