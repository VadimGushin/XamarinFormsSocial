using System;
using System.Threading.Tasks;

namespace XamarinSocial.VideoHelper.Interfaces
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}
