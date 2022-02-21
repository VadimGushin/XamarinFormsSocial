using System.Threading.Tasks;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Services.Interfaces
{
    public interface IUserSearchService
    {
        Task<ResponseModel<GetSearchUserModel>> GetSearchUserList(int take, int offset);
    }
}
