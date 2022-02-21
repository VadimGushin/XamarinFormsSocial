using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinSocial.Models;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Services.Interfaces
{

    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
        Task<ResponseModel<GooglePlaceReverseGeocodingResult>> GetReverseGeocodingData(Location location);
    }
}
