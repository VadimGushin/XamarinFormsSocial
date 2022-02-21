﻿using Xamarin.Forms.Xaml;
using XamarinSocial.ViewModels.Settings;
using XamarinSocial.Views.Base;

namespace XamarinSocial.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialNetworkSettingsPage : BaseContentPage<SocialNetworkSettingsViewModel>
    {
        public SocialNetworkSettingsPage()
        {
            InitializeComponent();
        }
    }
}