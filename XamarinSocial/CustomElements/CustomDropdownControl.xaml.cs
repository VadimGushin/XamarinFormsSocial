using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Enums;
using XamarinSocial.Enums.PersonalDetails;
using XamarinSocial.Helpers;
using XamarinSocial.Models.Common;
using XamarinSocial.Views.Shared;

namespace XamarinSocial.CustomElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDropdownControl : ContentView
    {
        #region Variables

        private List<PopupMenuItemModel> ItemsSource;

        #endregion

        public CustomDropdownControl()
        {
            InitializeComponent();
            InitControls();
        }

        #region Properties

        /// <summary>
        /// Title text of the CustomDropdownControl
        /// </summary>
        public string Title
        {
            get { return base.GetValue(TitleProperty).ToString(); }
            set { base.SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Selected item text of the CustomDropdownControl
        /// </summary>
        public string SelectedItem
        {
            get { return base.GetValue(SelectedItemProperty).ToString(); }
            set { base.SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Type of items source for the CustomDropdownControl
        /// </summary>
        public UserSearchDropdownTypes ItemsSourceType
        {
            get { return (UserSearchDropdownTypes)base.GetValue(ItemsSourceTypeProperty); }
            set { base.SetValue(ItemsSourceTypeProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Item selected command of CustomDropdownControl
        /// </summary>
        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                propertyName: nameof(Title),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomDropdownControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
                                                propertyName: nameof(SelectedItem),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomDropdownControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: SelectedItemPropertyChanged);

        public static readonly BindableProperty SelectedCommandProperty =
                                                BindableProperty.Create(
                                                propertyName: nameof(SelectedCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(CustomDropdownControl),
                                                defaultValue: null);

        public static readonly BindableProperty ItemsSourceTypeProperty = BindableProperty.Create(
                                                propertyName: nameof(ItemsSourceType),
                                                returnType: typeof(UserSearchDropdownTypes),
                                                declaringType: typeof(CustomDropdownControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ItemsSourceTypePropertyChanged);

        #endregion

        #region Handlers

        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDropdownControl)bindable;
            control.titleText.Text = newValue?.ToString();
            control.mainText.Text = newValue?.ToString();
            control.mainText.TextColor = Color.Gray;
            control.titleText.IsVisible = false;
        }

        private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDropdownControl)bindable;
            var value = newValue?.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                control.mainText.Text = control.titleText.Text;
                control.mainText.TextColor = Color.Gray;
                control.titleText.IsVisible = false;
                return;
            }
            control.mainText.TextColor = Color.Black;
            control.mainText.Text = value;
            control.titleText.IsVisible = true;
        }

        private static void ItemsSourceTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDropdownControl)bindable;
            control.ItemsSource = GetItemsSourceBySourceType((UserSearchDropdownTypes)newValue);
        }

        #endregion

        #region private Methods

        private void InitControls() 
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                PopupNavigation.Instance.PushAsync(new DropdownPopapView(SetSeletdedItem, ItemsSource));
            };

            this.parentLayout.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private static List<PopupMenuItemModel> GetItemsSourceBySourceType(UserSearchDropdownTypes sourceType)
        {
            Type type = default;
            if (sourceType == UserSearchDropdownTypes.Ethnicity)
            {
                type = typeof(Ethnicity);
            }
            if (sourceType == UserSearchDropdownTypes.MinHeight || sourceType == UserSearchDropdownTypes.MaxHeight)
            {
                return UserHeightHelper.GetUserHeightSource().Select(message => new PopupMenuItemModel(message)).ToList();
            }
            if (sourceType == UserSearchDropdownTypes.BodyType)
            {
                type = typeof(BodyType);
            }
            if (sourceType == UserSearchDropdownTypes.EyesColor)
            {
                type = typeof(EyesColor);
            }
            if (sourceType == UserSearchDropdownTypes.HairColor)
            {
                type = typeof(HairColor);
            }
            if (sourceType == UserSearchDropdownTypes.SexualOrientation)
            {
                type = typeof(SexualOrientation);
            }
            if (sourceType == UserSearchDropdownTypes.Religion)
            {
                type = typeof(Religion);
            }
            if (sourceType == UserSearchDropdownTypes.Smocking)
            {
                type = typeof(SmockingType);
            }
            if (sourceType == UserSearchDropdownTypes.Drinking)
            {
                type = typeof(DrinkingType);
            }
            if (sourceType == UserSearchDropdownTypes.Kids)
            {
                type = typeof(KidsType);
            }
            var resoultSource = EnumHelper.GetCollectionByType(type);
            return resoultSource.Values.ToList().Select(message => new PopupMenuItemModel(message)).ToList();
        }

        private void SetSeletdedItem(string selectedItem)
        {
            var selectedItemModel = ItemsSource.Where(item => item.Id.Equals(selectedItem)).FirstOrDefault();
            var responseModel = new PopupResponseModel(ItemsSourceType, selectedItemModel.Text);
            SelectedCommand?.Execute(responseModel);

            this.mainText.TextColor = Color.Black;
            this.mainText.Text = selectedItemModel.Text;
            this.titleText.IsVisible = true;
        }

        #endregion
    }
}