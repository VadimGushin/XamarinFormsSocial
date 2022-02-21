using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        
        public async Task AddString(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }

        public void ClearStorage()
        {
            SecureStorage.RemoveAll();
        }

        public void ClearString(string key)
        {
            SecureStorage.Remove(key);
        }

        public Task<string> GetStringByKey(string key)
        {
            return SecureStorage.GetAsync(key);
        }
    }
}
