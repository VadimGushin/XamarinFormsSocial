using System.Collections.Generic;

namespace XamarinSocial.Models.Api.Responses
{
    public class GetFeedListModel
    {
        public GetFeedListModel()
        {
            Posts = new List<Post>();
        }

        public IEnumerable<Post> Posts { get; set; }
    }
}