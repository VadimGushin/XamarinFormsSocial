using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSocial.Constants;
using XamarinSocial.Enums;
using XamarinSocial.Helpers.Interfaces;
using XamarinSocial.Models;
using XamarinSocial.Models.Api.Requests;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Common;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ValidationAttributes;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels
{
    public class CreatePostViewModel : BaseResultViewModel<DestructionResult<bool>>
    {
        #region Variables

        private GooglePlaceReverseGeocodingResult _geocodingResult;

        #endregion Variables

        #region Services

        private readonly ILocalMediaService _localMediaService;
        private readonly INetworkMediaService _networkMediaService;
        private readonly IDisplayAlertService _displayAlertService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly IValidationHelper _validationHelper;
        private readonly IDeviceLocationLocalService _deviceLocationLocalService;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        private readonly IFeedService _feedService;

        #endregion Services

        #region Properties

        private ObservableCollection<LocalMediaModel> _sources;

        [MediaSourceValidationAttribute]
        public ObservableCollection<LocalMediaModel> Sources
        {
            get => _sources;
            set
            {
                SetProperty(ref _sources, value);
            }
        }

        private string _profileImageSource;
        public string ProfileImageSource
        {
            get => _profileImageSource;
            set
            {
                SetProperty(ref _profileImageSource, value);
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                SetProperty(ref _description, value);
            }
        }

        /// <summary>
        /// Locality, etc.
        /// </summary>
        private string _pinnedSecondLevelArea;
        public string PinnedSecondLevelArea
        {
            get => _pinnedSecondLevelArea;
            set
            {
                SetProperty(ref _pinnedSecondLevelArea, value);
            }
        }

        /// <summary>
        /// Country
        /// </summary>
        private string _pinnedFirstLevelArea;
        public string PinnedFirstLevelArea
        {
            get => _pinnedFirstLevelArea;
            set
            {
                SetProperty(ref _pinnedFirstLevelArea, value);
            }
        }

        private bool _isLocationPinned;
        public bool IsLocationPinned
        {
            get => _isLocationPinned;
            set
            {
                SetProperty(ref _isLocationPinned, value);
            }
        }

        #endregion Properties

        #region Constructors

        public CreatePostViewModel(IMvxNavigationService navigationService,
            ILocalMediaService localMediaService,
            INetworkMediaService networkMediaService,
            IDisplayAlertService displayAlertService,
            ISecureStorageService secureStorageService,
            IValidationHelper validationHelper,
            IDeviceLocationLocalService deviceLocationLocalService,
            IGoogleMapsApiService googleMapsApiService,
            IFeedService feedService)
            : base(navigationService)
        {
            Sources = new ObservableCollection<LocalMediaModel>();

            _localMediaService = localMediaService;
            _networkMediaService = networkMediaService;
            _displayAlertService = displayAlertService;
            _secureStorageService = secureStorageService;
            _validationHelper = validationHelper;
            _deviceLocationLocalService = deviceLocationLocalService;
            _googleMapsApiService = googleMapsApiService;
            _feedService = feedService;
        }

        #endregion

        #region Commands

        private IMvxAsyncCommand _cancelCreatePostCommandAsync;
        public IMvxAsyncCommand CancelCreatePostCommandAsync
        {
            get
            {
                _cancelCreatePostCommandAsync = _cancelCreatePostCommandAsync ?? new MvxAsyncCommand(OnCancelCreatePost);
                return _cancelCreatePostCommandAsync;
            }
        }

        private IMvxAsyncCommand _confirmCreatePostCommandAsync;
        public IMvxAsyncCommand ConfirmCreatePostCommandAsync
        {
            get
            {
                _confirmCreatePostCommandAsync = _confirmCreatePostCommandAsync ?? new MvxAsyncCommand(OnConfirmCreatePost);
                return _confirmCreatePostCommandAsync;
            }
        }

        private IMvxAsyncCommand<LocalMediaModel> _deleteSourceCommandAsync;
        public IMvxAsyncCommand<LocalMediaModel> DeleteSourceCommandAsync
        {
            get
            {
                _deleteSourceCommandAsync = _deleteSourceCommandAsync ?? new MvxAsyncCommand<LocalMediaModel>(OnDeleteSource);
                return _deleteSourceCommandAsync;
            }
        }

        private IMvxAsyncCommand _getVideoFromLibraryCommandAsync;
        public IMvxAsyncCommand GetVideoFromLibraryCommandAsync
        {
            get
            {
                _getVideoFromLibraryCommandAsync = _getVideoFromLibraryCommandAsync ?? new MvxAsyncCommand(GetVideoFromLibraryAsync);
                return _getVideoFromLibraryCommandAsync;
            }
        }

        private IMvxAsyncCommand _getVideoFromCameraCommandAsync;
        public IMvxAsyncCommand GetVideoFromCameraCommandAsync
        {
            get
            {
                _getVideoFromCameraCommandAsync = _getVideoFromCameraCommandAsync ?? new MvxAsyncCommand(GetVideoFromCameraAsync);
                return _getVideoFromCameraCommandAsync;
            }
        }

        private IMvxAsyncCommand _getImageFromLibraryCommandAsync;
        public IMvxAsyncCommand GetImageFromLibraryCommandAsync
        {
            get
            {
                _getImageFromLibraryCommandAsync = _getImageFromLibraryCommandAsync ?? new MvxAsyncCommand(GetImageFromLibraryAsync);
                return _getImageFromLibraryCommandAsync;
            }
        }

        private IMvxAsyncCommand _getImageFromCameraCommandAsync;
        public IMvxAsyncCommand GetImageFromCameraCommandAsync
        {
            get
            {
                _getImageFromCameraCommandAsync = _getImageFromCameraCommandAsync ?? new MvxAsyncCommand(GetImageFromCameraAsync);
                return _getImageFromCameraCommandAsync;
            }
        }

        private IMvxAsyncCommand _getCurrentLocationCommandAsync;
        public IMvxAsyncCommand GetCurrentLocationCommandAsync
        {
            get
            {
                _getCurrentLocationCommandAsync = _getCurrentLocationCommandAsync ?? new MvxAsyncCommand(GetCurrentLocationAsync);
                return _getCurrentLocationCommandAsync;
            }
        }

        private IMvxCommand _removePinnedLocationCommand;
        public IMvxCommand RemovePinnedLocationCommand
        {
            get
            {
                _removePinnedLocationCommand = _removePinnedLocationCommand ?? new MvxCommand(OnRemovePinnedLocation);
                return _removePinnedLocationCommand;
            }
        }

        #endregion Commands

        #region Overrides

        public override async Task Initialize()
        {
            ProfileImageSource = await _secureStorageService.GetStringByKey(Constant.StorageConfig.UserProfileImageSource);
        }

        #endregion Overrides

        #region Methods

        private async Task OnCancelCreatePost()
        {
            await NavigationService.Close(this, new DestructionResult<bool>() {Entity = false, Destroyed = true });
        }

        private async Task OnConfirmCreatePost()
        {
            bool isMediaCountValid = _validationHelper.ValidateProperty(Sources, nameof(Sources), this);

            if (!isMediaCountValid)
            {
                _displayAlertService.ShowToast("No media selected");
                return;
            }

            ResponseModel<List<UploadedMediaModel>> uploadResult = await _networkMediaService.UploadMedia(Sources);
            if (!uploadResult.IsSucceed)
            {
                _displayAlertService.ShowToast("an error ocurred while uploading media");
                return;
            }

            var createPostModel = new CreatePostModel() 
            { 
                Description = Description, 
                MediaSources = uploadResult.Data, 
            };

            if (!string.IsNullOrEmpty(PinnedFirstLevelArea))
            {
                createPostModel.PinnedLocation = new PinnedLocationModel()
                {
                    FirstLevelArea = PinnedFirstLevelArea,
                    SecondLevelArea = PinnedSecondLevelArea
                };
            }

            ResponseModel createPostResult = await _feedService.CreatePost(createPostModel);
            if (!createPostResult.IsSucceed)
            {
                string errorMessage = createPostResult.Errors.FirstOrDefault();
                _displayAlertService.ShowToast(errorMessage);
                return;
            }

            await NavigationService.Close(this, new DestructionResult<bool>() { Entity = true, Destroyed = true });
        }

        private async Task OnDeleteSource(LocalMediaModel source)
        {
            Sources.Remove(source);
        }

        private void OnRemovePinnedLocation()
        {
            PinnedFirstLevelArea = string.Empty;
            PinnedSecondLevelArea = string.Empty;
            IsLocationPinned = false;

            _geocodingResult = null;
        }

        private async Task GetCurrentLocationAsync()
        {
            LocalDeviceLocationFetchResult location = await _deviceLocationLocalService.GetCurrentDeviceLocation();
            if (!location.IsSucceed)
            {
                _displayAlertService.ShowToast(location.ErrorMessage);
                return;
            }

            ResponseModel<GooglePlaceReverseGeocodingResult> result = await _googleMapsApiService.GetReverseGeocodingData(location.Location);
            if (!result.IsSucceed)
            {
                return;
            }

            _geocodingResult = result.Data;
            if (!_geocodingResult.Result.Any())
            {
                _displayAlertService.ShowToast("Unable to retriev geolocation");
                return;
            }

            //get country
            GooglePlaceResult countryPart = _geocodingResult.Result.FirstOrDefault(address => address.AdressComponents != null &&
                                    address.AdressComponents.FirstOrDefault(component => component.Types != null && component.Types.Contains("country")) != null);

            if (countryPart == null)
            {
                _displayAlertService.ShowToast("Unable to retriev location");
                return;
            }

            AddressComponent countyComponent = countryPart.AdressComponents.FirstOrDefault(component => component.Types != null && component.Types.Contains("country"));
            PinnedFirstLevelArea = countyComponent.LongName;
            IsLocationPinned = true;

            //get locality
            GooglePlaceResult localityPart = _geocodingResult.Result.FirstOrDefault(address => address.AdressComponents != null &&
                                    address.AdressComponents.FirstOrDefault(component => component.Types != null && component.Types.Contains("locality")) != null);

            if (localityPart != null)
            {
                AddressComponent localityComponent = localityPart.AdressComponents.FirstOrDefault(component => component.Types != null && component.Types.Contains("locality"));
                PinnedSecondLevelArea = localityComponent.LongName;

                return;
            }

            //if locality not found - get first political adress component
            localityPart = _geocodingResult.Result.FirstOrDefault(address => address.AdressComponents != null);
            AddressComponent addressComponent = localityPart.AdressComponents.FirstOrDefault(component => component.Types != null && component.Types.Contains("political"));
            if (addressComponent == null)
            {
                PinnedSecondLevelArea = string.Empty;
                return;
            }

            PinnedSecondLevelArea = addressComponent.LongName;
        }

        //todo: move media functionality to service
        private async Task GetVideoFromLibraryAsync()
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                return;
            }

            int existingVideoSources = GetMediaSourcesCountByContentType(Sources, ContentType.Video);
            if (existingVideoSources >= Constant.MediaConfig.MAX_ATTACHED_VIDEOS_COUNT)
            {
                _displayAlertService.ShowToast($"Post can contains up to {Constant.MediaConfig.MAX_ATTACHED_VIDEOS_COUNT} video(s) at a time");
                return;
            }

            MediaFile file = await CrossMedia.Current.PickVideoAsync(new CancellationToken());
            if (file is null)
            {
                return;
            }

            ImageSource thumbImageSource = _localMediaService.GenerateThumbImage(file.Path, 0);
            UpdateSources(new LocalMediaModel()
            {
                ImageSource = thumbImageSource,
                ContentType = Enums.ContentType.Video,
                MediaFile = file
            });
        }

        private async Task GetVideoFromCameraAsync()
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                return;
            }

            int existingVideoSources = GetMediaSourcesCountByContentType(Sources, Enums.ContentType.Video);
            if (existingVideoSources >= Constant.MediaConfig.MAX_ATTACHED_VIDEOS_COUNT)
            {
                _displayAlertService.ShowToast($"Post can contains up to {Constant.MediaConfig.MAX_ATTACHED_VIDEOS_COUNT} video(s) at a time");
                return;
            }

            var mediaOptions = new StoreVideoOptions()
            {
                DesiredLength = Constant.MediaConfig.MAX_VIDEO_RECORD_DURATION,
                Quality = VideoQuality.Medium,
                SaveToAlbum = true,
                Directory = Constant.MediaConfig.MEDIA_DIRECTORY,
                Name = $"{DateTime.Now.ToString(Constant.MediaConfig.VIDEO_SAVING_FORMAT)}{Constant.MediaConfig.VIDEO_EXTENSION}"
            };

            MediaFile file = await CrossMedia.Current.TakeVideoAsync(
              mediaOptions, new CancellationToken());

            if (file is null)
            {
                return;
            }

            ImageSource thumbImageSource = _localMediaService.GenerateThumbImage(file.Path, 0);
            UpdateSources(new LocalMediaModel()
            {
                ImageSource = thumbImageSource,
                ContentType = Enums.ContentType.Video,
                MediaFile = file
            });
        }

        private async Task GetImageFromLibraryAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                CompressionQuality = 50
            });

            if (file is null)
            {
                return;
            }

            UpdateSources(new LocalMediaModel()
            {
                ImageSource = ImageSource.FromFile(file.Path),
                ContentType = Enums.ContentType.Image,
                MediaFile = file
            });
        }

        private async Task GetImageFromCameraAsync()
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

            if (file is null)
            {
                return;
            }

            UpdateSources(new LocalMediaModel()
            {
                ImageSource = ImageSource.FromFile(file.Path),
                ContentType = Enums.ContentType.Image,
                MediaFile = file
            });
        }

        private int GetMediaSourcesCountByContentType(IEnumerable<LocalMediaModel> sources, ContentType contentType)
        {
            int count = sources.Where(source => source.ContentType == contentType).Count();
            return count;
        }

        private void UpdateSources(LocalMediaModel localMedia)
        {
            LocalMediaModel mediaWithCurrentPath = Sources.FirstOrDefault(source => source.MediaFile.AlbumPath == localMedia.MediaFile.AlbumPath);
            if (mediaWithCurrentPath == null)
            {
                InvokeOnMainThread(() =>
                {
                    Sources.Add(localMedia);
                });
                return;
            }

            InvokeOnMainThread(() =>
            {
                Sources.Remove(mediaWithCurrentPath);
                Sources.Add(localMedia);
            });
        }

        #endregion Methods
    }
}