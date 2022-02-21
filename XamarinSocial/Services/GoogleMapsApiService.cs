using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinSocial.Constants;
using XamarinSocial.Models;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class GoogleMapsApiService : BaseApiService, IGoogleMapsApiService
    {
        public async Task<GooglePlace> GetPlaceDetails(string placeId)
        {
            GooglePlace result = default;

            var url = string.Format(
               Constant.GoogleConfig.GOOGLE_PLACES_API_DETAILS,
               placeId,
               Constant.GoogleConfig.GOOGLE_API_KEY);

            var response = await ExecuteGetAsync<GooglePlace>(url);
            if (!response.IsSuccessStatusCode)
            {
                result.Errors.Add(response.ReasonPhrase);
                return result;
            }
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json.Equals(Constant.StringConstants.ERROR))
            {
                result.Errors.Add(Constant.StringConstants.FAIL_JSON_PARSE);
                return result;
            }
            result = new GooglePlace(JObject.Parse(json));
            return result;
        }

        public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
        {
            GooglePlaceAutoCompleteResult results = default;
            var url = string.Format(
                Constant.GoogleConfig.GOOGLE_PLACES_API_AUTO_COMPLETE_PATH,
                Constant.GoogleConfig.GOOGLE_API_KEY,
                text);
            var response = await ExecuteGetAsync<GooglePlaceAutoCompleteResult>(url);

            if (!response.IsSuccessStatusCode)
            {
                results.Errors.Add(response.ReasonPhrase);
                return results;
            }
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json.Equals(Constant.StringConstants.ERROR))
            {
                results.Errors.Add(Constant.StringConstants.FAIL_JSON_PARSE);
                return results;
            }
            results = JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json);
            return results;
        }

        public async Task<ResponseModel<GooglePlaceReverseGeocodingResult>> GetReverseGeocodingData(Location location)
        {
            var result = new ResponseModel<GooglePlaceReverseGeocodingResult>(); ;

            var url = string.Format(
                CultureInfo.InvariantCulture,
                Constant.GoogleConfig.GOOGLE_PLACES_API_GEOCODING_PATH, 
                location.Latitude, 
                location.Longitude, 
                Constant.GoogleConfig.GOOGLE_API_KEY);
            var response = await ExecuteGetAsync<GooglePlaceReverseGeocodingResult>(url);

            if (!response.IsSuccessStatusCode)
            {
                result.Errors.Add(response.ReasonPhrase);
                result.IsSucceed = false;
                return result;
            }
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json.Equals(Constant.StringConstants.ERROR))
            {
                result.Errors.Add(Constant.StringConstants.FAIL_JSON_PARSE);
                result.IsSucceed = false;
                return result;
            }

            result.Data = JsonConvert.DeserializeObject<GooglePlaceReverseGeocodingResult>(json);
            result.IsSucceed = true;
            return result;
        }
    }
}
