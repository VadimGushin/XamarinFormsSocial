using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Enums.Notification;
using XamarinSocial.Helpers;

namespace XamarinSocial.CustomElements.Notification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationControl : ContentView
    {
        public NotificationControl()
        {
            InitializeComponent();
            InitControls();
        }

        #region Properties

        /// <summary>
        ///Identifier value of the NotificationControl
        /// </summary>
        public string IdValue
        {
            get { return base.GetValue(IdValueProperty).ToString(); }
            set { base.SetValue(IdValueProperty, value); }
        }

        /// <summary>
        ///Left top text of the NotificationControl
        /// </summary>
        public string LeftTopText
        {
            get { return base.GetValue(LeftTopTextProperty).ToString(); }
            set { base.SetValue(LeftTopTextProperty, value); }
        }

        /// <summary>
        ///Type of the NotificationControl
        /// </summary>
        public NotificationType NotifyType
        {
            get { return (NotificationType)base.GetValue(NotifyTypeProperty); }
            set { base.SetValue(NotifyTypeProperty, value); }
        }

        /// <summary>
        ///Action type of the NotificationControl
        /// </summary>
        public NotificationActionType NotifyActionType
        {
            get { return (NotificationActionType)base.GetValue(NotifyActionTypeProperty); }
            set { base.SetValue(NotifyActionTypeProperty, value); }
        }

        /// <summary>
        ///Image source of the NotificationControl
        /// </summary>
        public string ImageSource
        {
            get { return base.GetValue(ImageSourceProperty).ToString(); }
            set { base.SetValue(ImageSourceProperty, value); }
        }

        /// <summary>
        ///Message text of the NotificationControl
        /// </summary>
        public string MessageText
        {
            get { return base.GetValue(MessageTextProperty).ToString(); }
            set { base.SetValue(MessageTextProperty, value); }
        }

        /// <summary>
        ///Date of the NotificationControl
        /// </summary>
        public DateTime Date
        {
            get { return (DateTime)base.GetValue(DateProperty); }
            set { base.SetValue(DateProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Click command of NotificationControl
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
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: default);

        public static readonly BindableProperty LeftTopTextProperty = BindableProperty.Create(
                                                propertyName: nameof(LeftTopText),
                                                returnType: typeof(string),
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: LeftTopTextPropertyChanged);

        public static readonly BindableProperty NotifyTypeProperty = BindableProperty.Create(
                                                propertyName: nameof(NotifyType),
                                                returnType: typeof(NotificationType),
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: default);

        public static readonly BindableProperty NotifyActionTypeProperty = BindableProperty.Create(
                                                propertyName: nameof(NotifyActionType),
                                                returnType: typeof(NotificationActionType),
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: NotifyActionTypePropertyChanged);

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
                                                propertyName: nameof(ImageSource),
                                                returnType: typeof(string),
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ImageSourcePropertyChanged);

        public static readonly BindableProperty MessageTextProperty = BindableProperty.Create(
                                                propertyName: nameof(MessageText),
                                                returnType: typeof(string),
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: MessageTextPropertyChanged);

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
                                                propertyName: nameof(Date),
                                                returnType: typeof(DateTime),
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: default,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: DatePropertyChanged);

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
                                                propertyName: nameof(TapCommand),
                                                returnType: typeof(ICommand),
                                                declaringType: typeof(NotificationControl),
                                                defaultValue: null);

        #endregion

        #region Handlers

        private static void LeftTopTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (NotificationControl)bindable;
            control.leftTopTextView.Text = newValue.ToString();
        }

        private static void NotifyActionTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (NotificationControl)bindable;
            control.rightTopTextView.Text = EnumHelper.GetEnumDescription(Enum.Parse(typeof(NotificationActionType), newValue.ToString()));
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (NotificationControl)bindable;
            control.imageView.Source = newValue.ToString();
        }

        private static void MessageTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (NotificationControl)bindable;
            if (!string.IsNullOrWhiteSpace(newValue.ToString()))
            {
                control.messageTextView.Text = newValue.ToString();
                control.messageTextView.IsVisible = true;
            }
        }

        private static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (NotificationControl)bindable;
            var date = ((DateTime)newValue);
            var text = $"{date.ToString("M")} {XamarinSocial.Resources.Strings.Strings.At.ToLower()} {date.ToString("HH:mm")}";
            control.dateTextView.Text = text;
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