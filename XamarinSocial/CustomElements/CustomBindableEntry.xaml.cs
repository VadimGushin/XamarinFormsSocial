using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LocalStrings = XamarinSocial.Resources.Strings.Strings;

namespace XamarinSocial.CustomElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBindableEntry : ContentView
    {
        #region

        private const int _maxTextLenght = 200;
        private readonly Color _errorBorderColor = (Color)(Application.Current.Resources["tcolor_b6"]); 
        private readonly Color _normalBorderColor = (Color)(Application.Current.Resources["tcolor_b5"]); 
        private readonly Color _errorTextColor = (Color)(Application.Current.Resources["fcolor_b4"]);
        private readonly Color _normalTextColor = (Color)(Application.Current.Resources["fcolor_b2"]);

        #endregion

        public CustomBindableEntry()
        {
            InitializeComponent();
            InitControls();
        }

        #region Properties

        /// <summary>
        /// Title text of the CustomBindableEntry
        /// </summary>
        public string Title
        {
            get { return base.GetValue(TitleProperty).ToString(); }
            set { base.SetValue(TitleProperty, value); }
        }


        /// <summary>
        /// Main bindable value of the CustomBindableEntry
        /// </summary>
        public string MainValue
        {
            get { return base.GetValue(MainValueProperty).ToString(); }
            set { base.SetValue(MainValueProperty, value); }
        }

        /// <summary>
        /// Right image source of the CustomBindableEntry
        /// </summary>
        public string ImageSource
        {
            get { return base.GetValue(ImageSourceProperty).ToString(); }
            set { base.SetValue(ImageSourceProperty, value); }
        }

        /// <summary>
        /// Text for error control of the CustomBindableEntry
        /// </summary>
        public string ErrorText
        {
            get { return base.GetValue(ErrorTextProperty).ToString(); }
            set { base.SetValue(ErrorTextProperty, value); }
        }

        /// <summary>
        /// Validation rules for the CustomBindableEntry
        /// </summary>
        public string ValidationPattern
        {
            get { return base.GetValue(ValidationPatternProperty).ToString(); }
            set { base.SetValue(ValidationPatternProperty, value); }
        }

        /// <summary>
        /// Background color of the CustomBindableEntry
        /// </summary>
        public Color BackgroundViewColor
        {
            get { return (Color)base.GetValue(BackgroundViewColorProperty); }
            set { base.SetValue(BackgroundViewColorProperty, value); }
        }

        /// <summary>
        /// Border color of the CustomBindableEntry
        /// </summary>
        public Color BorderViewColor
        {
            get { return (Color)base.GetValue(BorderViewColorProperty); }
            set { base.SetValue(BorderViewColorProperty, value); }
        }


        /// <summary>
        /// Indicates whether the value of the CustomBindableEntry is readonly
        /// </summary>
        public bool IsValueReadOnly
        {
            get { return (bool)base.GetValue(IsValueReadOnlyProperty); }
            set { base.SetValue(IsValueReadOnlyProperty, value); }
        }

        /// <summary>
        /// Indicates whether the content of the CustomBindableEntry consist only text
        /// </summary>
        public bool IsTextOnly
        {
            get { return (bool)base.GetValue(IsTextOnlyProperty); }
            set { base.SetValue(IsTextOnlyProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Click command of CustomBindableEntry
        /// </summary>
        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                propertyName: nameof(Title),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty MainValueProperty = BindableProperty.Create(
                                                propertyName: nameof(MainValue),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: string.Empty,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: MainValuePropertyChanged);

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
                                                propertyName: nameof(ImageSource),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: string.Empty,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ImageSourcePropertyChanged);

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
                                                propertyName: nameof(ErrorText),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: string.Empty,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ErrorTextPropertyChanged);

        public static readonly BindableProperty ValidationPatternProperty = BindableProperty.Create(
                                                propertyName: nameof(ValidationPattern),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: string.Empty);

        public static readonly BindableProperty BackgroundViewColorProperty = BindableProperty.Create(
                                                propertyName: nameof(BackgroundViewColor),
                                                returnType: typeof(Color),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: Color.Default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: BackgroundViewColorPropertyChanged);

        public static readonly BindableProperty BorderViewColorProperty = BindableProperty.Create(
                                                propertyName: nameof(BorderViewColor),
                                                returnType: typeof(Color),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: Color.Default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: BorderViewColorPropertyChanged);

        public static readonly BindableProperty IsValueReadOnlyProperty = BindableProperty.Create(
                                                propertyName: nameof(IsValueReadOnly),
                                                returnType: typeof(bool),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: false,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: IsValueReadOnlyPropertyChanged);

        public static readonly BindableProperty IsTextOnlyProperty = BindableProperty.Create(
                                                propertyName: nameof(IsTextOnly),
                                                returnType: typeof(bool),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: false,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: IsTextOnlyPropertyChanged);

        public static readonly BindableProperty ClickCommandProperty =
                                                BindableProperty.Create(
                                                propertyName: nameof(ClickCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(CustomBindableEntry),
                                                defaultValue: null);

        #endregion

        #region Handlers

        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.lblTitle.Text = newValue?.ToString();
        }

        private static void MainValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.entryValue.Text = newValue?.ToString();
            control.entryTextValue.Text = newValue?.ToString();
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.RightImage.Source = (string)newValue;
        }

        private static void ErrorTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.lblError.Text = newValue?.ToString();
        }

        private static void BackgroundViewColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.frameView.BackgroundColor = (Color)newValue;
            control.frameTextView.BackgroundColor = (Color)newValue;
        }

        private static void BorderViewColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.frameView.BorderColor = (Color)newValue;
            control.frameTextView.BorderColor = (Color)newValue;
        }

        private static void IsValueReadOnlyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.entryValue.IsReadOnly = (bool)newValue;
            control.tappedBoxView.IsVisible = (bool)newValue;

            if ((bool)newValue)
            {
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    control.ClickCommand?.Execute(null);
                };

                control.tappedBoxView.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }

        private static void IsTextOnlyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBindableEntry)bindable;
            control.frameTextView.IsVisible = true;
            control.frameView.IsVisible = false;
            control.lblError.IsVisible = true;
            control.entryTextValue.MaxLength = _maxTextLenght;
        }

        private void EntryValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsTextOnly)
            {
                return;
            }
            var validationResult = ValidateValue(this.entryValue.Text);
            this.lblError.IsVisible = !validationResult;
            this.frameView.BorderColor = validationResult ? _normalBorderColor : _errorBorderColor;
            this.MainValue = this.entryValue.Text;
        }

        private void EntryTextValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsTextOnly)
            {
                return;
            }
            var validationResult = ValidateValue(this.entryTextValue.Text);
            this.frameTextView.BorderColor = validationResult ? _normalBorderColor : _errorBorderColor;
            this.lblError.TextColor = this.entryTextValue.Text.Length > 0 ? _normalTextColor : _errorTextColor;
            this.MainValue = this.entryTextValue.Text;
            this.lblError.IsVisible = true;
            var currentTextLenght = this.entryTextValue.Text.Length > _maxTextLenght ? _maxTextLenght : this.entryTextValue.Text.Length;
            this.lblError.Text = $"{LocalStrings.Added} {currentTextLenght} {LocalStrings.SymbolsFrom} {_maxTextLenght}";
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            this.lblError.IsVisible = false;
            this.entryValue.TextChanged += EntryValue_TextChanged;
            this.entryTextValue.TextChanged += EntryTextValue_TextChanged;
            this.tappedBoxView.IsVisible = false;
            this.frameTextView.IsVisible = false;
        }

        private bool ValidateValue(string text)
        {
            if (text.Length == 0)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(ValidationPattern))
            {
                return true;
            }
            return Regex.IsMatch(text, ValidationPattern);
        }

        #endregion
    }
}