using System.Threading.Tasks;
using XamarinSocial.Models;

namespace XamarinSocial.Services.Interfaces
{
    public interface IDeviceLocationLocalService
    {
        Task<LocalDeviceLocationFetchResult> GetCurrentDeviceLocation();
    }
}
