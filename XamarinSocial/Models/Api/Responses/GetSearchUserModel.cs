using System.Collections.Generic;

namespace XamarinSocial.Models.Api.Responses
{
    public class GetSearchUserModel
    {
        public GetSearchUserModel()
        {
            SearchUsers = new List<SearchUserModel>();
        }

        public IEnumerable<SearchUserModel> SearchUsers { get; set; }
    }
}
