using MvvmCross.Binding.Bindings.Source.Leaf;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XamarinSocial.Enums;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;
using XamarinSocial.ViewModels.Shared;
using XamarinSocial.ViewModelsResult;
using XamarinSocial.Views.Shared;
using static XamarinSocial.Constants.Constant;

namespace XamarinSocial.ViewModels.TabsView
{
    public class SearchFilterViewModel : BaseViewModel<SearchUserFilter, DestructionResult<SearchUserFilter>>
    {
        private List<string> _filterHairColors;
        private List<string> _filterEyeColors;
        private List<string> _filterEthnicity;
        private List<string> _filterMinHeight;
        private List<string> _filterMaxHeight;
        private List<string> _filterSexualOrientation;

        public SearchFilterViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            UserFilter = new SearchUserFilter();
            SetupUI();
            GetDataForFilters();
        }

        public override void Prepare(SearchUserFilter parameter)
        {
        }

        #region Properties

        private SearchUserFilter _userFilter;
        public SearchUserFilter UserFilter
        {
            get => _userFilter;
            set => SetProperty(ref _userFilter, value);
        }

        private List<string> _currentDropDownList;
        public List<string> CurrentDropDownList
        {
            get => _currentDropDownList;
            set => SetProperty(ref _currentDropDownList, value);
        }

        private int _minLookingAge;
        public int MinLookingAge
        {
            get => _minLookingAge;
            set => SetProperty(ref _minLookingAge, value);
        }

        private int _maxLookingAge;
        public int MaxLookingAge
        {
            get => _maxLookingAge;
            set => SetProperty(ref _maxLookingAge, value);
        }

        private int _minDistance;
        public int MinDistance
        {
            get => _minDistance;
            set => SetProperty(ref _minDistance, value);
        }

        private int _maxDistance;
        public int MaxDistance
        {
            get => _maxDistance;
            set => SetProperty(ref _maxDistance, value);
        }

        private bool _isPopular;
        public bool IsPopular
        {
            get => _isPopular;
            set => SetProperty(ref _isPopular, value);
        }


        private bool _isActiveUsers;
        public bool IsActiveUsers
        {
            get => _isActiveUsers;
            set => SetProperty(ref _isActiveUsers, value);
        }

        private double _searchFemaleOpacity;
        public double SearchFemaleOpacity
        {
            get => _searchFemaleOpacity;
            set => SetProperty(ref _searchFemaleOpacity, value);
        }

        private double _searchMaleOpacity;
        public double SearchMaleOpacity
        {
            get => _searchMaleOpacity;
            set => SetProperty(ref _searchMaleOpacity, value);
        }

        private double _searchOtherOpacity;
        public double SearchOtherOpacity
        {
            get => _searchOtherOpacity;
            set => SetProperty(ref _searchOtherOpacity, value);
        }

        private double _searchAllOpacity;
        public double SearchAllOpacity
        {
            get => _searchAllOpacity;
            set => SetProperty(ref _searchAllOpacity, value);
        }

        private GooglePlaceAutoCompletePrediction _currentSearchLocation;
        public GooglePlaceAutoCompletePrediction CurrentSearchLocation
        {
            get => _currentSearchLocation;
            set
            {
                SetProperty(ref _currentSearchLocation, value);
                UserFilter.Location = CurrentSearchLocation.StructuredFormatting.MainText;
                UserFilter.GoogleLocationId = CurrentSearchLocation.PlaceId;
                UserFilter.LocationOtherStrings = CurrentSearchLocation.StructuredFormatting.SecondaryText;
                SearchLocation = CurrentSearchLocation.StructuredFormatting.MainText; 
            }
        }

        private string _searchLocation;
        public string SearchLocation
        {
            get => _searchLocation;
            set => SetProperty(ref _searchLocation, value);
        }

        #endregion

        #region Commands

        public IMvxAsyncCommand NavigateToSearchLocationViewCommandAsync => new MvxAsyncCommand(NavigateToSearchLocationViewAsync);
        public IMvxCommand SearchFemaleCommand => new MvxCommand(SetFemaleGender);
        public IMvxCommand SearchMaleCommand => new MvxCommand(SetMaleGender);
        public IMvxCommand SearchOtherCommand => new MvxCommand(SetOtherGender);
        public IMvxCommand SearchAllCommand => new MvxCommand(SetAllGender);
        public IMvxCommand SetPopularUserCommand => new MvxCommand(SetPopularUser);
        public IMvxCommand SetActiveUsersCommand => new MvxCommand(SetActiveUser);
        public IMvxAsyncCommand OpenMinHeightDropDownCommandAsync => new MvxAsyncCommand(OpenMinHeightDropDownAsync);
        public IMvxAsyncCommand OpenMaxHeightDropDownCommandAsync => new MvxAsyncCommand(OpenMaxHeightDropDownAsync);
        public IMvxAsyncCommand OpenEthnicityDropDownCommandAsync => new MvxAsyncCommand(OpenEthnicityDropDownAsync);
        public IMvxAsyncCommand OpenSexualOrientationDropDownCommandAsync => new MvxAsyncCommand(OpenSexualOrientationDropDownAsync);
        public IMvxAsyncCommand OpenEyesDropDownCommandAsync => new MvxAsyncCommand(OpenEyesDropDownAsync);
        public IMvxAsyncCommand OpenHairDropDownCommandAsync => new MvxAsyncCommand(OpenHairDropDownAsync);
        public IMvxAsyncCommand ApplyFilterCommandAsync => new MvxAsyncCommand(ApplyFilterAsync);

