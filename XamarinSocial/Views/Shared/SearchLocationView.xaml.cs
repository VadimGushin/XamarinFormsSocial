using System;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XamarinSocial.Models;
using XamarinSocial.ViewModels.Shared;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchLocationView : BaseContentPage<SearchLocationViewModel>
    {
        public SearchLocationView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as GooglePlaceAutoCompletePrediction;
            ViewModel.CurrentSearchLocation = ViewModel.Places.FirstOrDefault(place => place.StructuredFormatting.MainText.Equals(item.StructuredFormatting.MainText));
            ViewModel.SearchLocation = $"{ViewModel.CurrentSearchLocation.StructuredFormatting.MainText}, {ViewModel.CurrentSearchLocation.StructuredFormatting.SecondaryText}";
            ViewModel.IsAutoCompleteVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            ViewModel.BackCommand.Execute();
            return true;
        }
    }
}