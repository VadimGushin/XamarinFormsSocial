using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinSocial.Models.Settings;
using XamarinSocial.ViewModelsResult;

namespace XamarinSocial.ViewModels.Settings
{
    public class AdditionalNotificationSettingsViewModel : BaseViewModel<AdditionalNotificationSettingsModel, DestructionResult<AdditionalNotificationSettingsModel>>
    {
        public AdditionalNotificationSettingsViewModel(IMvxNavigationService navigationService)
                                                       : base(navigationService)
        {
            Items = new MvxObservableCollection<AdditionalNotificationItemModel>();
        }

        #region Properties

        private AdditionalNotificationSettingsModel _dataModel;
        public AdditionalNotificationSettingsModel DataModel
        {
            get => _dataModel;
            set
            {
                SetProperty(ref _dataModel, value);
            }
        }

        private MvxObservableCollection<AdditionalNotificationItemModel> _items;
        public MvxObservableCollection<AdditionalNotificationItemModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
            }
        }

        #endregion

        #region Commands

        #endregion


        #region Overrides

        public override void Prepare(AdditionalNotificationSettingsModel parameter)
        {
            if (parameter != null)
            {
                DataModel = parameter;
                var items = new List<AdditionalNotificationItemModel>();
                foreach (var item in DataModel.Values)
                {
                    items.Add(new AdditionalNotificationItemModel()
                    {
                        Value = item.Value,
                        IsSelected = (item.Value.Equals(DataModel.SelectedValue) ? true : false)
                    });
                }
                Items.AddRange(items);               
            }

        }

        protected async override Task TryBackPage()
        {
            var selectedItemValue = Items.Where(item => item.IsSelected).FirstOrDefault();
            var selectedModel = new AdditionalNotificationSettingsModel(DataModel.Values, selectedItemValue.Value, DataModel.Title, DataModel.IsNotificationTurned);
            await NavigationService.Close(this, new DestructionResult<AdditionalNotificationSettingsModel>
            {
                Entity = selectedModel,
                Destroyed = true
            });
        }

        #endregion
        }
}
