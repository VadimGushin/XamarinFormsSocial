using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinSocial.ViewModels.TabsView
{
    public class MainTabbedViewModel : BaseViewModel
    {
        #region Properties

        private bool _isFirstTime = true;

        #endregion  Properties
        public MainTabbedViewModel(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {

        }

        #region Methods

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
               NavigationService.Navigate<HomeViewModel>(),
               NavigationService.Navigate<SearchUsersViewModel>(),
               NavigationService.Navigate<ChatViewModel>(),
               NavigationService.Navigate<ProfileViewModel>(),
               NavigationService.Navigate<NotificationViewModel>()
            };
            return Task.WhenAll(tasks);
        }

        public override void ViewAppearing()
        {
            if (_isFirstTime)
            {
                ShowInitialViewModels();
                _isFirstTime = false;
            }
        }

        #endregion Methods

    }
}
