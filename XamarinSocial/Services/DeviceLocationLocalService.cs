using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinSocial.Models;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class DeviceLocationLocalService : IDeviceLocationLocalService
    {
        public async Task<LocalDeviceLocationFetchResult> GetCurrentDeviceLocation()
        {
            var fetchResult = new LocalDeviceLocationFetchResult();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                Location location = await Geolocation.GetLocationAsync(request);

                if (location == null)
                {
                    throw new Exception();
                }

                fetchResult.IsSucceed = true;
                fetchResult.Location = location;
            }
            catch (FeatureNotSupportedException featureNotSupportedException)
            {
                // Handle not supported on device exception
                fetchResult.IsSucceed = false;
                fetchResult.Exception = featureNotSupportedException;
                fetchResult.ErrorMessage = "Feature Not Supported";
            }
            catch (FeatureNotEnabledException featureNotEnabledException)
            {
                // Handle not enabled on device exception
                fetchResult.IsSucceed = false;
                fetchResult.Exception = featureNotEnabledException;
                fetchResult.ErrorMessage = "Feature Not Enabled";
            }
            catch (PermissionException permissionException)
            {
                // Handle permission exception
                fetchResult.IsSucceed = false;
                fetchResult.Exception = permissionException;
                fetchResult.ErrorMessage = "Permissions Not Granted";
            }
            catch (Exception exception)
            {
                // Unable to get location
                fetchResult.IsSucceed = false;
                fetchResult.Exception = exception;
                fetchResult.ErrorMessage = "Unable to get location";
            }

            return fetchResult;
        }
    }
}
