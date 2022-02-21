using System.Threading.Tasks;
using XamarinSocial.Models;

namespace XamarinSocial.Services.Interfaces
{
    public interface IOAuthGoogleService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
