using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSocial.CustomElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomNavigationBar : ContentView
    {
        public CustomNavigationBar()
        {
            InitializeComponent();
            InitControls();
        }

        #region Properties

        /// <summary>
        /// Title text of the CustomNavigationBar
        /// </summary>
        public string Title
        {
            get { return base.GetValue(TitleProperty).ToString(); }
            set { base.SetValue(TitleProperty, value); }
        }


        /// <summary>
        /// Left image source of the CustomNavigationBar
        /// </summary>
        public string LeftImage
        {
            get { return base.GetValue(LeftImageProperty).ToString(); }
            set { base.SetValue(LeftImageProperty, value); }
        }

        /// <summary>
        /// Right image source of the CustomNavigationBar
        /// </summary>
        public string RightImage
        {
            get { return base.GetValue(RightImageProperty).ToString(); }
            set { base.SetValue(RightImageProperty, value); }
        }

        /// <summary>
        /// Background color of the CustomNavigationBar
        /// </summary>
        public Color BackgroundViewColor
        {
            get { return (Color)base.GetValue(BackgroundViewColorProperty); }
            set { base.SetValue(BackgroundViewColorProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Left button click command of CustomNavigationBar
        /// </summary>
        public ICommand LeftButtonClickCommand
        {
            get { return (ICommand)GetValue(LeftButtonClickCommandProperty); }
            set { SetValue(LeftButtonClickCommandProperty, value); }
        }

        /// <summary>
        /// Right button click command of CustomNavigationBar
        /// </summary>
        public ICommand RightButtonClickCommand
        {
            get { return (ICommand)GetValue(RightButtonClickCommandProperty); }
            set { SetValue(RightButtonClickCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                propertyName: nameof(Title),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomNavigationBar),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty LeftImageProperty = BindableProperty.Create(
                                                propertyName: nameof(LeftImage),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomNavigationBar),
                                                defaultValue: string.Empty,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: LeftImagePropertyChanged);

        public static readonly BindableProperty RightImageProperty = BindableProperty.Create(
                                                propertyName: nameof(RightImage),
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomNavigationBar),
                                                defaultValue: string.Empty,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: RightImagePropertyChanged);

        public static readonly BindableProperty BackgroundViewColorProperty = BindableProperty.Create(
                                                propertyName: nameof(BackgroundViewColor),
                                                returnType: typeof(Color),
                                                declaringType: typeof(CustomNavigationBar),
                                                defaultValue: Color.Default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: BackgroundViewColorPropertyChanged);

        public static readonly BindableProperty LeftButtonClickCommandProperty =
                                                BindableProperty.Create(
                                                propertyName: nameof(LeftButtonClickCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(CustomNavigationBar),
                                                defaultValue: null);

        public static readonly BindableProperty RightButtonClickCommandProperty =
                                                BindableProperty.Create(
                                                propertyName: nameof(RightButtonClickCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(CustomNavigationBar),
                                                defaultValue: null);

        #endregion

        #region Handlers

        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomNavigationBar)bindable;
            control.titleText.Text = newValue.ToString();
        }

        private static void LeftImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomNavigationBar)bindable;
            control.leftButton.Source = (string)newValue;
        }

        private static void RightImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomNavigationBar)bindable;
            control.rightButton.Source = (string)newValue;
        }

        private static void BackgroundViewColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomNavigationBar)bindable;
            control.parentLayout.BackgroundColor = (Color)newValue;
        }

        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            LeftButtonClickCommand?.Execute(null);
        }

        private void RightButton_Clicked(object sender, EventArgs e)
        {
            RightButtonClickCommand?.Execute(null);
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            this.leftButton.Clicked += LeftButton_Clicked;
            this.rightButton.Clicked += RightButton_Clicked;
        }

        #endregion
    }
}