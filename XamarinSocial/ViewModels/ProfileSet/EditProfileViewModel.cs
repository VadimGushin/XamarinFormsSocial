using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinSocial.Enums;
using XamarinSocial.Models;
using XamarinSocial.Models.Common;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModels.Shared;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.ProfileSet
{
    public class EditProfileViewModel : BaseViewModel
    {
        #region Services

        private readonly IProfileService _profileService;
        private readonly IUserDialogs _userDialogs;

        #endregion

        #region Variables

        #endregion

        public EditProfileViewModel(IMvxNavigationService navigationService,
                                    IProfileService profileService, IUserDialogs userDialogs)
                                    : base(navigationService)
        {
            _profileService = profileService;
            InitControls();
            _userDialogs = userDialogs;
        }

        #region Properties

        private UserProfile _profile;
        public UserProfile Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        private bool _isValidName;
        public bool IsValidName
        {
            get => _isValidName;
            set
            {
                SetProperty(ref _isValidName, value);
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                SetProperty(ref _location, value);
            }
        }

        private List<string> _currentDropDownList;
        public List<string> CurrentDropDownList
        {
            get => _currentDropDownList;
            set => SetProperty(ref _currentDropDownList, value);
        }

        public string NameValidationPattern { get; set; } //can get from constants
        public string ImageCountText { get; set; }

        #endregion

        #region Commands

        public IMvxCommand ClosePageCommand { get; private set; }
        public IMvxCommand EditProfileCommand { get; private set; }
        public IMvxCommand ChangeAvatarCommand { get; private set; }
        public IMvxCommand NavigateToSearchLocationViewCommand { get; private set; }
        public IMvxCommand AddInterestsCommand { get; private set; }
        public IMvxCommand OpenAlbumCommand { get; private set; }
        public IMvxCommand SelectedDropdownItemCommand { get; private set; }


        #endregion

        #region Override Methods

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            Profile = await _profileService.GetByIdAsync();
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            ClosePageCommand = new MvxAsyncCommand(ClosePageAsync);
            EditProfileCommand = new MvxAsyncCommand(EditProfileAsync);
            ChangeAvatarCommand = new MvxAsyncCommand(ChangeAvatarAsync);
            NavigateToSearchLocationViewCommand = new MvxAsyncCommand(OpenSearchLocationViewAsync);
            AddInterestsCommand = new MvxAsyncCommand(EditInterestsAsync);
            OpenAlbumCommand = new MvxAsyncCommand(OpenAlbumAsync);
            SelectedDropdownItemCommand = new MvxAsyncCommand<PopupResponseModel>(async (param) => await OnSelectedDropdownItemAsync(param));

            Profile = _profileService.GetByIdAsync().Result;
            NameValidationPattern = @"^[a-zA-Z]+$";
            ImageCountText = $"{Profile.Interests.Count} {Resources.Strings.Strings.Photos.ToLower()} ";
            Location = Profile.Location;
        }

        private async Task ClosePageAsync()
        {
            await NavigationService.Close(this);
        }

        private async Task EditProfileAsync()
        {
            _userDialogs.Toast("Save Profile Clicked");
        }

        private async Task ChangeAvatarAsync()
        {
            _userDialogs.Toast("Change Avatar Clicked");
        }

        private async Task OpenSearchLocationViewAsync()
        {
            var locationResult = await NavigationService.Navigate<SearchLocationViewModel, GooglePlaceAutoCompletePrediction, DestructionResult<GooglePlaceAutoCompletePrediction>>(new GooglePlaceAutoCompletePrediction());
            if (locationResult != null && locationResult.Destroyed)
            {
                Profile.GoogleLocationId = locationResult.Entity.PlaceId;
                Profile.Location = $"{locationResult.Entity.StructuredFormatting.MainText} ,{locationResult.Entity.StructuredFormatting.SecondaryText}";

                Location = $"{locationResult.Entity.StructuredFormatting.MainText} ,{locationResult.Entity.StructuredFormatting.SecondaryText}";
            }
        }

        private async Task EditInterestsAsync()
        {
            await NavigationService.Navigate<EditInterestsViewModel>();
        }

        private async Task OpenAlbumAsync()
        {
            await NavigationService.Navigate<ImageAlbumViewModel, string>(Profile.Id);
        }

        private async Task OnSelectedDropdownItemAsync(PopupResponseModel responseModel)
        {
            if (responseModel != null && !string.IsNullOrWhiteSpace(responseModel.SelectedValue))
            {
                Profile.GetType().GetProperty(responseModel.SourceType.ToString()).SetValue(Profile, responseModel.SelectedValue);
            }         
        }

        #endregion

    }
}
