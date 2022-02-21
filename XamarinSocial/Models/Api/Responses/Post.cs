using System;
using System.Collections.Generic;
using System.ComponentModel;
using XamarinSocial.Enums;

namespace XamarinSocial.Models.Api.Responses
{
    public class Post : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public DateTime DateCreatedUtc { get; set; }
        public string Description { get; set; }
        public IEnumerable<PostContent> Content { get; set; }
        public UserMark CurrentUserMark { get; set; }
        public BaseProfileInfo OwnerProfile { get; set; }
        public bool IsOwnPost { get; set; }
        public PinnedLocationModel PinnedLocation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertiesChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUserMark)));
        }
    }
}
