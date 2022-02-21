using MvvmCross.Binding.Extensions;
using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Converters
{
    public class PostToAdditionalElementsStringConverter : IValueConverter
    {
        public const int BASE_CONTENT_COUNT = 3;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var post = value as Post;
            if (post == null)
            {
                return string.Empty;
            }

            var additionalCount = post.Content.Count() - BASE_CONTENT_COUNT;
            if (additionalCount <= 0)
            {
                return string.Empty;
            }

            var formattedTitle = $"+{additionalCount}";
            return formattedTitle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
