using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSocial.CustomElements.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadgeControl : ContentView
    {
        #region

        private static readonly Color _roundedColor = (Color)(Application.Current.Resources["badge_color_i2"]);

        #endregion
        public BadgeControl()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        /// Value string of the BadgeControl
        /// </summary>
        public string Value
        {
            get { return base.GetValue(ValueProperty).ToString(); }
            set { base.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Indicates whether the frame corners is cyrcled for BadgeControl
        /// </summary>
        public bool IsRounded
        {
            get { return (bool)base.GetValue(IsRoundedProperty); }
            set { base.SetValue(IsRoundedProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
                                                propertyName: nameof(Value),
                                                returnType: typeof(string),
                                                declaringType: typeof(BadgeControl),
                                                defaultValue: "0",
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ValuePropertyChanged);

        public static readonly BindableProperty IsRoundedProperty = BindableProperty.Create(
                                                propertyName: nameof(IsRounded),
                                                returnType: typeof(bool),
                                                declaringType: typeof(BadgeControl),
                                                defaultValue: false,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: IsRoundedPropertyChanged);

        #endregion

        #region Handlers

        private static void ValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (BadgeControl)bindable;
            control.valueView.Text = newValue.ToString();
        }

        private static void IsRoundedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (BadgeControl)bindable;
            if ((bool)newValue)
            {
                control.frameView.CornerRadius = 8;
                control.frameView.BackgroundColor = _roundedColor;
            }
        }

        #endregion
    }
}