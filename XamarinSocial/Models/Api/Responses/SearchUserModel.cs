using System.ComponentModel;
using XamarinSocial.Enums;
using XamarinSocial.ViewModels;

namespace XamarinSocial.Models.Api.Responses
{
    public class SearchUserModel : BaseCellModel, INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string ImgURL { get; set; }

        private bool _isLiked;
        public bool IsLiked
        {
            get
            {
                return _isLiked;
            }
            set
            {
                _isLiked = value;
                OnPropertyChanged("IsLiked");
            }
        }

        public string UserUrl { get; set; }
        public Gender UserGender { get; set; }
        public int LastVisitAgoInHours { get; set; }
        public bool IsNew { get; set; }
        public bool IsPopular { get; set; }
        public double MatchPercent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
