using MvvmCross.Forms.Presenters.Attributes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.TabsView;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.TabsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class ProfileView : BaseContentPage<ProfileViewModel>
    {
        public ProfileView()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(() =>
            {
                GridItemLayoutSecondTab.Span = 3;
                SecondTabGridButton.Source = "profile_photos_grid_active.png";
            });

        }
        protected override void OnAppearing()
        {
            FirsTabBoxView.HeightRequest = ViewHeader.Height;
            SecondTabBoxView.HeightRequest = ViewHeader.Height;
            ThirdTabBoxView.HeightRequest = ViewHeader.Height;
            base.OnAppearing();
        }

        private void ScrollViewFirstTab_Scrolled(object sender, ScrolledEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (e.ScrollY > 60)
                {
                    ViewModel.IsShowTabsHeader = false;
                }
                if (e.ScrollY <= 60)
                {
                    ViewModel.IsShowTabsHeader = true;
                }
            });
        }

        private void CollectionViewSecondTab_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (e.VerticalDelta > 0 && e.VerticalOffset > 90)
                {
                    //scrolled down
                    ViewModel.IsShowTabsHeader = false;
                }
                if (e.VerticalDelta < -0)
                {
                    //scrolled up
                    ViewModel.IsShowTabsHeader = true;
                }
            });
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GridItemLayoutSecondTab.Span = 1;
                SecondTabGridButton.Source = "profile_photos_grid.png";
                SecondTabListButton.Source = "profile_photos_list_active.png";
            });
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GridItemLayoutSecondTab.Span = 3;
                SecondTabListButton.Source = "profile_photos_list.png";
                SecondTabGridButton.Source = "profile_photos_grid_active.png";
            });
        }

        private void CollectionViewThirdTab_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (e.VerticalDelta > 0 && e.VerticalOffset > 90)
                {
                    //scrolled down
                    ViewModel.IsShowTabsHeader = false;
                }
                if (e.VerticalDelta < -0)
                {
                    //scrolled up
                    ViewModel.IsShowTabsHeader = true;
                }
            });
        }
    }
}