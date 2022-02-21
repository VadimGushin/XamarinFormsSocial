using MvvmCross.Commands;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Enums;

namespace XamarinSocial.CustomElements.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTwoTabsView : ContentView
    {
        public CustomTwoTabsView()
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

        #endregion

        #region Bindable Properties
        public static readonly BindableProperty CustomTabStateProperty = BindableProperty.Create(
                                              propertyName: nameof(TabState),
                                              returnType: typeof(CustomTabState),
                                              declaringType: typeof(CustomTwoTabsView),
                                              defaultValue: CustomTabState.FirstTabSelected,
                                              defaultBindingMode: BindingMode.TwoWay,
                                              propertyChanged: CustomTabStateChanged);

        public static readonly BindableProperty ClickCommandProperty =
                                         BindableProperty.Create(
                                         propertyName: nameof(ClickTabCommand),
                                         returnType: typeof(IMvxCommand<CustomTabState>),
                                         declaringType: typeof(CustomTwoTabsView),
                                         defaultValue: null);

        public static readonly BindableProperty FirstTabProperty = BindableProperty.Create(
                                         propertyName: nameof(FirstTabText),
                                         returnType: typeof(string),
                                         declaringType: typeof(CustomTwoTabsView),
                                         defaultValue: default,
                                         defaultBindingMode: BindingMode.TwoWay,
                                         propertyChanged: FirstTabPropertyChanged);

        public static readonly BindableProperty SecondTabProperty = BindableProperty.Create(
                                 propertyName: nameof(SecondTabText),
                                 returnType: typeof(string),
                                 declaringType: typeof(CustomTwoTabsView),
                                 defaultValue: default,
                                 defaultBindingMode: BindingMode.TwoWay,
                                 propertyChanged: SecondTabPropertyChanged);

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
            var control = (CustomTwoTabsView)bindable;
            control.firstTab.Text = newValue.ToString();
        }

        private static void SecondTabPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomTwoTabsView)bindable;
            control.secondTab.Text = newValue.ToString();
        }

        private static void CustomTabStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomTwoTabsView)bindable;
        }

        private void FirstTabClicked(object sender, EventArgs e)
        {
            TabState = CustomTabState.FirstTabSelected;
            ClickTabCommand.Execute(CustomTabState.FirstTabSelected);
        }

        private void SecondTabClicked(object sender, EventArgs e)
        {
            TabState = CustomTabState.SecondTabSelected;
            ClickTabCommand.Execute(CustomTabState.SecondTabSelected);
        }
        #endregion
    }
}