using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSocial.CustomElements.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsButton : ContentView
    {
        public SettingsButton()
        {
            InitializeComponent();
            InitControls();
        }

        #region Properties

        /// <summary>
        /// Top text of the SettingsButton
        /// </summary>
        public string TopText
        {
            get { return base.GetValue(TopTextProperty).ToString(); }
            set { base.SetValue(TopTextProperty, value); }
        }

        /// <summary>
        /// Bottom text of the SettingsButton
        /// </summary>
        public string BottomText
        {
            get { return base.GetValue(BottomTextProperty).ToString(); }
            set { base.SetValue(BottomTextProperty, value); }
        }

        /// <summary>
        /// Image source of the SettingsButton
        /// </summary>
        public string ImageSource
        {
            get { return base.GetValue(ImageSourceProperty).ToString(); }
            set { base.SetValue(ImageSourceProperty, value); }
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

        public static readonly BindableProperty TopTextProperty = BindableProperty.Create(
                                                propertyName: nameof(TopText),
                                                returnType: typeof(string),
                                                declaringType: typeof(SettingsButton),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: TopTextPropertyChanged);

        public static readonly BindableProperty BottomTextProperty = BindableProperty.Create(
                                                propertyName: nameof(BottomText),
                                                returnType: typeof(string),
                                                declaringType: typeof(SettingsButton),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: BottomTextPropertyChanged);

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
                                                propertyName: nameof(ImageSource),
                                                returnType: typeof(string),
                                                declaringType: typeof(SettingsButton),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ImageSourcePropertyChanged);

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
                                                propertyName: nameof(TapCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(SettingsButton),
                                                defaultValue: null);

        #endregion

        #region Handlers

        private static void TopTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SettingsButton)bindable;
            control.topText.Text = newValue.ToString();
        }

        private static void BottomTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SettingsButton)bindable;
            control.bottomText.Text = newValue.ToString();
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SettingsButton)bindable;
            control.imageView.Source = newValue.ToString();
        }

        #endregion

        #region Private Methodds

        private void InitControls()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                this.TapCommand?.Execute(this.TopText);
            };
            this.mainLayout.GestureRecognizers.Add(tapGestureRecognizer);
        }

        #endregion
    }
}