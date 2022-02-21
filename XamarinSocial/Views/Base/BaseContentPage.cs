using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinSocial.Interfaces;
using XamarinSocial.ViewModels;

namespace XamarinSocial.Views.Base
{
    public class BaseContentPage<TViewModel> : MvxContentPage<TViewModel> where TViewModel : MvxViewModel
    {
        private static MvxContentPage<TViewModel> _currentPage;
        protected bool _isLoadingMore = false;

        #region IKeyboardResizedContentPage Variables
        public event EventHandler EntryUnfocused;
        public bool CancelsTouchesInView { get; set; } = false;
        #endregion

        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            _currentPage = this;
        }

        #region Public Properties

        /// <summary>
        /// Set visibility of native NavigationBar
        /// </summary>
        public bool HasNativeNavBar
        {
            get { return (bool)GetValue(HasNativeNavBarProperty); }
            set { SetValue(HasNativeNavBarProperty, value); }
        }

        /// <summary>
        /// Set visibility of BackButton at native NavigationBar
        /// </summary>
        public bool HasNativeBackButton
        {
            get { return (bool)GetValue(HasNativeBackButtonProperty); }
            set { SetValue(HasNativeBackButtonProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static BindableProperty HasNativeNavBarProperty = BindableProperty.Create(
                                        propertyName: nameof(HasNativeNavBar),
                                        returnType: typeof(bool),
                                        declaringType: typeof(BaseContentPage<TViewModel>),
                                        defaultValue: true,
                                        propertyChanged: OnHasNativeNavBarPropertyChanged);

        public static BindableProperty HasNativeBackButtonProperty = BindableProperty.Create(
                                        propertyName: nameof(HasNativeBackButton),
                                        returnType: typeof(bool),
                                        declaringType: typeof(BaseContentPage<TViewModel>),
                                        defaultValue: true,
                                        propertyChanged: OnHasNativeBackButtonPropertyChanged);

        #endregion

        #region Override Methods

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            (_currentPage?.ViewModel as IBackViewModel)?.BackCommand?.Execute();
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this is IKeyboardResizedContentPage)
            {
                Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (this is IKeyboardResizedContentPage)
            {
                Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
            }
        }

        #endregion

        #region Protected Methods

        protected void EntryView_Unfocused(object sender, FocusEventArgs e)
        {
            EntryUnfocused?.Invoke(sender, e); //for ios renderer
        }

        protected async void OnCollectionItemTapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter.ToString();
            await (ViewModel as ICollectionViewModel)?.OnItemSelectedAsync(id);
        }

        protected virtual void OnItemsCollectionScrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as Xamarin.Forms.ScrollView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace <= e.ScrollY && !_isLoadingMore)
            {
                Task.Run(async () =>
                {
                    _isLoadingMore = true;
                    await (ViewModel as ICollectionViewModel)?.LoadMoreAsync();
                    _isLoadingMore = false;
                });
            }
        }

        #endregion

        #region Private Methods

        private static void OnHasNativeNavBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(bindable, (bool)newValue);
        }

        private static void OnHasNativeBackButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Xamarin.Forms.NavigationPage.SetHasBackButton(_currentPage, (bool)newValue);
        }

        #endregion
    }
}
