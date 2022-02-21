using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinSocial.Helpers.Converters
{
    public class RadiusConverter : IValueConverter
    {
        public const int Half = 2;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var height = value as double?;
            return height / Half ?? default(int);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
