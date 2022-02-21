using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSocial.Models.Api.Responses
{
    public class GetProfileFeedListModel
    {
        public IEnumerable<ProfileFeed> ProfileFeeds { get; set; }
        public GetProfileFeedListModel()
        {
            ProfileFeeds = new List<ProfileFeed>();
        }
    }
}
