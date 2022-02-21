using System.Threading.Tasks;

namespace XamarinSocial.Services.Interfaces
{
    public interface ISecureStorageService
    {
        Task<string> GetStringByKey(string key);
        Task AddString(string key, string value);
        void ClearStorage();

        void ClearString(string key);
    }
}
