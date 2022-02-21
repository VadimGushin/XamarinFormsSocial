using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSocial.CustomElements.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageSvgButton : ContentView
    {
        public ImageSvgButton()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        ///Image source of the ImageSvgButton
        /// </summary>
        public string ImageSource
        {
            get { return base.GetValue(ImageSourceProperty).ToString(); }
            set { base.SetValue(ImageSourceProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Click command of ImageSvgButton
        /// </summary>
        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
                                                propertyName: nameof(ImageSource),
                                                returnType: typeof(string),
                                                declaringType: typeof(ImageSvgButton),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ImageSourcePropertyChanged);

        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
                                                propertyName: nameof(ClickCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(ImageSvgButton),
                                                defaultValue: null);

        #endregion

        #region Handlers

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ImageSvgButton)bindable;
            control.image.Source = newValue.ToString();
        }

        private void OnTapped(object sender, System.EventArgs e)
        {
            this.ClickCommand?.Execute(null);
        }

        #endregion

    }
}