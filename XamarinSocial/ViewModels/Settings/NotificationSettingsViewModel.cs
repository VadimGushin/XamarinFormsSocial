using MvvmCross.Commands;
using MvvmCross.Navigation;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Enums.Settings;
using XamarinSocial.Helpers;
using XamarinSocial.Models.Settings;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModelsResult;
using Localization = XamarinSocial.Resources.Strings.Strings;

namespace XamarinSocial.ViewModels.Settings
{
    public class NotificationSettingsViewModel : BaseViewModel
    {
        #region Services

        private readonly ISettingsService _settingsService;

        #endregion

        public NotificationSettingsViewModel(IMvxNavigationService navigationService,
                                             ISettingsService settingsService)
                                             : base(navigationService)
        {
            _settingsService = settingsService;
            InitControls();
        }

        #region Properties

        private NotificationSettingsModel _notificationSettingsData;
        public NotificationSettingsModel NotificationSettingsData
        {
            get => _notificationSettingsData;
            set
            {
                SetProperty(ref _notificationSettingsData, value);
            }
        }

        #endregion

        #region Commnads

        public IMvxCommand ItemClickCommand { get; private set; }

        #endregion

        #region Overrides

        protected async override Task TryBackPage()
        {
            await _settingsService.SetNotificationSettingsAsync(NotificationSettingsData);
            await base.TryBackPage();
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            ItemClickCommand = new MvxAsyncCommand<string>(OnItemClickAsync);

            var notificationSettingsData = _settingsService.GetNotificationSettingsAsync().Result;
            notificationSettingsData.SetStringValues();
            NotificationSettingsData = notificationSettingsData;
        }

        private async Task OnItemClickAsync(string title)
        {
            AdditionalNotificationSettingsModel additionalModel = new AdditionalNotificationSettingsModel();
            if (title.Equals(Localization.PrivateMessages))
            {
                additionalModel = new AdditionalNotificationSettingsModel(EnumHelper.GetCollectionByType(typeof(PrivateMessageType)),
                                      NotificationSettingsData.PrivateMessageString,
                                      Localization.PrivateMessages,
                                      NotificationSettingsData.IsPrivateMessagesNotificationTurnedOn);
            }
            if (title.Equals(Localization.Likes))
            {
                additionalModel = new AdditionalNotificationSettingsModel(EnumHelper.GetCollectionByType(typeof(LikeType)),
                                      NotificationSettingsData.LikeString,
                                      Localization.Likes,
                                      NotificationSettingsData.IsLikesNotificationTurnedOn);
            }
            if (title.Equals(Localization.Comments_Settings_))
            {
                additionalModel = new AdditionalNotificationSettingsModel(EnumHelper.GetCollectionByType(typeof(CommentType)),
                                      NotificationSettingsData.CommentString,
                                      Localization.Comments_Settings_,
                                      NotificationSettingsData.IsCommentsNotificationTurnedOn);
            }
            if (title.Equals(Localization.InterestingPosts))
            {
                additionalModel = new AdditionalNotificationSettingsModel(EnumHelper.GetCollectionByType(typeof(InterestingPostType)),
                                      NotificationSettingsData.InterestingPostString,
                                      Localization.InterestingPosts,
                                      NotificationSettingsData.IsInterestingPostsNotificationTurnedOn);
            }
            if (title.Equals(Localization.FriendsRequests))
            {
                additionalModel = new AdditionalNotificationSettingsModel(EnumHelper.GetCollectionByType(typeof(FriendRequestType)),
                                      NotificationSettingsData.FriendRequestString,
                                      Localization.FriendsRequests,
                                      NotificationSettingsData.IsFriendRequestsNotificationTurnedOn);
            }
            if (title.Equals(Localization.PhotoTags))
            {
                additionalModel = new AdditionalNotificationSettingsModel(EnumHelper.GetCollectionByType(typeof(PhotoTagType)),
                                      NotificationSettingsData.PhotoTagString,
                                      Localization.PhotoTags,
                                      NotificationSettingsData.IsPhotoTagsNotificationTurnedOn);
            }
            await OpenAdditionalNotificationPageAsync(additionalModel);
        }

        private async Task OpenAdditionalNotificationPageAsync(AdditionalNotificationSettingsModel model)
        {
            var result = await NavigationService.Navigate<AdditionalNotificationSettingsViewModel,
                                                          AdditionalNotificationSettingsModel,
                                                          DestructionResult<AdditionalNotificationSettingsModel>>(model);
            if (result != null && result.Destroyed)
            {
                SaveAdditionalNotificationSettingsResult(result.Entity);
            }
        }

        private void SaveAdditionalNotificationSettingsResult(AdditionalNotificationSettingsModel result)
        {
            

            var name = result.Title.Substring(0, result.Title.Length - 1).Replace(" ", string.Empty);

            NotificationSettingsData.GetType().GetProperties()
                .Where(prop => prop.PropertyType == typeof(string) && prop.Name.Contains(name))?
                .FirstOrDefault()?.SetValue(NotificationSettingsData, result.SelectedValue);

            NotificationSettingsData.GetType().GetProperties()
                .Where(prop => prop.PropertyType == typeof(bool) && prop.Name.Contains(name))?
                .FirstOrDefault()?.SetValue(NotificationSettingsData, result.IsNotificationTurned);

            NotificationSettingsData.NotifiPropertyChanges();
        }

        #endregion
    }
}
