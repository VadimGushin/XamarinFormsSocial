using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.TabsView;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.TabsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class ChatPage : BaseContentPage<ChatViewModel>
    {
        public ChatPage()
        {
            InitializeComponent();
        }

        #region Overrides

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    this.IconImageSource = "img_chatRedIcon.png";
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    this.IconImageSource = "img_chatIcon.png";
        //}

        #endregion
    }
}