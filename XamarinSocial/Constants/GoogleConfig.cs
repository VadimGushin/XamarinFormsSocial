namespace XamarinSocial.Constants
{
    public static partial class Constant 
    {
        public static class GoogleConfig
        {
            public const string GOOGLE_API_KEY = "";
            public const string GOOGLE_PLACES_API_BASE_PATH = "https://maps.googleapis.com/maps";
            public const string GOOGLE_PLACES_API_AUTO_COMPLETE_PATH = "https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}&input={1}";
            public const string GOOGLE_PLACES_API_DETAILS = "https://maps.googleapis.com/maps/api/place/details/json?placeid={0}&key={1}";
            public const string GOOGLE_PLACES_API_GEOCODING_PATH = "https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}";
            public const string GOOGLE_PLACES_API_NEARBY_SEARCH_PATH = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&key={3}";

            public const int NEARBY_SEARCH_RADIUS_METERS = 500;
        }
    }
}
