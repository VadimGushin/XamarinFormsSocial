using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSocial.Interfaces;
using XamarinSocial.Resources.Strings;

namespace XamarinSocial.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel, IBackViewModel, ICollectionViewModel
    {
        public readonly IMvxNavigationService NavigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
            BackCommand = new MvxAsyncCommand(async () => await TryBackPage());
            RefreshCommand = new MvxAsyncCommand(RefreshAsync);
        }

        #region Properties

        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        #endregion

        #region Commands

        public virtual IMvxCommand BackCommand { get; set; }
        public IMvxAsyncCommand RefreshCommand { get; private set; }

        #endregion

        #region VirtualMethods

        protected virtual async Task TryBackPage()
        {
            try
            {
                if (((NavigationPage)(Application.Current.MainPage)).Pages.Count() > 1)
                {
                    await NavigationService.Close(this);
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BaseViewModel.TryBackPage error: {ex.Message}");
            }
        }

        protected virtual async Task RefreshAsync()
        {
            IsRefreshing = false;
        }

        public virtual async Task OnItemSelectedAsync(string id)
        {
        }

        public virtual async Task LoadMoreAsync()
        {           
        }

        #endregion
    }

    public abstract class BaseViewModel<T> : MvxViewModel<T>, IBackViewModel, ICollectionViewModel
    {
        public readonly IMvxNavigationService NavigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
            BackCommand = new MvxAsyncCommand(async () => await TryBackPage());
            RefreshCommand = new MvxAsyncCommand(RefreshAsync);
        }

        #region Properties

        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        #endregion

        #region Commands

        public virtual IMvxCommand BackCommand { get; set; }
        public IMvxAsyncCommand RefreshCommand { get; private set; }

        #endregion

        #region VirtualMethods

        protected virtual async Task TryBackPage()
        {
            try
            {
                if (((NavigationPage)(Application.Current.MainPage)).Pages.Count() > 1)
                {
                    await NavigationService.Close(this);
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BaseViewModel.TryBackPage error: {ex.Message}");
            }
        }

        protected virtual async Task RefreshAsync()
        {
            IsRefreshing = false;
        }

        public virtual async Task OnItemSelectedAsync(string id)
        {
        }

        public virtual async Task LoadMoreAsync()
        {
        }

        #endregion
    }

    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>, IBackViewModel, ICollectionViewModel
        where TParameter : class
        where TResult : class
    {
        public readonly IMvxNavigationService NavigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
            BackCommand = new MvxAsyncCommand(async () => await TryBackPage());
            RefreshCommand = new MvxAsyncCommand(RefreshAsync);
        }

        #region Properties

        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        #endregion

        #region Commands

        public virtual IMvxCommand BackCommand { get; set; }
        public IMvxAsyncCommand RefreshCommand { get; private set; }

        #endregion

        #region VirtualMethods

        protected virtual async Task TryBackPage()
        {
            try
            {
                if (((NavigationPage)(Application.Current.MainPage)).Pages.Count() > 1)
                {
                    await NavigationService.Close(this);
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BaseViewModel.TryBackPage error: {ex.Message}");
            }
        }

        protected virtual async Task RefreshAsync()
        {
            IsRefreshing = false;
        }

        public virtual async Task OnItemSelectedAsync(string id)
        {
        }

        public virtual async Task LoadMoreAsync()
        {
        }

        #endregion

    }

    public abstract class BaseResultViewModel<T> : MvxViewModel, IMvxViewModelResult<T>, IBackViewModel, ICollectionViewModel
    {
        public readonly IMvxNavigationService NavigationService;
        protected BaseResultViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
            BackCommand = new MvxAsyncCommand(async () => await TryBackPage());
            RefreshCommand = new MvxAsyncCommand(RefreshAsync);
        }

        #region Properties

        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        #endregion

        #region Commands

        public virtual IMvxCommand BackCommand { get; set; }
        public IMvxAsyncCommand RefreshCommand { get; private set; }

        #endregion        

        #region VirtualMethods

        protected virtual async Task TryBackPage()
        {
            try
            {
                if (((NavigationPage)(Application.Current.MainPage)).Pages.Count() > 1)
                {
                    await NavigationService.Close(this);
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BaseViewModel.TryBackPage error: {ex.Message}");
            }
        }

        protected virtual async Task RefreshAsync()
        {
            IsRefreshing = false;
        }

        public virtual async Task OnItemSelectedAsync(string id)
        {
        }

        public virtual async Task LoadMoreAsync()
        {
        }

        #endregion

        #region Overrides

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
            {
                return;
            }          

            base.ViewDestroy(viewFinishing);
        }

        #endregion

    }
}
