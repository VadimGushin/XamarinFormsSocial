using Xamarin.Forms;

namespace XamarinSocial.Models.Photo
{
    public class ImagesSourceModel
    {
        #region Properties

        public ImageSource ImageSource { get; set; }
        public string Base64Source { get; set; }
        public string ImagePath { get; set; }
        public bool IsSucceded { get; set; }

        #endregion

        #region Constructors

        public ImagesSourceModel()
        {
            IsSucceded = false;
        }

        public ImagesSourceModel(ImageSource imageSource, string base64Source, string imagePath, bool isSucceded = true)
        {
            ImageSource = imageSource;
            Base64Source = base64Source;
            ImagePath = imagePath;
            IsSucceded = isSucceded;
        }

        #endregion
    }
}
