using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Converters
{
    public class HaveMediaContentToIsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            var post = value as Post;
            if (post == null || !post.Content.Any())
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
