using System.Collections.Generic;
using XamarinSocial.Models.Base;

namespace XamarinSocial.Models.Chat
{
    public class ChatModel : BaseApiModel
    {
        public List<ChatItemModel> Items { get; set; }

        #region Constructore

        public ChatModel()
        {
            Items = new List<ChatItemModel>();
        }

        #endregion
    }
    public class ChatItemModel : BaseApiModel
    {
        public string ImageSource { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Count { get; set; }
        public bool IsOnline { get; set; }
        public string OnlineStatusText { get; set; }
    }
}
