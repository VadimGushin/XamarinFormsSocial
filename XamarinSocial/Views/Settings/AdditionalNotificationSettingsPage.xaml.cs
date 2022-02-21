using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.Settings;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdditionalNotificationSettingsPage : BaseContentPage<AdditionalNotificationSettingsViewModel>
    {
        public AdditionalNotificationSettingsPage()
        {
            InitializeComponent();
        }
    }
}