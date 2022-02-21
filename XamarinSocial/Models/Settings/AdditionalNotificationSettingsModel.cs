using System.Collections.Generic;

namespace XamarinSocial.Models.Settings
{
    public class AdditionalNotificationSettingsModel
    {
        #region Properties
        public Dictionary<string, string> Values { get; set; }
        public string SelectedValue { get; set; }
        public string Title { get; set; }
        public bool IsNotificationTurned { get; set; }

        #endregion

        #region Constructors

        public AdditionalNotificationSettingsModel()
        {           
        }

        public AdditionalNotificationSettingsModel(Dictionary<string, string> values, string selectedValue, string title, bool isTurned = false)
        {
            Values = values;
            SelectedValue = selectedValue;
            Title = title;
            IsNotificationTurned = isTurned;
        }

        #endregion
    }
}
