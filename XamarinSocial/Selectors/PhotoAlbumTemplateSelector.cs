using Xamarin.Forms;
using XamarinSocial.Models.Photo;

namespace XamarinSocial.Selectors
{
    public class PhotoAlbumTemplateSelector : DataTemplateSelector
    {
        #region Templates
        public DataTemplate AddItem { get; set; }
        public DataTemplate ImageItem { get; set; }

        #endregion

        #region Overrides

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((AlbumPhotoModel)item).IsPhotoItem ? ImageItem : AddItem;
        }

        #endregion
    }
}
