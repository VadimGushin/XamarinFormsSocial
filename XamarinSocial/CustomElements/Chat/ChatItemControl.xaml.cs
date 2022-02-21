using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Extensions;

namespace XamarinSocial.CustomElements.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatItemControl : ContentView
    {
        public ChatItemControl()
        {
            InitializeComponent();
            InitControls();
        }

        #region Properties

        /// <summary>
        ///Identifier value of the ChatItemControl
        /// </summary>
        public string IdValue
        {
            get { return base.GetValue(IdValueProperty).ToString(); }
            set { base.SetValue(IdValueProperty, value); }
        }

        /// <summary>
        ///Top user name of the ChatItemControl
        /// </summary>
        public string UserName
        {
            get { return base.GetValue(UserNameProperty).ToString(); }
            set { base.SetValue(UserNameProperty, value); }
        }

        /// <summary>
        ///Image source of the ChatItemControl
        /// </summary>
        public string ImageSource
        {
            get { return base.GetValue(ImageSourceProperty).ToString(); }
            set { base.SetValue(ImageSourceProperty, value); }
        }

        /// <summary>
        ///Message text of the ChatItemControl
        /// </summary>
        public string MessageText
        {
            get { return base.GetValue(MessageTextProperty).ToString(); }
            set { base.SetValue(MessageTextProperty, value); }
        }

        /// <summary>
        ///Indicates whether the user is online of the ChatItemControl
        /// </summary>
        public bool IsOnline
        {
            get { return (bool)base.GetValue(IsOnlineProperty); }
            set { base.SetValue(IsOnlineProperty, value); }
        }

        /// <summary>
        ///Date of the ChatItemControl
        /// </summary>
        public DateTime Date
        {
            get { return (DateTime)base.GetValue(DateProperty); }
            set { base.SetValue(DateProperty, value); }
        }

        /// <summary>
        ///Badge count text of the ChatItemControl
        /// </summary>
        public string BadgeCount
        {
            get { return base.GetValue(BadgeCountProperty).ToString(); }
            set { base.SetValue(BadgeCountProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Click command of ChatItemControl
        /// </summary>
        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty IdValueProperty = BindableProperty.Create(
                                                propertyName: nameof(IdValue),
                                                returnType: typeof(string),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: default);

        public static readonly BindableProperty UserNameProperty = BindableProperty.Create(
                                                propertyName: nameof(UserName),
                                                returnType: typeof(string),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: UserNamePropertyChanged);

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
                                                propertyName: nameof(ImageSource),
                                                returnType: typeof(string),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ImageSourcePropertyChanged);

        public static readonly BindableProperty MessageTextProperty = BindableProperty.Create(
                                                propertyName: nameof(MessageText),
                                                returnType: typeof(string),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: MessageTextPropertyChanged);

        public static readonly BindableProperty IsOnlineProperty = BindableProperty.Create(
                                                propertyName: nameof(MessageText),
                                                returnType: typeof(bool),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: IsOnlinePropertyChanged);

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
                                                propertyName: nameof(Date),
                                                returnType: typeof(DateTime),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: DatePropertyChanged);

        public static readonly BindableProperty BadgeCountProperty = BindableProperty.Create(
                                                propertyName: nameof(BadgeCount),
                                                returnType: typeof(string),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: BadgeCountPropertyChanged);

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
                                                propertyName: nameof(TapCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(ChatItemControl),
                                                defaultValue: null);

        #endregion

        #region Handlers

        private static void UserNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ChatItemControl)bindable;
            control.userNameView.Text = newValue.ToString();
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ChatItemControl)bindable;
            control.imageView.Source = newValue.ToString();
        }

        private static void MessageTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ChatItemControl)bindable;
            control.messageTextView.Text = newValue.ToString();
        }

        private static void IsOnlinePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ChatItemControl)bindable;
            control.onlineView.IsVisible = true;
        }

        private static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ChatItemControl)bindable;
            control.dateTextView.Text = ((DateTime)newValue).GetTimeString();
        }

        private static void BadgeCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ChatItemControl)bindable;
            var text = newValue.ToString();
            if (!string.IsNullOrWhiteSpace(text))
            {
                control.badgeView.Value = text;
                control.badgeView.IsVisible = true;
            }

        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            //var tapGesture = new TapGestureRecognizer();
            //tapGesture.Tapped += (s, e) => 
            //{
            //    this.TapCommand?.Execute(Id);
            //};

            //this.mainLayout.GestureRecognizers.Add(tapGesture);
        }

        #endregion
    }
}