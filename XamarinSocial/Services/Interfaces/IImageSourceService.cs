using System.Threading.Tasks;
using XamarinSocial.Models.Photo;

namespace XamarinSocial.Services.Interfaces
{
    public interface IImageSourceService
    {
        Task<ImagesSourceModel> GetImageFromCameraAsync();
        Task<ImagesSourceModel> GetImageFromLibraryAsync();
    }
}
