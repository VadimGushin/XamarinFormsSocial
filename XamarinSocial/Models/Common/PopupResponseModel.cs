using XamarinSocial.Enums;

namespace XamarinSocial.Models.Common
{
    public class PopupResponseModel
    {
        public UserSearchDropdownTypes SourceType { get; set; }
        public string SelectedValue { get; set; }

        public PopupResponseModel()
        { }

        public PopupResponseModel(UserSearchDropdownTypes sourceType, string selectedValue)
        {
            SourceType = sourceType;
            SelectedValue = selectedValue;
        }
    }
}
