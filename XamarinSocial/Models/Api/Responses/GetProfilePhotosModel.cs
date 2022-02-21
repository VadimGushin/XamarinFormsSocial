using System.Collections.Generic;

namespace XamarinSocial.Models.Api.Responses
{
    public class GetProfilePhotosModel
    {
        public IEnumerable<PhotoModel> Photos { get; set; }
        public GetProfilePhotosModel()
        {
            Photos = new List<PhotoModel>();
        }
    }
}
