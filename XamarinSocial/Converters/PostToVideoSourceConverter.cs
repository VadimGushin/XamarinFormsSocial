using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Converters
{
    public class PostToVideoSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var post = value as Post;
            if (post == null || !post.Content.Any())
            {
                return null;
            }
            if (post.Content == null || post.Content.Count() > 1)
            {
                return null;
            }

            return post.Content.FirstOrDefault().Source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
