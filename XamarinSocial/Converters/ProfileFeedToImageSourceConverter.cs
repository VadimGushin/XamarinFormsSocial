using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinSocial.Enums;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Converters
{
    public class ProfileFeedToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            var profileFeed = value as ProfileFeed;
            if (profileFeed.Content == null)
            {
                return null;
            }
            if (profileFeed.Content.ContentType == ContentType.Image)
            {
                return profileFeed.Content.Source;
            }
            if (profileFeed.Content.ContentType == ContentType.Video)
            {
                return profileFeed.Content.PreviewSource;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
