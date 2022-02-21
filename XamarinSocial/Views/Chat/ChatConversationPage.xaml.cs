using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Enums.Chat;
using XamarinSocial.Helpers;
using XamarinSocial.Interfaces;
using XamarinSocial.Models.Common;
using XamarinSocial.ViewModels.Chat;
using XamarinSocial.Views.Base;
using XamarinSocial.Views.Shared;

namespace XamarinSocial.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatConversationPage : BaseContentPage<ChatConsersationViewModel>, IKeyboardResizedContentPage
    {
        #region Variables

        private List<PopupMenuItemModel> _menuSource;

        #endregion

        public ChatConversationPage()
        {
            InitializeComponent();
            SetMenuItems();
        }

        #region Iteractions

        private IMvxInteraction<ChatConversationActionType> _chatIteraction;
        public IMvxInteraction<ChatConversationActionType> ChatIteraction
        {
            get => _chatIteraction;
            set
            {
                if (_chatIteraction != null)
                    _chatIteraction.Requested -= ChatIteraction_Requested;

                _chatIteraction = value;
                _chatIteraction.Requested += ChatIteraction_Requested;
            }
        }

        #endregion

        #region Overrides

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var set = this.CreateBindingSet<ChatConversationPage, ChatConsersationViewModel>();
            set.Bind(this).For(v => v.ChatIteraction).To(vm => vm.ChatIteraction);
            set.Apply();
        }

        #endregion

        #region Private Methods

        private void ChatIteraction_Requested(object sender, MvvmCross.Base.MvxValueEventArgs<ChatConversationActionType> e)
        {
            if (e.Value == ChatConversationActionType.ScrollToLast)
            {
                this.ChatList.ScrollTo(ViewModel.Items.LastOrDefault(), ScrollToPosition.Start, false);
            }
            if (e.Value == ChatConversationActionType.ScrollToFirstVisible)
            {
                this.ChatList.ChangeIsLoadedValue();
                this.ChatList.ScrollTo(ViewModel.Items[XamarinSocial.Constants.Configuration.TakeChatConversationItemsCount], ScrollToPosition.Start, false);
            }
            if (e.Value == ChatConversationActionType.OpenMenu)
            {
                PopupNavigation.Instance.PushAsync(new DropdownPopapView(SetSeletdedItem, _menuSource));
            }
        }

        private void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            entryView.Unfocus();
        }

        private void SetSeletdedItem(string selectedItem)
        {
            var actionType = (ChatConversationMenuActionType)(_menuSource.Where(item => item.Id.Equals(selectedItem)).FirstOrDefault().ActionType);
            ViewModel.OnMenuActionInvokeAsync(actionType);
        }

        private void SetMenuItems()
        {
            _menuSource = new List<PopupMenuItemModel>()
            {
                new PopupMenuItemModel(EnumHelper.GetEnumDescription(Enum.Parse(typeof(ChatConversationMenuActionType),
                                       ChatConversationMenuActionType.SearchChatHistory.ToString())),
                                       (Color)(Application.Current.Resources["fcolor_b6"]),
                                       "\uf002", ChatConversationMenuActionType.SearchChatHistory),
                new PopupMenuItemModel(EnumHelper.GetEnumDescription(Enum.Parse(typeof(ChatConversationMenuActionType),
                                       ChatConversationMenuActionType.Unmatch.ToString())),
                                       (Color)(Application.Current.Resources["fcolor_b6"]),
                                       "\uf004", ChatConversationMenuActionType.Unmatch),
                new PopupMenuItemModel(EnumHelper.GetEnumDescription(Enum.Parse(typeof(ChatConversationMenuActionType),
                                       ChatConversationMenuActionType.Block.ToString())),
                                       (Color)(Application.Current.Resources["fcolor_b6"]),
                                       "\uf55a", ChatConversationMenuActionType.Block),
                new PopupMenuItemModel(EnumHelper.GetEnumDescription(Enum.Parse(typeof(ChatConversationMenuActionType),
                                       ChatConversationMenuActionType.ReportUser.ToString())),
                                       (Color)(Application.Current.Resources["fcolor_b8"]),
                                       "\uf057", ChatConversationMenuActionType.ReportUser)
            };
        }

        #endregion
    }
}