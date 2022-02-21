using System.Collections.Generic;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Models.Api.Requests
{
    public class CreatePostModel
    {
        public IEnumerable<UploadedMediaModel> MediaSources { get; set; }

        public string Description { get; set; }

        public PinnedLocationModel PinnedLocation { get; set; }
    }
}
