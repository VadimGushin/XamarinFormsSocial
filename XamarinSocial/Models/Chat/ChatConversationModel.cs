using System.Collections.Generic;
using XamarinSocial.Models.Base;

namespace XamarinSocial.Models.Chat
{
    public class ChatConversationModel : BaseApiModel
    {
        public List<ChatConversationItemModel> Items { get; set; }

        public ChatConversationModel()
        {
            Items = new List<ChatConversationItemModel>();
        }
    }

    public class ChatConversationItemModel : BaseApiModel
    {
        public string Message { get; set; }
        public bool IsOwn { get; set; }
        public string ImageSource { get; set; }
        public bool HasTopDate { get; set; } = false;
        public string ShortDateString { get; set; }
        public string TopDateString { get; set; }

        public ChatConversationItemModel()
        {
            Message = string.Empty;
            ImageSource = string.Empty;
            HasTopDate = false;
        }
    }
}
