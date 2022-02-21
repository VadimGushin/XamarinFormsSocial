using MvvmCross.Plugin.Messenger;
using XamarinSocial.Enums;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Messages
{
    public class UpdateFeedItemMessage : MvxMessage
    {
        public Post Post { get; private set; }

        public CollectionItemEventType CollectionItemEventType { get; private set; }

        public UpdateFeedItemMessage(object sender, Post post, CollectionItemEventType eventType)
            : base(sender)
        {
            Post = post;
            CollectionItemEventType = eventType;
        }
    }
}
