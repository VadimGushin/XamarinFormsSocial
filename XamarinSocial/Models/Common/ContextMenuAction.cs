using System.Windows.Input;
using XamarinSocial.Enums;

namespace XamarinSocial.Models.Common
{
    public class ContextMenuAction
    {
        public ContextMenuActionType ActionType { get; set; }
        public ICommand Command { get; set; }
        public object CommandParametr { get; set; }
    }
}
