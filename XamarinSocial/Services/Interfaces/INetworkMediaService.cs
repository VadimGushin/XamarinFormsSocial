using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Common;

namespace XamarinSocial.Services.Interfaces
{
    public interface INetworkMediaService
    {
        Task<ResponseModel<List<UploadedMediaModel>>> UploadMedia(IEnumerable<LocalMediaModel> sources);
    }
}
