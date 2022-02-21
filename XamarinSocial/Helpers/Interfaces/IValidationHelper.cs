namespace XamarinSocial.Helpers.Interfaces
{
    public interface IValidationHelper
    {
        bool Validate(object model, bool validateAllProperties = true);
        bool ValidateProperty(object property, string propertyName, object modelContext);
    }
}
