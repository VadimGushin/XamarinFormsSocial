using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinSocial.Converters
{
    public class NullToBoolConverslyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
