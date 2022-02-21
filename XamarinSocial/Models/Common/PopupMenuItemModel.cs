using System;
using Xamarin.Forms;

namespace XamarinSocial.Models.Common
{
    public class PopupMenuItemModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public string ImageSource { get; set; }
        public object ActionType { get; set; }

        #region Constructors

        public PopupMenuItemModel(string text)
        {
            Id = Guid.NewGuid().ToString();
            Text = text;
            ImageSource = string.Empty;
            Color = (Color)(Application.Current.Resources["fcolor_b6"]);
            
        }
        public PopupMenuItemModel(string text, Color color, string image = "", object actionType = null)
        {
            Id = Guid.NewGuid().ToString();
            Text = text;
            Color = color;
            ImageSource = image;
            ActionType = actionType;
        }

        #endregion
    }
}
