using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using XamarinSocial.Enums;
using XamarinSocial.Extensions;
using XamarinSocial.Models.Api.Responses;

namespace XamarinSocial.Converters
{
    public class ImagePostToIsVisibleBoolConverter : IValueConverter
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

            var imagePlacement = parameter.ToString().ToEnum<PostMediaPlacementType>();
            if (imagePlacement == PostMediaPlacementType.None)
            {
                return false;
            }

            bool isVisible = CheckIsImageVisible(imagePlacement, post.Content);

            return isVisible;
        }

        private bool CheckIsImageVisible(PostMediaPlacementType type, IEnumerable<PostContent> content)
        {
            bool source = false;

            int contentCount = content.Count();
            if (type == PostMediaPlacementType.MainImage)
            {
                source = (contentCount == 1);
                return source;
            }

            if (type == PostMediaPlacementType.FirstImage)
            {
                source = (contentCount > 1);
                return source;
            }

            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

