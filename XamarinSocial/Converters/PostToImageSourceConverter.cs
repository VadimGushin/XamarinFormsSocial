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
    public class PostToImageSourceConverter : IValueConverter
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

            var imagePlacement = parameter.ToString().ToEnum<PostMediaPlacementType>();
            if (imagePlacement == PostMediaPlacementType.None)
            {
                return null;
            }

            string source = GetImageSourceByPostImagePlacementType(imagePlacement, post.Content);

            return source;
        }

        private string GetImageSourceByPostImagePlacementType(PostMediaPlacementType type, IEnumerable<PostContent> content)
        {
            string source = null;

            int contentCount = content.Count();
            if (type == PostMediaPlacementType.MainImage)
            {
                if (contentCount != 1)
                {
                    return null;
                }

                var itemContent = content.FirstOrDefault();
                if (itemContent.ContentType == ContentType.Video)
                {
                    return null;
                }

                source = GetSourceByPostContent(itemContent);

                return source;
            }

            if (type == PostMediaPlacementType.FirstImage)
            {

                source = (contentCount > 1) ? GetSourceByPostContent(content.FirstOrDefault()) : null;
                return source;
            }

            if (type == PostMediaPlacementType.SecondImage)
            {
                source = (contentCount >= 2) ? content.Skip(1).FirstOrDefault().Source : null;
                return source;
            }

            if (type == PostMediaPlacementType.ThirdImage)
            {
                source = (contentCount >= 3) ? content.Skip(2).FirstOrDefault().Source : null;
                return source;
            }

            return source;
        }

        private string GetSourceByPostContent(PostContent content)
        {
            if (content.ContentType == ContentType.Image)
            {
                return content.Source;
            }

            return content.PreviewSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
