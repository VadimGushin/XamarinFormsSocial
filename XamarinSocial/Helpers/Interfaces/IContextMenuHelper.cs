using System.Collections.Generic;
using Xamarin.Forms;
using XamarinSocial.Models.Common;

namespace XamarinSocial.Helpers.Interfaces
{
    public interface IContextMenuHelper
    {
        Forms9Patch.BubblePopup GetPostContextMenuPopup(VisualElement target, IEnumerable<ContextMenuAction> actions);
    }
}
