using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSocial.Views.Feed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePostHorizontalImageList : ContentView
    {
        public event Action AddContentPressed;

        public CreatePostHorizontalImageList()
        {
            InitializeComponent();
        }

        private void AddContentTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            AddContentPressed?.Invoke();
        }
    }
}