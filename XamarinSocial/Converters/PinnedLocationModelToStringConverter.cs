using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinSocial.Models;

namespace XamarinSocial.Converters
{
    public class PinnedLocationModelToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var location = value as PinnedLocationModel;
            if (location == null)
            {
                return string.Empty;
            }

            string text = string.IsNullOrEmpty(location.SecondLevelArea) 
                ? location.FirstLevelArea : $"{location.SecondLevelArea}, {location.FirstLevelArea}";

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
