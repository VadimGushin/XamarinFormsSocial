using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSocial.CustomElements.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialNetworkControl : ContentView
    {
        public SocialNetworkControl()
        {
            InitializeComponent();
            InitControls();
        }

        #region Properties

        /// <summary>
        ///Main text of the SocialNetworkControl
        /// </summary>
        public string Text
        {
            get { return base.GetValue(TextProperty).ToString(); }
            set { base.SetValue(TextProperty, value); }
        }

        /// <summary>
        ///Left image source of the SocialNetworkControl
        /// </summary>
        public string LeftImageSource
        {
            get { return base.GetValue(LeftImageSourceProperty).ToString(); }
            set { base.SetValue(LeftImageSourceProperty, value); }
        }

        /// <summary>
        ///Indicates whether the right image of the SocialNetworkControl is visible
        /// </summary>
        public bool IsRightImageVisible
        {
            get { return (bool)base.GetValue(IsRightImageVisibleProperty); }
            set { base.SetValue(IsRightImageVisibleProperty, value); }
        }

        /// <summary>
        ///Badge text of the SocialNetworkControl
        /// </summary>
        public string BadgeText
        {
            get { return base.GetValue(BadgeTextProperty).ToString(); }
            set { base.SetValue(BadgeTextProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Click command of SettingsButton
        /// </summary>
        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                                propertyName: nameof(Text),
                                                returnType: typeof(string),
                                                declaringType: typeof(SocialNetworkControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: TextPropertyChanged);

        public static readonly BindableProperty LeftImageSourceProperty = BindableProperty.Create(
                                                propertyName: nameof(LeftImageSource),
                                                returnType: typeof(string),
                                                declaringType: typeof(SocialNetworkControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: LeftImageSourcePropertyChanged);

        public static readonly BindableProperty IsRightImageVisibleProperty = BindableProperty.Create(
                                                propertyName: nameof(IsRightImageVisible),
                                                returnType: typeof(bool),
                                                declaringType: typeof(SocialNetworkControl),
                                                defaultValue: false,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: IsRightImageVisiblePropertyChanged);

        public static readonly BindableProperty BadgeTextProperty = BindableProperty.Create(
                                                propertyName: nameof(BadgeText),
                                                returnType: typeof(string),
                                                declaringType: typeof(SocialNetworkControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: BadgeTextPropertyChanged);

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
                                                propertyName: nameof(TapCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(SocialNetworkControl),
                                                defaultValue: null);

        #endregion

        #region Handlers

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SocialNetworkControl)bindable;
            control.mainText.Text = newValue.ToString();
        }

        private static void LeftImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SocialNetworkControl)bindable;
            var source = newValue.ToString();
            control.leftImageView.Source = source;
            control.leftImageView.IsVisible = string.IsNullOrWhiteSpace(source) ? false : true;
        }

        private static void IsRightImageVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SocialNetworkControl)bindable;
            control.rightImage.IsVisible = (bool)newValue;
        }

        private static void BadgeTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SocialNetworkControl)bindable;
            var text = newValue.ToString();
            control.badgeView.Value = text;
            control.badgeView.IsVisible = string.IsNullOrWhiteSpace(text) ? false : true;
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                this.TapCommand?.Execute(null);
            };
            this.mainFrameView.GestureRecognizers.Add(tapGestureRecognizer);
        }

        #endregion

    }
}