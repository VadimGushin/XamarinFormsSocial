using System.Threading.Tasks;
using XamarinSocial.Models.Api.Requests;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Services.Interfaces
{
    public interface IFeedService
    {
        Task<ResponseModel<GetProfilePhotosModel>> GetProfilePhotos(int take, int offset);
        Task<ResponseModel<GetProfileFeedListModel>> GetProfileFeedList(int take, int offset);
        Task<ResponseModel<GetFeedListModel>> GetFeedList(int take, int offset);
        Task<ResponseModel> CreatePost(CreatePostModel postModel);
    }
}
