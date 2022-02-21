using System.Threading.Tasks;

namespace XamarinSocial.Interfaces
{
    public interface ICollectionViewModel
    {
        Task OnItemSelectedAsync(string id);
        Task LoadMoreAsync();
    }
}
