using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Models;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Photo;
using XamarinSocial.Services.Interfaces;
using LocalStrings = XamarinSocial.Resources.Strings.Strings;

namespace XamarinSocial.ViewModels.Shared
{
    public class ImageAlbumViewModel : BaseViewModel<string>
    {
        #region Services

        private readonly IFeedService _feedService;
        private readonly IUserDialogs _userDialogs;
        private readonly IImageSourceService _imageSourceService;

        #endregion

        public ImageAlbumViewModel(IMvxNavigationService navigationService,
                                   IFeedService feedService,
                                   IUserDialogs userDialogs,
                                   IImageSourceService imageSourceService)
                                   : base(navigationService)
        {
            _feedService = feedService;
            _userDialogs = userDialogs;
            _imageSourceService = imageSourceService;
            InitControls();
        }


        #region Properties

        private MvxObservableCollection<AlbumPhotoModel> _photos;
        public MvxObservableCollection<AlbumPhotoModel> Photos
        {
            get => _photos;
            set
            {
                SetProperty(ref _photos, value);
            }
        }

        private AlbumPhotoModel _selectedItem;
        public AlbumPhotoModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        private string _userId { get; set; }

        #endregion

        #region Commands

        public IMvxCommand ClosePageCommand { get; private set; }
        public IMvxCommand SaveChangesCommand { get; private set; }
        public IMvxAsyncCommand LoadMoreCommand { get; private set; }
        public IMvxCommand SelectionItemCommand { get; private set; }

        #endregion


        #region Overrides

        public async override void Prepare(string userId)
        {
            _userId = userId;
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            if (!string.IsNullOrWhiteSpace(_userId))
            {
                Photos.Clear();
                await LoadPhotos(Configuration.TakeAlbumPhotosCount, 0);
            }
        }

        protected async override Task RefreshAsync()
        {
            Photos.Clear();
            await LoadPhotos(Configuration.TakeAlbumPhotosCount, 0);
            await base.RefreshAsync();
        }

        public async override Task LoadMoreAsync()
        {
            await LoadPhotos(Configuration.TakeAlbumPhotosCount, Photos.Count);
            await base.LoadMoreAsync();
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            ClosePageCommand = new MvxAsyncCommand(ClosePageAsync);
            SaveChangesCommand = new MvxAsyncCommand(SaveChangesAsync);
            LoadMoreCommand = new MvxAsyncCommand(LoadMoreAsync);
            SelectionItemCommand = new MvxAsyncCommand<AlbumPhotoModel>(async (param) => await OnItemSelected(param));
            Photos = new MvxObservableCollection<AlbumPhotoModel>();
        }

        private async Task ClosePageAsync()
        {
            await NavigationService.Close(this);
        }

        private async Task SaveChangesAsync()
        {
            //await _profileService.SaveAsync(_userProfile);
            await ClosePageAsync();
        }

        private async Task LoadPhotos(int take, int offset)
        {
            //use _userId to get photos
            //ResponseModel<GetProfilePhotosModel> photosResponse = await _feedService.GetProfilePhotos(take, offset);
            //if (!photosResponse.IsSucceed || !photosResponse.Data.Photos.Any())
            //{
            //    return;
            //}
            //Photos.AddRange(photosResponse.Data.Photos);


            //HardCode
            var photos = new List<AlbumPhotoModel>();
            for (int i = 0; i < take; i++)
            {
                int id = Photos.Count + (Photos.Count > 0 ? 1 : 0) + i;
                bool test = (id == 0 ? false : true);
                photos.Add(new AlbumPhotoModel
                {
                    Id = id.ToString(),
                    IsPhotoItem = test,
                    Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg"
                });
            }

            Photos.AddRange(photos);
        }

        private async Task OnItemSelected(AlbumPhotoModel item)
        {
            //For item unselecting
            Task.Run(async () => {
                await Task.Delay(200);
                SelectedItem = null;
            });
           
            if (item == null)
            {
                return;
            }
            if (item.IsPhotoItem)
            {
                using (_userDialogs.Loading())
                {
                    //todo: open photo
                }
                return;
            }

            var imageSourceResult = new ImagesSourceModel();
            var result = await _userDialogs.ActionSheetAsync(LocalStrings.GetPhotoFrom_,
                                                 LocalStrings.Cancel, null, null,
                                                 new string[] { LocalStrings.Camera, LocalStrings.Library });
            if (result.Equals(LocalStrings.Cancel))
            {
                return;
            }
            
            if (result.Equals(LocalStrings.Camera))
            {
                imageSourceResult = await _imageSourceService.GetImageFromCameraAsync();
            }
            if (result.Equals(LocalStrings.Library))
            {
                imageSourceResult = await _imageSourceService.GetImageFromLibraryAsync();
            }

            //_userDialogs.ShowLoading();
            //todo: send image to server
            //_userDialogs.HideLoading();
            _userDialogs.Toast("Image added");
        }

        #endregion
    }
}
