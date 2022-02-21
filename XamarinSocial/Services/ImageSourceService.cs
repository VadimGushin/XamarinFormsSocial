using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSocial.Constants;
using XamarinSocial.Models.Photo;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class ImageSourceService : IImageSourceService
    {
        public async Task<ImagesSourceModel> GetImageFromCameraAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable && !CrossMedia.Current.IsTakePhotoSupported)
            {
                return new ImagesSourceModel();
            }

            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                DefaultCamera = CameraDevice.Front,
                SaveToAlbum = true,
                Directory = Constant.MediaConfig.MEDIA_DIRECTORY,
                Name = $"{DateTime.Now.ToString(Constant.MediaConfig.IMAGE_SAVING_FORMAT)}{Constant.MediaConfig.IMAGE_EXTENSION}"
            });

            return GetImageSourceModel(file);
        }

        public async Task<ImagesSourceModel> GetImageFromLibraryAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return new ImagesSourceModel();
            }

            MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                CompressionQuality = 50
            });
            return GetImageSourceModel(file);
        }

        private ImagesSourceModel GetImageSourceModel(MediaFile file)
        {
            var resultModel = new ImagesSourceModel();
            if (!(file is null))
            {
                resultModel.ImageSource = ImageSource.FromFile(file.Path);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);

                    resultModel.Base64Source = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return resultModel;
        }
    }
}
