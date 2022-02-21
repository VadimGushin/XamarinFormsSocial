using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinSocial.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using XamarinSocial.Helpers;
using System;
using Xamarin.Forms;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.SignUp
{
    public class CountryCodeViewModel : BaseViewModel<string, DestructionResult<string>>
    {
        public CountryCodeViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            CountryCodes = new ObservableCollection<Grouping<string, CountryCode>>();
            GetCountryCodes();
        }

        public ObservableCollection<Grouping<string, CountryCode>> CountryCodes { get; set; }

        private List<CountryCode> _countryCodes;

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                if (SetProperty(ref _searchString, value))
                {
                    GetFilteredCodes(_searchString);
                }
            }
        }

        private CountryCode _selectedItem;
        public CountryCode SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                if (_selectedItem == null)
                {
                    return;
                }
                NavigateToSignInViewWithParamsCommandAsync?.Execute(_selectedItem.Code);

                SelectedItem = null;
            }
        }

        public void GetFilteredCodes(string searchString)
        {
            var filteredCodes = _countryCodes;
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                filteredCodes = _countryCodes.Where(code => code.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            var sorted = from code in filteredCodes
                         orderby code.Name
                         group code by code.NameSort into codeGroup
                         select new Grouping<string, CountryCode>(codeGroup.Key, codeGroup);

            //create a new collection of groups
            CountryCodes = new ObservableCollection<Grouping<string, CountryCode>>(sorted);
            RaisePropertyChanged(nameof(CountryCodes));
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
        }

        #region Commands
        public IMvxAsyncCommand<string> NavigateToSignInViewWithParamsCommandAsync => new MvxAsyncCommand<string>(NavigateWithParametrToSignUpViewAsync);
        #endregion

        #region Functions


        private async Task NavigateWithParametrToSignUpViewAsync(string countryCode)
        {
            await NavigationService.Close(this, new DestructionResult<string>
            {
                Entity = countryCode,
                Destroyed = true
            });
        }

        private void GetCountryCodes()
        {
            string jsonFileName = "PhoneCodes.json";

            var assembly = typeof(CountryCodeViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.PhoneCodes.{jsonFileName}");
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

                //Converting JSON Array Objects into generic list    
                _countryCodes = JsonConvert.DeserializeObject<List<CountryCode>>(jsonString);

                var sorted = from code in _countryCodes
                             orderby code.Name
                             group code by code.NameSort into codeGroup
                             select new Grouping<string, CountryCode>(codeGroup.Key, codeGroup);

                //create a new collection of groups
                CountryCodes = new ObservableCollection<Grouping<string, CountryCode>>(sorted);
            }
        }

        public override void Prepare(string parameter)
        {
           
        }
        #endregion Functions
    }
}
