using MvvmCross.Commands;
using MvvmCross.Forms.Presenters.Attributes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Enums;

namespace XamarinSocial.CustomElements.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomThreeTabsView : ContentView
    {
        public CustomThreeTabsView()
        {
            InitializeComponent();
            TabState = CustomTabState.FirstTabSelected;
        }

        #region Properties
        public CustomTabState TabState
        {
            get { return (CustomTabState)base.GetValue(CustomTabStateProperty); }
            set { base.SetValue(CustomTabStateProperty, value); }
        }
        public string FirstTabText
        {
            get { return base.GetValue(FirstTabProperty).ToString(); }
            set { base.SetValue(FirstTabProperty, value); }
        }

        public string SecondTabText
        {
            get { return base.GetValue(SecondTabProperty).ToString(); }
            set { base.SetValue(SecondTabProperty, value); }
        }

        public string ThirdTabText
        {
            get { return base.GetValue(ThirdTabProperty).ToString(); }
            set { base.SetValue(ThirdTabProperty, value); }
        }

        public bool IsBadgesVisible
        {
            get { return (bool)base.GetValue(IsBadgesVisibleProperty); }
            set { base.SetValue(IsBadgesVisibleProperty, value); }
        }

        public string FirstBadgeText
        {
            get { return base.GetValue(FirstBadgeTextProperty).ToString(); }
            set { base.SetValue(FirstBadgeTextProperty, value); }
        }

        public string SecondBadgeText
        {
            get { return base.GetValue(SecondBadgeTextProperty).ToString(); }
            set { base.SetValue(SecondBadgeTextProperty, value); }
        }

        public string ThirdBadgeText
        {
            get { return base.GetValue(ThirdBadgeTextProperty).ToString(); }
            set { base.SetValue(ThirdBadgeTextProperty, value); }
        }


        #endregion

        #region Bindable Properties
        public static readonly BindableProperty CustomTabStateProperty = BindableProperty.Create(
                                                                          propertyName: nameof(TabState),
                                                                          returnType: typeof(CustomTabState),
                                                                          declaringType: typeof(CustomThreeTabsView),
                                                                          defaultValue: CustomTabState.FirstTabSelected,
                                                                          defaultBindingMode: BindingMode.TwoWay,
                                                                          propertyChanged: CustomTabStateChanged);

        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
                                                                         propertyName: nameof(ClickTabCommand),
                                                                         returnType: typeof(IMvxCommand<CustomTabState>),
                                                                         declaringType: typeof(CustomThreeTabsView),
                                                                         defaultValue: null);

        public static readonly BindableProperty FirstTabProperty = BindableProperty.Create(
                                                                     propertyName: nameof(FirstTabText),
                                                                     returnType: typeof(string),
                                                                     declaringType: typeof(CustomThreeTabsView),
                                                                     defaultValue: default,
                                                                     defaultBindingMode: BindingMode.TwoWay,
                                                                     propertyChanged: FirstTabPropertyChanged);

        public static readonly BindableProperty SecondTabProperty = BindableProperty.Create(
                                                                     propertyName: nameof(SecondTabText),
                                                                     returnType: typeof(string),
                                                                     declaringType: typeof(CustomThreeTabsView),
                                                                     defaultValue: default,
                                                                     defaultBindingMode: BindingMode.TwoWay,
                                                                     propertyChanged: SecondTabPropertyChanged);

        public static readonly BindableProperty ThirdTabProperty = BindableProperty.Create(
                                                                     propertyName: nameof(ThirdTabText),
                                                                     returnType: typeof(string),
                                                                     declaringType: typeof(CustomThreeTabsView),
                                                                     defaultValue: default,
                                                                     defaultBindingMode: BindingMode.TwoWay,
                                                                     propertyChanged: ThirdTabPropertyChanged);

        public static readonly BindableProperty IsBadgesVisibleProperty = BindableProperty.Create(
                                                                          propertyName: nameof(IsBadgesVisible),
                                                                          returnType: typeof(bool),
                                                                          declaringType: typeof(CustomThreeTabsView),
                                                                          defaultValue: false,
                                                                          defaultBindingMode: BindingMode.TwoWay,
                                                                          propertyChanged: IsBadgesVisiblePropertyChanged);

        public static readonly BindableProperty FirstBadgeTextProperty = BindableProperty.Create(
                                                                     propertyName: nameof(FirstBadgeText),
                                                                     returnType: typeof(string),
                                                                     declaringType: typeof(CustomThreeTabsView),
                                                                     defaultValue: default,
                                                                     defaultBindingMode: BindingMode.TwoWay,
                                                                     propertyChanged: FirstBadgeTextPropertyChanged);

        public static readonly BindableProperty SecondBadgeTextProperty = BindableProperty.Create(
                                                                     propertyName: nameof(SecondBadgeText),
                                                                     returnType: typeof(string),
                                                                     declaringType: typeof(CustomThreeTabsView),
                                                                     defaultValue: default,
                                                                     defaultBindingMode: BindingMode.TwoWay,
                                                                     propertyChanged: SecondBadgeTextPropertyChanged);

        public static readonly BindableProperty ThirdBadgeTextProperty = BindableProperty.Create(
                                                                     propertyName: nameof(ThirdBadgeText),
                                                                     returnType: typeof(string),
                                                                     declaringType: typeof(CustomThreeTabsView),
                                                                     defaultValue: default,
                                                                     defaultBindingMode: BindingMode.TwoWay,
                                                                     propertyChanged: ThirdBadgeTextPropertyChanged);
        #endregion

        #region Commands
        public IMvxCommand<CustomTabState> ClickTabCommand
        {
            get { return (IMvxCommand<CustomTabState>)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }
        #endregion

        #region Property Handlers

        private static void FirstTabPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
            control.firstTab.Text = newValue.ToString();
        }

        private static void SecondTabPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
            control.secondTab.Text = newValue.ToString();
        }

        private static void ThirdTabPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
            control.thirdTab.Text = newValue.ToString();
        }

        private static void CustomTabStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
        }

        private static void IsBadgesVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
            control.firstBadge.IsVisible = (bool)newValue;
            control.secondBadge.IsVisible = (bool)newValue;
            control.thirdBadge.IsVisible = (bool)newValue;
        }

        private static void FirstBadgeTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
            control.firstBadge.Value = newValue.ToString();
        }

        private static void SecondBadgeTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
            control.secondBadge.Value = newValue.ToString();
        }

        private static void ThirdBadgeTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomThreeTabsView)bindable;
            control.thirdBadge.Value = newValue.ToString();
        }

        private void FirstTabClicked(object sender, EventArgs e)
        {
            TabState = CustomTabState.FirstTabSelected;
            ClickTabCommand?.Execute(CustomTabState.FirstTabSelected);
        }

        private void SecondTabClicked(object sender, EventArgs e)
        {
            TabState = CustomTabState.SecondTabSelected;
            ClickTabCommand?.Execute(CustomTabState.SecondTabSelected);
        }

        private void ThirdTabClicked(object sender, EventArgs e)
        {
            TabState = CustomTabState.ThirdTabSelected;
            ClickTabCommand?.Execute(CustomTabState.ThirdTabSelected);
        }
        #endregion
    }
}