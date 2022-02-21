using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinSocial.Converters
{
    public class StringIsNullOrEmptyToBoolConverslyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isNullOrEmpty = string.IsNullOrEmpty(value as string);

            return !isNullOrEmpty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
