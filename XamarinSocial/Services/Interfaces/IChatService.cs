using System.Threading.Tasks;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Chat;

namespace XamarinSocial.Services.Interfaces
{
    public interface IChatService
    {
        Task<ResponseModel<ChatModel>> GetByIdAsync(int skip, int take, string id = "");
        Task<ResponseModel<ChatConversationModel>> GetConservationByIdAsync(int skip, int take, string id = "");
    }
}
