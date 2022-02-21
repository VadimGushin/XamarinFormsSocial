using MvvmCross;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSocial.Helpers.Interfaces;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Common;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostCellHeader : ContentView
    {
        private readonly IContextMenuHelper _contextMenuHelper;
        private readonly ILocationFetcherService _locationFetcherService;

        private Forms9Patch.BubblePopup _contextMenuPopup;

        public static readonly BindableProperty EditPostCommandAsyncProperty 
            = BindableProperty.Create(nameof(EditPostCommandAsync), typeof(IMvxAsyncCommand<Post>), typeof(PostCellHeader), null);

        public static readonly BindableProperty DeletePostCommandAsyncProperty 
            = BindableProperty.Create(nameof(DeletePostCommandAsync), typeof(IMvxAsyncCommand<Post>), typeof(PostCellHeader), null);

        public static readonly BindableProperty ReportPostCommandAsyncProperty 
            = BindableProperty.Create(nameof(ReportPostCommandAsync), typeof(IMvxAsyncCommand<Post>), typeof(PostCellHeader), null);

        public IMvxAsyncCommand<Post> EditPostCommandAsync
        {
            get { return (IMvxAsyncCommand<Post>)base.GetValue(EditPostCommandAsyncProperty); }
            set { base.SetValue(EditPostCommandAsyncProperty, value); }
        }
        public IMvxAsyncCommand<Post> DeletePostCommandAsync
        {
            get { return (IMvxAsyncCommand<Post>)base.GetValue(DeletePostCommandAsyncProperty); }
            set { base.SetValue(DeletePostCommandAsyncProperty, value); }
        }
        public IMvxAsyncCommand<Post> ReportPostCommandAsync
        {
            get { return (IMvxAsyncCommand<Post>)base.GetValue(ReportPostCommandAsyncProperty); }
            set { base.SetValue(ReportPostCommandAsyncProperty, value); }
        }

        public PostCellHeader()
        {
            InitializeComponent();
            _contextMenuHelper = Mvx.IoCProvider.Resolve<IContextMenuHelper>();
            _locationFetcherService = Mvx.IoCProvider.Resolve<ILocationFetcherService>();
        }

        public void SettingsTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var actionsList = new List<ContextMenuAction>();

            var post = BindingContext as Post;

            if (post != null && post.IsOwnPost)
            {
                actionsList.AddRange(new ContextMenuAction[] { new ContextMenuAction() { ActionType = Enums.ContextMenuActionType.EditPost, Command = EditPostCommandAsync, CommandParametr = BindingContext},
                                                               new ContextMenuAction() { ActionType = Enums.ContextMenuActionType.DeletePost, Command = DeletePostCommandAsync, CommandParametr = BindingContext}});

            }
            if (post != null && !post.IsOwnPost)
            {
                actionsList.AddRange(new ContextMenuAction[] { new ContextMenuAction() { ActionType = Enums.ContextMenuActionType.ReportPost, Command = ReportPostCommandAsync, CommandParametr = BindingContext } });

            }

            _contextMenuPopup = _contextMenuHelper.GetPostContextMenuPopup(imgSettings, actionsList);

            PointF point = _locationFetcherService.GetCoordinates(imgSettings);

            double heightFormatted = (DeviceInfo.Platform == DevicePlatform.iOS) ? 
                Application.Current.MainPage.Height : 
                DeviceDisplay.MainDisplayInfo.Height;

            _contextMenuPopup.PointerDirection = (heightFormatted / 2 > point.Y) ?
                Forms9Patch.PointerDirection.Up :
                Forms9Patch.PointerDirection.Down;

            _contextMenuPopup.IsVisible = true;
        }
    }
}