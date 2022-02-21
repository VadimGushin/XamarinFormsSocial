using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Converters
{
    public class PostToIsVisibleAdditionalElementsBoolConverter : IValueConverter
    {
        public const int BASE_CONTENT_COUNT = 3;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            var post = value as Post;
            if (post == null)
            {
                return false;
            }

            var additionalCount = post.Content.Count() - BASE_CONTENT_COUNT;
            bool isVisible = (additionalCount > 0);

            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}