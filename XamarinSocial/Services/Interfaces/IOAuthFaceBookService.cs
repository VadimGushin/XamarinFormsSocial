using System.Threading.Tasks;
using XamarinSocial.Models;

namespace XamarinSocial.Services.Interfaces
{
    public interface IOAuthFacebookService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
