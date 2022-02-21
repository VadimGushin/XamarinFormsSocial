using P42.Utils;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Enums;
using XamarinSocial.Models.Common;
using XamarinSocial.ViewModels.TabsView;

namespace XamarinSocial.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DropdownPopapView : PopupPage
    {
        #region Variables

        private SearchFilterViewModel _viewModel;
        private UserSearchDropdownTypes _filterType;
        private Action<string> _selectedItemAction;
        #endregion

        #region Constructors

        public DropdownPopapView()
        {
            InitializeComponent();
        }
        public DropdownPopapView(SearchFilterViewModel viewModel, UserSearchDropdownTypes filterType)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _filterType = filterType;
            BindingContext = _viewModel;
        }

        public DropdownPopapView(Action<string> responseAction = null, List<string> items = null)
        {
            InitializeComponent();
            if (responseAction != null)
            {
                _selectedItemAction = responseAction;
            }
            this.bottomButton.IsVisible = false;
            this.list.ItemsSource = items;
        }

        public DropdownPopapView(Action<string> responseAction = null, List<PopupMenuItemModel> items = null)
        {
            InitializeComponent();
            if (responseAction != null)
            {
                _selectedItemAction = responseAction;
            }
            this.mainContent.IsVisible = false;
            this.bottomButton.IsVisible = false;

            bindableLayoutContainer.IsVisible = true;
            BindableLayout.SetItemsSource(bindableLayout, items);
        }

        #endregion

        #region Private Methods

        private async void GradientButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            _selectedItemAction?.Invoke(e.SelectedItem.ToString());
            if (_viewModel != null)
            {
                var propertyInFilter = _viewModel.UserFilter.GetProperty(_filterType.ToString());
                propertyInFilter.SetValue(_viewModel.UserFilter, e.SelectedItem.ToString());
            }
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnFreeScapeClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        protected async void OnCollectionItemTapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter.ToString();
            _selectedItemAction?.Invoke(id);
            await PopupNavigation.Instance.PopAsync(true);
        }

        #endregion
    }
}