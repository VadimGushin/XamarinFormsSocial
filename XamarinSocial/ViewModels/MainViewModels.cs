using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using XamarinSocial.Constants;
using XamarinSocial.Extensions;
using XamarinSocial.Models;
using XamarinSocial.Models.Chat;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModels.Chat;
using XamarinSocial.ViewModels.ProfileSet;
using XamarinSocial.ViewModels.SignUp;
using XamarinSocial.ViewModels.TabsView;

namespace XamarinSocial.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ISecureStorageService _secureStorageService;
        public MainViewModel(IMvxNavigationService navigationService, ISecureStorageService secureStorageService) : base(navigationService)
        {
            ShowFirstViewModelCommand = new MvxAsyncCommand(async () => await ShowFirstPageAsync());
            _secureStorageService = secureStorageService;
        }

        public IMvxCommand ShowFirstViewModelCommand { get; private set; }

        public async override void ViewAppeared()
        {
            base.ViewAppeared();
        }

        private async Task ShowFirstPageAsync()
        {
            if (!string.IsNullOrWhiteSpace(await _secureStorageService.GetStringByKey(Constant.StorageConfig.SequreKeyForLoged)))
            {
                string firstUserName = await _secureStorageService.GetStringByKey(Constant.StorageConfig.UserFirstName);
                string lastUserName = await _secureStorageService.GetStringByKey(Constant.StorageConfig.UserLastName);
                string userEmail = await _secureStorageService.GetStringByKey(Constant.StorageConfig.UserEmail);

                var userProfile = new AuthProviderProfile()
                {
                    FirstName = firstUserName,
                    LastName = lastUserName,
                    Email = userEmail
                };
                //await NavigationService.Navigate<UserProfileViewModel, AuthProviderProfile>(userProfile);
                await NavigationService.Navigate<MainTabbedViewModel>();
                
                return;
            }

            //await NavigationService.Navigate<SignUpViewModel>();

            await NavigationService.Navigate<MainTabbedViewModel>();

            /*using (_userDialogs.Loading())
            {
                if (!CheckConnectionStatus())
                {
                    await _navigationService.Navigate<StandaloneViewModel>();
                    return;
                }
                var initResult = await _localDataService.InitDataAsync();
                if (initResult)
                {
                    await GetInviteDataAsync();
                    //await _navigationService.Navigate<WelcomeViewModel>();
                    return;
                }
                if (!_localDataService.UploadingResultModel.IsAgreementConfirm)
                {
                    await _navigationService.Navigate<UserAgreementViewModel>();
                    return;
                }
                var userData = await _localDataService.GetUserDataAsync();
                if (!userData.IsFull)
                {
                    _localDataService.ClearStorage();
                    await _localDataService.InitDataAsync();
                    //await _localDataService.SetUserAgreementAsync();
                    await _navigationService.Navigate<PersonalDataViewModel>();
                    return;
                }
                var userPhotos = await _localDataService.GetAllPhotosAsync();
                if (userPhotos.Where(x => !x.IsFull).FirstOrDefault() != null)
                {
                    await _navigationService.Navigate<TakePhotoViewModel>();
                    return;
                }
                await _navigationService.Navigate<ReviewDataViewModel>();
            }*/
        }

    }
}
