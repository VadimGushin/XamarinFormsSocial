using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSocial.Models
{
    public class GooglePlaceReverseGeocodingResult
    {
        [JsonProperty("plus_code")]
        public GooglePlacePlusCode PlusCode { get; set; }

        [JsonProperty("results")]
        public List<GooglePlaceResult> Result { get; set; }
    }

    public class GooglePlacePlusCode
    {
        [JsonProperty("compound_code")]
        public string CompoundCode { get; set; }

        [JsonProperty("global_code")]
        public string GlobalCode { get; set; }
    }

    #region Result

    public class GooglePlaceResult
    {
        [JsonProperty("address_components")]
        public List<AddressComponent> AdressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public GooglePlaceGeometry Geometry { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    #region Geometry
    public class GooglePlaceGeometry
    {
        [JsonProperty("location")]
        public GooglePlaceLocation Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("viewport")]
        public GooglePlaceViewPort ViewPort { get; set; }
    }

    public class GooglePlaceViewPort
    {
        [JsonProperty("northeast")]
        public GooglePlaceLocation NorthEast { get; set; }

        [JsonProperty("southwest")]
        public GooglePlaceLocation SouthWest { get; set; }
    }

    public class GooglePlaceLocation
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }
    #endregion Geometry
    #endregion Result
}
