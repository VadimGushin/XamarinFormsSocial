using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.Shared
{
    public class SearchLocationViewModel : BaseViewModel<GooglePlaceAutoCompletePrediction, DestructionResult<GooglePlaceAutoCompletePrediction>>
    {
        private readonly IGoogleMapsApiService _googleMapsApiService;
        public SearchLocationViewModel(IMvxNavigationService navigationService, IGoogleMapsApiService googleMapsApiService) : base(navigationService)
        {
            _googleMapsApiService = googleMapsApiService;
        }

        #region Properties
        private string _searchLocation;
        public string SearchLocation
        {
            get => _searchLocation;
            set
            {
                if (SetProperty(ref _searchLocation, value))
                {
                    GetPlacesCommandAsync.Execute(_searchLocation);
                }
            }
        }

        public IList<GooglePlaceAutoCompletePrediction> Places { get; set; }

        private GooglePlaceAutoCompletePrediction _currentSearchLocation;
        public GooglePlaceAutoCompletePrediction CurrentSearchLocation
        {
            get => _currentSearchLocation;
            set
            {
                SetProperty(ref _currentSearchLocation, value);
            }
        }

        private bool _isAutoCompleteVisible;
        public bool IsAutoCompleteVisible
        {
            get => _isAutoCompleteVisible;
            set => SetProperty(ref _isAutoCompleteVisible, value);
        }
        #endregion

        #region Commands
        public IMvxAsyncCommand<GooglePlaceAutoCompletePrediction> NavigateBackWithParametrsCommandAsync => new MvxAsyncCommand<GooglePlaceAutoCompletePrediction>(NavigateBackWithParametrAsync);
        public IMvxCommand AcceptClickedCommand => new MvxCommand(AcceptClicked);
        public IMvxCommand GetPlacesCommandAsync => new MvxAsyncCommand<string>(GetPlacesAsync);
        #endregion


        #region Functions

        private void AcceptClicked()
        {
            if (CurrentSearchLocation == null)
            {
                CurrentSearchLocation = new GooglePlaceAutoCompletePrediction();
            }
            NavigateBackWithParametrsCommandAsync?.Execute(CurrentSearchLocation);
        }

        private async Task NavigateBackWithParametrAsync(GooglePlaceAutoCompletePrediction location)
        {
            await NavigationService.Close(this, new DestructionResult<GooglePlaceAutoCompletePrediction>
            {
                Entity = location,
                Destroyed = true
            });
        }

        public override void Prepare(GooglePlaceAutoCompletePrediction parameter)
        {
        }

        private async Task GetPlacesAsync(string place)
        {
            var places = await _googleMapsApiService.GetPlaces(place);
            var placesResult = places.AutoCompletePlaces;
            if (!(placesResult is null) && placesResult.Count > default(int))
            {
                Places = new List<GooglePlaceAutoCompletePrediction>(placesResult);
                await RaisePropertyChanged(nameof(Places));
                IsAutoCompleteVisible = true;
            }
        }
        #endregion
    }
}
