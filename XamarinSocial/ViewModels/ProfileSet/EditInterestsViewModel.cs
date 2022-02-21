using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.ViewModels.ProfileSet
{
    public class EditInterestsViewModel : BaseViewModel
    {
        #region Services

        private readonly IProfileService _profileService;

        #endregion

        public EditInterestsViewModel(IMvxNavigationService navigationService,
                                      IProfileService profileService)
                                      : base(navigationService)
        {
            _profileService = profileService;
            InitControls();
        }

        #region Properties

        private MvxObservableCollection<string> _interests;
        public MvxObservableCollection<string> Interests
        {
            get => _interests;
            set => SetProperty(ref _interests, value);
        }

        private string _newTagText;
        public string NewTagText
        {
            get => _newTagText;
            set => SetProperty(ref _newTagText, value);
        }

        private UserProfile _userProfile { get; set; }

        #endregion

        #region Commands

        public IMvxCommand ClosePageCommand { get; private set; }
        public IMvxCommand EditInterestsCommand { get; private set; }
        public IMvxCommand RemoveInterestCommand { get; private set; }
        public IMvxCommand AddTagCommand { get; private set; }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            _userProfile = _profileService.GetByIdAsync().Result;
            Interests = new MvxObservableCollection<string>(_userProfile.Interests);

            ClosePageCommand = new MvxAsyncCommand(ClosePageAsync);
            EditInterestsCommand = new MvxAsyncCommand(EditInterestsAsync);
            RemoveInterestCommand = new MvxCommand<string>(RemoveInterestAsync);
            AddTagCommand = new MvxCommand(AddTagAsync);
        }

        private async Task ClosePageAsync()
        {
            await NavigationService.Close(this);
        }

        private async Task EditInterestsAsync()
        {
            _userProfile.Interests = Interests.ToList();
            await _profileService.SaveAsync(_userProfile);
            await ClosePageAsync();
        }

        private void RemoveInterestAsync(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                Interests.Remove(tag);
            }
        }

        private void AddTagAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewTagText))
            {
                Interests.Add(NewTagText);
                NewTagText = string.Empty;
            }

        }

        #endregion
    }
}
