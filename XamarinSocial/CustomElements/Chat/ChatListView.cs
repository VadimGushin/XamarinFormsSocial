using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinSocial.CustomElements.Chat
{
    public class ChatListView : ListView
    {
        #region Variables

        private bool _isLoaded = false;

        #endregion

        #region Constructors
        public ChatListView() : this(ListViewCachingStrategy.RecycleElement)
        {
        }

        public ChatListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            this.ItemSelected += OnItemSelected;
            this.ItemTapped += OnItemTapped;
            this.ItemAppearing += OnItemAppearing;
            this.ItemDisappearing += OnItemDisappering;
            this.Scrolled += ChatListView_Scrolled;
        }

        #endregion

        #region Properties

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public ICommand ItemAppearingCommand
        {
            get { return (ICommand)GetValue(ItemAppearingCommandProperty); }
            set { SetValue(ItemAppearingCommandProperty, value); }
        }

        public ICommand ItemDisappearingCommand
        {
            get { return (ICommand)GetValue(ItemDisappearingCommandProperty); }
            set { SetValue(ItemDisappearingCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(
                                                                        propertyName: nameof(TappedCommand),
                                                                        returnType: typeof(ICommand),
                                                                        declaringType: typeof(ChatListView),
                                                                        defaultValue: default(ICommand));

        public static readonly BindableProperty ItemAppearingCommandProperty = BindableProperty.Create(
                                                                               propertyName: nameof(ItemAppearingCommand),
                                                                               returnType: typeof(ICommand),
                                                                               declaringType: typeof(ChatListView),
                                                                               defaultValue: default(ICommand));

        public static readonly BindableProperty ItemDisappearingCommandProperty = BindableProperty.Create(
                                                                                  propertyName: nameof(ItemDisappearingCommand),
                                                                                  returnType: typeof(ICommand),
                                                                                  declaringType: typeof(ChatListView),
                                                                                  defaultValue: default(ICommand));

        #endregion

        #region Handlers

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ChatListView)sender;
            if (e == null)
            {
                return;
            }
            listView.SelectedItem = null;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            TappedCommand?.Execute(e.Item);
            SelectedItem = null;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (!_isLoaded)
            {
                return;
            }
            ItemAppearingCommand?.Execute(e.Item);
        }


        private void OnItemDisappering(object sender, ItemVisibilityEventArgs e)
        {
            ItemDisappearingCommand?.Execute(e.Item);
        }

        private void ChatListView_Scrolled(object sender, ScrolledEventArgs e)
        {
            //Is needed for iOS 
            _isLoaded = true;
            //this.Scrolled -= ChatListView_Scrolled;
        }

        #endregion

        #region Public Methods

        public void ScrollToFirst()
        {
            if (ItemsSource != null && ItemsSource.Cast<object>().Count() > 0)
            {
                var msg = ItemsSource.Cast<object>().FirstOrDefault();
                if (msg != null)
                {
                    ScrollTo(msg, ScrollToPosition.Start, false);
                }

            }
        }

        public void ScrollToLast()
        {
            if (ItemsSource != null && ItemsSource.Cast<object>().Count() > 0)
            {
                var msg = ItemsSource.Cast<object>().LastOrDefault();
                if (msg != null)
                {
                    ScrollTo(msg, ScrollToPosition.End, false);
                }
            }
        }

        public void ChangeIsLoadedValue()
        {
            _isLoaded = false;
        }

        #endregion
    }
}