using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using XamarinSocial.Models.Common;

namespace XamarinSocial.ValidationAttributes
{
    public class MediaSourceValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var media = value as IEnumerable<LocalMediaModel>;

            bool isValid = (media != null && media.Any());
            return isValid;
        }
    }
}
