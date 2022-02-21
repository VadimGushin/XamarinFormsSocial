using Xamarin.Forms;
using XamarinSocial.CustomElements.Chat.Cells;
using XamarinSocial.Models.Chat;

namespace XamarinSocial.Selectors
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;
        DataTemplate imageDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
            this.imageDataTemplate = new DataTemplate(typeof(ImageViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var chatItem = item as ChatConversationItemModel;
            if (chatItem == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(chatItem.ImageSource))
            {
                return imageDataTemplate;
            }
            return chatItem.IsOwn ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}