        #endregion

        #region Functions

        private async Task NavigateToSearchLocationViewAsync()
        {
            var result = await NavigationService.Navigate<SearchLocationViewModel, GooglePlaceAutoCompletePrediction, DestructionResult<GooglePlaceAutoCompletePrediction>>(new GooglePlaceAutoCompletePrediction());
            if (result != null && result.Destroyed)
            {
                CurrentSearchLocation = result.Entity;
            }
        }

        private void GetDataForFilters()
        {
            _filterHairColors = new List<string> { "LightBrown", "Auburn/Red", "Blonde", "White", "Dark Blonde", "Dark Brown", "Black", "Grey" };
            _filterEyeColors = new List<string> { "Grey", "Hazel", "Blue", "Brown", "Green", "Black" };
            _filterEthnicity = new List<string> { "Pacific Islander", "Latino/Hispanic", "East Indian", "White/Caucasian", "Middle Eastern", "Black/African Decent", "Asian", "Native American", "Other" };
            _filterMinHeight = new List<string> { "3'(91cm)", "3'1\"(94cm)", "3'2\"(97cm)", "3'3\"(99cm)", "3'4\"(102cm)", "3'5\"(105cm)",
             "3'6\"(107cm)", "3'7\"(109cm)", "3'8\"(112cm)", "3'9\"(114cm)", "3'10\"(117cm)", "3'11\"(119cm)", "4'(121cm)", "4'1\"(125cm)",
             "4'2\"(127cm)", "4'3\"(130cm)", "4'4\"(132cm)", "4'5\"(135cm)", "4'6\"(137cm)", "4'7\"(140cm)", "4'8\"(142cm)", "4'9\"(145cm)",
             "4'10\"(147cm)", "4'11\"(150cm)", "5'(152cm)", "5'1\"(155cm)", "5'2\"(157cm)", "5'3\"(160cm)", "5'4\"(163cm)", "5'5\"(165cm)",
             "5'6\"(168cm)", "5'7\"(170cm)", "5'8\"(173cm)", "5'9\"(175cm)", "5'10\"(178cm)", "5'11\"(180cm)", "6'(183cm)", "6'1\"(185cm)",
            "6'2\"(188cm)", "6'3\"(190cm)", "6'4\"(193cm)", "6'5\"(196cm)", "6'6\"(198cm)", "6'7\"(201cm)", "6'8\"(203cm)", "6'9\"(206cm)",
            "6'10\"(209cm)", "6'11\"(211cm)", "7'(213cm)", "7'1\"(216cm)", "7'2\"(218cm)", "7'3\"(221cm)", "7'4\"(224cm)", "7'5\"(226cm)",
            "7'6\"(229cm)", "7'7\"(231cm)", "7'8\"(234cm)", "7'9\"(236cm)","7'10\"(239cm)", "7'11\"(241cm)", "8'(244cm)"};
            _filterMaxHeight = new List<string> { "3'(91cm)", "3'1\"(94cm)", "3'2\"(97cm)", "3'3\"(99cm)", "3'4\"(102cm)", "3'5\"(105cm)",
             "3'6\"(107cm)", "3'7\"(109cm)", "3'8\"(112cm)", "3'9\"(114cm)", "3'10\"(117cm)", "3'11\"(119cm)", "4'(121cm)", "4'1\"(125cm)",
             "4'2\"(127cm)", "4'3\"(130cm)", "4'4\"(132cm)", "4'5\"(135cm)", "4'6\"(137cm)", "4'7\"(140cm)", "4'8\"(142cm)", "4'9\"(145cm)",
             "4'10\"(147cm)", "4'11\"(150cm)", "5'(152cm)", "5'1\"(155cm)", "5'2\"(157cm)", "5'3\"(160cm)", "5'4\"(163cm)", "5'5\"(165cm)",
             "5'6\"(168cm)", "5'7\"(170cm)", "5'8\"(173cm)", "5'9\"(175cm)", "5'10\"(178cm)", "5'11\"(180cm)", "6'(183cm)", "6'1\"(185cm)",
            "6'2\"(188cm)", "6'3\"(190cm)", "6'4\"(193cm)", "6'5\"(196cm)", "6'6\"(198cm)", "6'7\"(201cm)", "6'8\"(203cm)", "6'9\"(206cm)",
            "6'10\"(209cm)", "6'11\"(211cm)", "7'(213cm)", "7'1\"(216cm)", "7'2\"(218cm)", "7'3\"(221cm)", "7'4\"(224cm)", "7'5\"(226cm)",
            "7'6\"(229cm)", "7'7\"(231cm)", "7'8\"(234cm)", "7'9\"(236cm)","7'10\"(239cm)", "7'11\"(241cm)", "8'(244cm)"};
            _filterSexualOrientation = new List<string> { "Female", "Male", "Other", "All" };
        }
        private async Task ApplyFilterAsync()
        {
            SetAllFilterFields();
            await NavigationService.Close(this, new DestructionResult<SearchUserFilter>
            {
                Entity = UserFilter,
                Destroyed = true
            });
        }
        private async Task OpenHairDropDownAsync()
        {
            CurrentDropDownList = _filterHairColors;
            await PopupNavigation.Instance.PushAsync(new DropdownPopapView(this, UserSearchDropdownTypes.HairColor));
        }
        private async Task OpenEyesDropDownAsync()
        {
            CurrentDropDownList = _filterEyeColors;
            await PopupNavigation.Instance.PushAsync(new DropdownPopapView(this, UserSearchDropdownTypes.EyesColor));
        }
        private async Task OpenSexualOrientationDropDownAsync()
        {
            CurrentDropDownList = _filterSexualOrientation;
            await PopupNavigation.Instance.PushAsync(new DropdownPopapView(this, UserSearchDropdownTypes.SexualOrientation));
        }
        private async Task OpenMinHeightDropDownAsync()
        {
            CurrentDropDownList = _filterMinHeight;
            await PopupNavigation.Instance.PushAsync(new DropdownPopapView(this, UserSearchDropdownTypes.MinHeight));
        }
        private async Task OpenMaxHeightDropDownAsync()
        {
            CurrentDropDownList = _filterMaxHeight;
            await PopupNavigation.Instance.PushAsync(new DropdownPopapView(this, UserSearchDropdownTypes.MaxHeight));
        }
        private async Task OpenEthnicityDropDownAsync()
        {
            CurrentDropDownList = _filterEthnicity;
            await PopupNavigation.Instance.PushAsync(new DropdownPopapView(this, UserSearchDropdownTypes.Ethnicity));
        }

