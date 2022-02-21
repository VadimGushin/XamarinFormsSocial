using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace XamarinSocial
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            Device.SetFlags(new string[] 
            { 
                "RadioButton_Experimental",
                "Markup_Experimental",
                "Brush_Experimental"
            });
            InitializeComponent();
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
