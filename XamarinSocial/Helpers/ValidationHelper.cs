using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using XamarinSocial.Helpers.Interfaces;

namespace XamarinSocial.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        public bool Validate(object model, bool validateAllProperties = true)
        {
            var context = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, context, validationResults, validateAllProperties);
        }

        public bool ValidateProperty(object property, string propertyName, object modelContext)
        {
            var context = new ValidationContext(modelContext, new Dictionary<object, object>() { { propertyName, property } });
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(modelContext, context, validationResults, true);

            if (!validationResults.Any())
            {
                return true;
            }

            bool isNotValid = validationResults.Any(x => x.MemberNames.Contains(propertyName));
            return !isNotValid;
        }
    }
}