        private void SetPopularUser()
        {
            IsPopular = !IsPopular;
            UserFilter.IsPopular = IsPopular;
        }

        private void SetActiveUser()
        {
            IsActiveUsers = !IsActiveUsers;
            UserFilter.IsActiveUsers = IsActiveUsers;
        }
        private void SetupUI()
        {
            SetFemaleGender();

            MinLookingAge = Numeric.LookingForLowAge;
            MaxLookingAge = Numeric.LookingForHighAge;

            MinDistance = Numeric.MinDistance;
            MaxDistance = Numeric.MaxDistnce;

            IsActiveUsers = false;
            IsPopular = false;
        }
        private void SetFemaleGender()
        {
            SetAllIAmGenderHalfOpacity();
            SearchFemaleOpacity = Numeric.GenderButtonFullOpacity;
            UserFilter.PreferencesGender = Gender.Female;
        }
        private void SetAllGender()
        {
            SetAllIAmGenderHalfOpacity();
            SearchAllOpacity = Numeric.GenderButtonFullOpacity;
            UserFilter.PreferencesGender = Gender.All;
        }

        private void SetOtherGender()
        {
            SetAllIAmGenderHalfOpacity();
            SearchOtherOpacity = Numeric.GenderButtonFullOpacity;
            UserFilter.PreferencesGender = Gender.Other;
        }

        private void SetMaleGender()
        {
            SetAllIAmGenderHalfOpacity();
            SearchMaleOpacity = Numeric.GenderButtonFullOpacity;
            UserFilter.PreferencesGender = Gender.Male;
        }

        private void SetAllIAmGenderHalfOpacity()
        {
            SearchFemaleOpacity = Numeric.GenderButtonHalfOpacity;
            SearchMaleOpacity = Numeric.GenderButtonHalfOpacity;
            SearchOtherOpacity = Numeric.GenderButtonHalfOpacity;
            SearchAllOpacity = Numeric.GenderButtonHalfOpacity;
        }

        private void SetAllFilterFields()
        {
            UserFilter.MinAge = MinLookingAge;
            UserFilter.MaxAge = MaxLookingAge;
            UserFilter.MinDistance = MinDistance;
            UserFilter.MaxDistance = MaxDistance;
        }

        #endregion
    }
}
