using System;
using Xamarin.Essentials;

namespace XamarinSocial.Models
{
    public class LocalDeviceLocationFetchResult
    {
        public bool IsSucceed { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public Location Location { get; set; }
    }
}
