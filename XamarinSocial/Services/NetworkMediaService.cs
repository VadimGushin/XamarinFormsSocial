using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Common;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class NetworkMediaService : INetworkMediaService
    {
        //todo: remove hardcode
        public async Task<ResponseModel<List<UploadedMediaModel>>> UploadMedia(IEnumerable<LocalMediaModel> sources)
        {
            var result = new ResponseModel<List<UploadedMediaModel>>();
            result.IsSucceed = true;

            return result;
        }
    }
}
