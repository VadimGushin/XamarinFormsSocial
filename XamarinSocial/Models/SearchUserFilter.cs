using XamarinSocial.Enums;

namespace XamarinSocial.Models
{
    public class SearchUserFilter : BaseNotifyModel
    {
        public Gender PreferencesGender { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Location { get; set; }
        public string LocationOtherStrings { get; set; }
        public string GoogleLocationId { get; set; }
        public double MinDistance { get; set; }
        public double MaxDistance { get; set; }
        public bool IsPopular { get; set; }
        public bool IsActiveUsers { get; set; }

        private string _ethnicity { get; set; }
        public string Ethnicity
        {
            get => _ethnicity;
            set
            {
                _ethnicity = value;
                OnPropertyChanged("Ethnicity");
            }
        }

        private string _minHeight { get; set; }
        public string MinHeight
        {
            get => _minHeight;
            set
            {
                _minHeight = value;
                OnPropertyChanged("MinHeight");
            }
        }

        private string _maxHeight;
        public string MaxHeight
        {
            get => _maxHeight;
            set
            {
                _maxHeight = value;
                OnPropertyChanged("MaxHeight");
            }
        }

        private string _sexualOrientation;
        public string SexualOrientation
        {
            get => _eyesColor;
            set
            {
                _eyesColor = value;
                OnPropertyChanged("SexualOrientation");
            }
        }

        private string _eyesColor;
        public string EyesColor { 
            get=> _eyesColor;
            set
            {
                _eyesColor = value;
                OnPropertyChanged("EyesColor");
            }
        }

        private string _hairColor;
        public string HairColor
        {
            get => _hairColor;
            set
            {
                _hairColor = value;
                OnPropertyChanged("HairColor");
            }
        }
    }
}
