using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSocial.Constants;
using XamarinSocial.Enums;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModels.Shared;
using XamarinSocial.ViewModels.TabsView;
using XamarinSocial.ViewModelsResult;
using XamarinSocial.Views.TabsView;
using static XamarinSocial.Constants.Constant;

namespace XamarinSocial.ViewModels.ProfileSet
{
    public class UserProfileViewModel : BaseViewModel<AuthProviderProfile>
    {
        private ProfileSetup _profile { get; set; }
        private readonly ICloseApplicationService _closeApplicationService;
        public UserProfileViewModel(IMvxNavigationService navigationService, ICloseApplicationService closeApplicationService) : base(navigationService)
        {
            _profile = new ProfileSetup();

            SetupView();
            _closeApplicationService = closeApplicationService;
        }

        public override void Prepare(AuthProviderProfile parameter)
        {
            if (parameter == null)
            {
                return;
            }
            _profile.Email = parameter.Email;
            _profile.FirstName = parameter.FirstName;
            _profile.LastName = parameter.LastName;
            _profile.UserName = $"{_profile.FirstName} {_profile.LastName}";
            UserName = $"{_profile.FirstName} {_profile.LastName}";
        }

        #region Properties

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _authUserName;

        public string AuthUserName
        {
            get => _authUserName;
            set => SetProperty(ref _authUserName, value);
        }

        private int _minLookingAge;
        public int MinLookingAge
        {
            get => _minLookingAge;
            set => SetProperty(ref _minLookingAge, value);
        }



        private int _maxLookingAge;
        public int MaxLookingAge
        {
            get => _maxLookingAge;
            set => SetProperty(ref _maxLookingAge, value);
        }

        private double _iAmFemaleOpacity;
        public double IAmFemaleOpacity
        {
            get => _iAmFemaleOpacity;
            set => SetProperty(ref _iAmFemaleOpacity, value);
        }

        private double _iAmMaleOpacity;
        public double IAmMaleOpacity
        {
            get => _iAmMaleOpacity;
            set => SetProperty(ref _iAmMaleOpacity, value);
        }

        private double _iAmOtherOpacity;
        public double IAmOtherOpacity
        {
            get => _iAmOtherOpacity;
            set => SetProperty(ref _iAmOtherOpacity, value);
        }
        private double _lookingForFemaleOpacity;
        public double LookingForFemaleOpacity
        {
            get => _lookingForFemaleOpacity;
            set => SetProperty(ref _lookingForFemaleOpacity, value);
        }
        private double _lookingForMaleOpacity;
        public double LookingForMaleOpacity
        {
            get => _lookingForMaleOpacity;
            set => SetProperty(ref _lookingForMaleOpacity, value);
        }

        private double _lookingForOtherOpacity;
        public double LookingForOtherOpacity
        {
            get => _lookingForOtherOpacity;
            set => SetProperty(ref _lookingForOtherOpacity, value);
        }

        private GooglePlaceAutoCompletePrediction _currentSearchLocation;
        public GooglePlaceAutoCompletePrediction CurrentSearchLocation
        {
            get => _currentSearchLocation;
            set
            {
                _profile.GoogleLocationId = value.PlaceId;
                _profile.Location = $"{value.StructuredFormatting.MainText} ,{value.StructuredFormatting.SecondaryText}";
                SearchLocation = value.StructuredFormatting.MainText;
                SetProperty(ref _currentSearchLocation, value);
            }
        }

        private string _searchLocation;
        public string SearchLocation
        {
            get => _searchLocation;
            set => SetProperty(ref _searchLocation, value);
        }

        private bool _isAutoCompleteVisible;
        public bool IsAutoCompleteVisible
        {
            get => _isAutoCompleteVisible;
            set => SetProperty(ref _isAutoCompleteVisible, value);
        }

        private bool _isFirstPhotoButtonVisible;

        public bool IsFirstPhotoButtonVisible
        {
            get => _isFirstPhotoButtonVisible;
            set => SetProperty(ref _isFirstPhotoButtonVisible, value);
        }

        private bool _isChangePhotoButtonVisible;
        public bool IsChangePhotoButtonVisible
        {
            get => _isChangePhotoButtonVisible;
            set => SetProperty(ref _isChangePhotoButtonVisible, value);
        }

        private ImageSource _uploadedImageSource;
        public ImageSource UploadedImageSource
        {
            get => _uploadedImageSource;
            set
            {
                if (value != null)
                {
                    IsVisiblePhotoFrame = false;
                }
                SetProperty(ref _uploadedImageSource, value);
            }
        }

        private string _uploadedImageBase64;
        public string UploadedImageBase64
        {
            get => _uploadedImageBase64;
            set => SetProperty(ref _uploadedImageBase64, value);
        }

        private bool _isVisiblePhotoFrame;
        public bool IsVisiblePhotoFrame
        {
            get => _isVisiblePhotoFrame;
            set => SetProperty(ref _isVisiblePhotoFrame, value);
        }

        #endregion

