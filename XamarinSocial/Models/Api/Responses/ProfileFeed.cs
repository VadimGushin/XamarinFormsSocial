using System;
using XamarinSocial.Enums;
using XamarinSocial.ViewModels;

namespace XamarinSocial.Models.Api.Responses
{
    public class ProfileFeed : BaseCellModel
    {
        public Guid Id { get; set; }
        public DateTime DateCreatedUtc { get; set; }
        public PostType PostType { get; set; }
        public string Description { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public PostContent Content { get; set; }
        public PinnedLocationModel PinnedLocation { get; set; }
        public ProfileFeed()
        {
            LikeCount = 0;
            CommentCount = 0;
        }
    }
}
