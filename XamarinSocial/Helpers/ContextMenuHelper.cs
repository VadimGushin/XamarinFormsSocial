using MvvmCross.Commands;
using P42.Utils;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinSocial.Enums;
using XamarinSocial.Helpers.Interfaces;
using XamarinSocial.Models.Common;

namespace XamarinSocial.Helpers
{
    public class ContextMenuHelper : IContextMenuHelper
    {
        public Forms9Patch.BubblePopup GetPostContextMenuPopup(VisualElement target, IEnumerable<ContextMenuAction> actions)
        {
            var popup = new Forms9Patch.BubblePopup(target)
            {
                HasShadow = false,
                PointerLength = 0,
                PointerTipRadius = 0,
                UsePoint = true,
                PointerCornerRadius = 0,
                BorderRadius = 10,
                Point = new Point(0, 0),
                Content = new StackLayout
                {
                    Padding = 7,
                },
            };

            IEnumerable<View> controls = GetMenuControls(actions, popup);
            (popup.Content as StackLayout).Children.AddRange(controls);

            return popup;
        }

        private IEnumerable<View> GetMenuControls(IEnumerable<ContextMenuAction> actions, Forms9Patch.BubblePopup popup)
        {
            List<View> controls = new List<View>();

            foreach (ContextMenuAction action in actions)
            {
                if (action.ActionType == ContextMenuActionType.EditPost)
                {
                    View editControl = GetContextMenuItemControl(popup, "Edit post", "edit_icon.png", "welcomStyle_i24", action.Command, action.CommandParametr);
                    controls.Add(editControl);
                    continue;
                }
                if (action.ActionType == ContextMenuActionType.DeletePost)
                {
                    View deleteControl = GetContextMenuItemControl(popup, "Delete post", "delete_icon.png", "welcomStyle_i25", action.Command, action.CommandParametr);
                    controls.Add(deleteControl);
                    continue;
                }
                if (action.ActionType == ContextMenuActionType.ReportPost)
                {
                    View reportControl = GetContextMenuItemControl(popup, "Report", "report_icon.png", "welcomStyle_i25", action.Command, action.CommandParametr);
                    controls.Add(reportControl);
                }
            }

            return controls;
        }

        private View GetContextMenuItemControl(Forms9Patch.BubblePopup popup, string text, string imageSource, string textStyle, ICommand clickCommand, object parametr)
        {
            var label = new Label()
            {
                Text = text,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Style = (Style)Application.Current.Resources[textStyle]
            };

            var image = new Image() { Source = imageSource, Aspect = Aspect.AspectFit, VerticalOptions = LayoutOptions.Center, Margin = 5 };
            var controlsContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5,
                Margin = 0,
                HeightRequest = 30,
                Children =
                    {
                        image,
                        label
                    },
                BackgroundColor = Color.Transparent
            };

            controlsContainer.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new MvxAsyncCommand(async (data) => {
                    await popup.CancelAsync();
                    clickCommand?.Execute(parametr);
                })
            });

            return controlsContainer;
        }
    }
}
