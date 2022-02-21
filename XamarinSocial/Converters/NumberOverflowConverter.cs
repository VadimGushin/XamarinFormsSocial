using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinSocial.Converters
{
    public class NumberOverflowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            int.TryParse(value.ToString(), out var number);
            if (number >= 100000000)
                return (number / 1000000).ToString("#,0M");

            if (number >= 10000000)
                return (number / 1000000).ToString("0.#") + "M";

            if (number >= 100000)
                return (number / 1000).ToString("#,0K");

            if (number >= 10000)
                return (number / 1000).ToString("0.#") + "K";

            return number.ToString("#,0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