        #region Commands
        public IMvxCommand IAmFemaleCommand => new MvxCommand(SetFemaleGender);
        public IMvxCommand IAmMaleCommand => new MvxCommand(SetMaleGender);
        public IMvxCommand IAmOtherCommand => new MvxCommand(SetOtherGender);
        public IMvxCommand LookingForFemaleCommand => new MvxCommand(GetLookingForFemaleGender);
        public IMvxCommand LookingForMaleCommand => new MvxCommand(GetLookingForMaleGender);
        public IMvxCommand LookingForOtherCommand => new MvxCommand(GetLookingForOtherGender);
        public IMvxCommand FirstViewBackCommand => new MvxCommand(GetLookingForOtherGender);//TODO
        public IMvxCommand SkipCommand => new MvxCommand(GetLookingForOtherGender);//TODO
        public IMvxCommand AppCloseCommand => new MvxCommand(CloseApplication);
        public IMvxAsyncCommand GoToMainTabbedViewAsyncCommand => new MvxAsyncCommand(GoToTabbedViewAsync);

        public IMvxAsyncCommand NavigateToSearchLocationViewCommandAsync => new MvxAsyncCommand(NavigateToSearchLocationViewAsync);

        #endregion

        #region Methods

        private async Task GoToTabbedViewAsync()
        {
            await NavigationService.Navigate<MainTabbedViewModel>();
        }

        private async Task NavigateToSearchLocationViewAsync()
        {
            var result = await NavigationService.Navigate<SearchLocationViewModel, GooglePlaceAutoCompletePrediction, DestructionResult<GooglePlaceAutoCompletePrediction>>(new GooglePlaceAutoCompletePrediction());
            if (result != null && result.Destroyed)
            {
                CurrentSearchLocation = result.Entity;
            }
        }

        private void CloseApplication()
        {
            _closeApplicationService.CloseApplication();
        }

        private void SetFemaleGender()
        {
            SetAllIAmGenderHalfOpacity();
            IAmFemaleOpacity = Numeric.GenderButtonFullOpacity;
            _profile.UserGender = Gender.Female;
        }

        private void SetMaleGender()
        {
            SetAllIAmGenderHalfOpacity();
            IAmMaleOpacity = Numeric.GenderButtonFullOpacity;
            _profile.UserGender = Gender.Male;
        }

        private void SetOtherGender()
        {
            SetAllIAmGenderHalfOpacity();
            IAmOtherOpacity = Numeric.GenderButtonFullOpacity;
            _profile.UserGender = Gender.Female;
        }

        private void GetLookingForFemaleGender()
        {
            SetAllLookingForGenderHalfOpacity();
            LookingForFemaleOpacity = Numeric.GenderButtonFullOpacity;
            _profile.LookingForGender = Gender.Female;
        }

        private void GetLookingForMaleGender()
        {
            SetAllLookingForGenderHalfOpacity();
            LookingForMaleOpacity = Numeric.GenderButtonFullOpacity;
            _profile.LookingForGender = Gender.Male;
        }

        private void GetLookingForOtherGender()
        {
            SetAllLookingForGenderHalfOpacity();
            LookingForOtherOpacity = Numeric.GenderButtonFullOpacity;
            _profile.LookingForGender = Gender.Other;
        }

        private void SetAllIAmGenderHalfOpacity()
        {
            IAmFemaleOpacity = Numeric.GenderButtonHalfOpacity;
            IAmMaleOpacity = Numeric.GenderButtonHalfOpacity;
            IAmOtherOpacity = Numeric.GenderButtonHalfOpacity;
        }

        private void SetAllLookingForGenderHalfOpacity()
        {
            LookingForFemaleOpacity = Numeric.GenderButtonHalfOpacity;
            LookingForMaleOpacity = Numeric.GenderButtonHalfOpacity;
            LookingForOtherOpacity = Numeric.GenderButtonHalfOpacity;
        }

        private void SetupView()
        {
            MinLookingAge = Numeric.LookingForLowAge;
            MaxLookingAge = Numeric.LookingForHighAge;

            IAmFemaleOpacity = Numeric.GenderButtonFullOpacity;
            IAmMaleOpacity = Numeric.GenderButtonHalfOpacity;
            IAmOtherOpacity = Numeric.GenderButtonHalfOpacity;

            LookingForFemaleOpacity = Numeric.GenderButtonFullOpacity;
            LookingForMaleOpacity = Numeric.GenderButtonHalfOpacity;
            LookingForOtherOpacity = Numeric.GenderButtonHalfOpacity;

            IsChangePhotoButtonVisible = true;
            IsFirstPhotoButtonVisible = true;
            IsVisiblePhotoFrame = true;
        }

        public async Task GetImageFromLibraryAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                CompressionQuality = 50
            });
            if (!(file is null))
            {
                InvokeOnMainThread(() =>
                {
                    UploadedImageSource = ImageSource.FromFile(file.Path);
                });
                IsFirstPhotoButtonVisible = false;
                IsChangePhotoButtonVisible = true;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);

                    _uploadedImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public async Task GetImageFromCameraAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable && !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                DefaultCamera = CameraDevice.Front,
                SaveToAlbum = true,
                Directory = Constant.MediaConfig.MEDIA_DIRECTORY,
                Name = $"{DateTime.Now.ToString(Constant.MediaConfig.IMAGE_SAVING_FORMAT)}{Constant.MediaConfig.IMAGE_EXTENSION}"
            });

            if (!(file is null))
            {
                InvokeOnMainThread(() =>
                {
                    UploadedImageSource = ImageSource.FromFile(file.Path);
                });
                IsFirstPhotoButtonVisible = false;
                IsChangePhotoButtonVisible = true;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);

                    _uploadedImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
        #endregion
    }
}
