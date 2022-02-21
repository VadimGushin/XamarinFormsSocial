using System.ComponentModel;

namespace XamarinSocial.Enums.Chat
{
    public enum ChatConversationMenuActionType
    {
        None = 0,

        [Description("Search Chat History")]
        SearchChatHistory = 1,

        Unmatch = 2,
        Block = 3,

        [Description("Report User")]
        ReportUser = 4
    }
}
