using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XamarinSocial.Enums;
using XamarinSocial.Models;
using XamarinSocial.Models.Api.Requests;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class FeedService : BaseApiService, IFeedService
    {
        public async Task<ResponseModel<GetProfilePhotosModel>> GetProfilePhotos(int take, int offset)
        {
            Thread.Sleep(200);
            //Hardcode
            var response = new ResponseModel<GetProfilePhotosModel>()
            {
                IsSucceed = true,
                StatusCode = 200
            };

            response.Data = new GetProfilePhotosModel()
            {

                Photos = new List<PhotoModel>()
            };

            var index = offset;
            for (int i = index; i < take + index; i++)
            {
                if (index >= 10)
                {
                    return response;
                }
                var profilePhoto = new PhotoModel();
                switch (i)
                {
                    case 0:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 1:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 2:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 3:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 4:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;
                    case 5:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    //data 1-5
                    case 6:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 7:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 8:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 9:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 10:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 11:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 12:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    case 13:
                        {
                            profilePhoto.Source = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg";
                        }
                        break;

                    default:
                        {

                        }
                        break;
                }

                (response.Data.Photos as List<PhotoModel>).Add(profilePhoto);
            }

            return response;
        }
        public async Task<ResponseModel<GetProfileFeedListModel>> GetProfileFeedList(int take, int offset)
        {
            Thread.Sleep(200);
            //Hardcode
            var response = new ResponseModel<GetProfileFeedListModel>()
            {
                IsSucceed = true,
                StatusCode = 200
            };

            response.Data = new GetProfileFeedListModel()
            {

                ProfileFeeds = new List<ProfileFeed>()
            };

            var index = offset;
            for (int i = index; i < take + index; i++)
            {
                if (index >= 10)
                {
                    return response;
                }
                var profileFeed = new ProfileFeed()
                {
                    DateCreatedUtc = (i % 2 == 0) ? DateTime.Now.ToUniversalTime() : DateTime.Now.AddDays(-2).ToUniversalTime(),
                    Description = (i % 2 == 0) ? "Dating outside the box – Online speed dating party! Dating outside the box – Online speed dating party!" : null,
                    PinnedLocation = (i % 2 == 0) ? null : new Models.PinnedLocationModel() { FirstLevelArea = "Ukraine", SecondLevelArea = "Kharkiv" }
                };

                switch (i)
                {
                    case 0:
                        {
                            profileFeed.LikeCount = 30000;
                            profileFeed.CommentCount = 20;
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Video,
                                Source = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4",
                                PreviewSource = "https://lh3.googleusercontent.com/MtiCoKx0-vGhJscK-lNorESy40stxTzcgz1jxZFtjhXy87Lt2jAAFVRflQjkTmN_sotJ"
                            };
                        }
                        break;

                    case 1:
                        {
                            profileFeed.LikeCount = 300;
                            profileFeed.CommentCount = 20;
                            profileFeed.Description = "Dating outside the box – Online speed dating party! Dating outside the box – Online speed dating party!";
                        }
                        break;

                    case 2:
                        {
                            profileFeed.LikeCount = 301400;
                            profileFeed.CommentCount = 20;
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Video,
                                Source = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                                PreviewSource = "data:image/jpeg;base64,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"
                            };
                        }
                        break;

                    case 3:
                        {
                            profileFeed.LikeCount = 300;
                            profileFeed.CommentCount = 200435230;
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Video,
                                Source = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4",
                                PreviewSource = "https://lh3.googleusercontent.com/MtiCoKx0-vGhJscK-lNorESy40stxTzcgz1jxZFtjhXy87Lt2jAAFVRflQjkTmN_sotJ"
                            };
                        }
                        break;

                    case 4:
                        {
                            profileFeed.LikeCount = 10506050;
                            profileFeed.CommentCount = 265000;
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Image,
                                Source = "https://www.thespruce.com/thmb/wHTqgbcdVb1dyayLjmVdtSN5cCM=/1000x562/smart/filters:no_upscale()/tiny-house-mini-motives-56a887b95f9b58b7d0f3184e.jpg"
                            };
                        }
                        break;
                    case 5:
                        {
                            profileFeed.LikeCount = 10;
                            profileFeed.CommentCount = 20;
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Video,
                                Source = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/Sintel.mp4",
                                PreviewSource = "https://miro.medium.com/max/2800/1*jKqyEbQrhXBjy_4SrUjplw.jpeg"
                            };
                        }
                        break;

                    //data 1-5
                    case 6:
                        {
                            profileFeed.LikeCount = 100;
                            profileFeed.CommentCount = 20000;
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Video,
                                Source = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4",
                                PreviewSource = "https://lh3.googleusercontent.com/MtiCoKx0-vGhJscK-lNorESy40stxTzcgz1jxZFtjhXy87Lt2jAAFVRflQjkTmN_sotJ"
                            };
                        }
                        break;

                    case 7:
                        {
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Image,
                                Source = "https://cdn.aarp.net/content/dam/aarp/home-and-family/your-home/2018/06/1140-house-inheriting.imgcache.rev68c065601779c5d76b913cf9ec3a977e.jpg"
                            };
                        }
                        break;

                    case 8:
                        {
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Video,
                                Source = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                                PreviewSource = "https://expertdr.files.wordpress.com/2019/02/444-1.jpg"
                            };
                        }
                        break;

                    case 9:
                        {
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Image,
                                Source = "https://cdn.aarp.net/content/dam/aarp/home-and-family/your-home/2018/06/1140-house-inheriting.imgcache.rev68c065601779c5d76b913cf9ec3a977e.jpg"
                            };
                        }
                        break;
                    case 10:
                        {
                            profileFeed.Content = new PostContent()
                            {
                                ContentType = ContentType.Image,
                                Source = "https://cdn.aarp.net/content/dam/aarp/home-and-family/your-home/2018/06/1140-house-inheriting.imgcache.rev68c065601779c5d76b913cf9ec3a977e.jpg"
                            };
                        }
                        break;


                    default:
                        {

                        }
                        break;
                }

                (response.Data.ProfileFeeds as List<ProfileFeed>).Add(profileFeed);
            }

            return response;
        }

        //todo: remove hardcode data
        public async Task<ResponseModel<GetFeedListModel>> GetFeedList(int take, int offset)
        {

            //Thread.Sleep(500);

            //Hardcode
            var response = new ResponseModel<GetFeedListModel>()
            {
                IsSucceed = true,
                StatusCode = 200
            };

            response.Data = new GetFeedListModel()
            {

                Posts = new List<Post>()
            };

            var index = offset;
            for (int i = 0; i < take; i++)
            {
                var post = new Post()
                {
                    DateCreatedUtc = (i % 2 == 0) ? DateTime.Now.ToUniversalTime() : DateTime.Now.AddDays(-2).ToUniversalTime(),
                    IsOwnPost = (i % 2 == 0),
                    CurrentUserMark = (i % 2 == 0) ? UserMark.Liked : UserMark.WithoutMark,
                    OwnerProfile = (i % 2 == 0) ? new BaseProfileInfo() { FirstName = "Linda", LastName = "Taylor", ProfileImageSource = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg" } : new BaseProfileInfo() { FirstName = "Jeniffer", LastName = "Smith", ProfileImageSource = "https://www.ajyall.com/sites/default/files/default_images/team-default.png" },
                    Description = (i % 2 == 0) ? "Dating outside the box – Online speed dating party! Dating outside the box – Online speed dating party!" : null,
                    PinnedLocation = (i % 2 == 0) ? null : new Models.PinnedLocationModel() { FirstLevelArea = "Ukraine", SecondLevelArea = "Kharkiv" }
                };

                switch (i)
                {
                    case 0:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Video, Source = "https://storage.googleapis.com/exchangemedia/big_buck_bunny.mp4", PreviewSource = "https://lh3.googleusercontent.com/MtiCoKx0-vGhJscK-lNorESy40stxTzcgz1jxZFtjhXy87Lt2jAAFVRflQjkTmN_sotJ" } };
                        }
                        break;

                    case 1:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Image, Source = "https://cdn.aarp.net/content/dam/aarp/home-and-family/your-home/2018/06/1140-house-inheriting.imgcache.rev68c065601779c5d76b913cf9ec3a977e.jpg" } };
                        }
                        break;

                    case 2:
                        {
                            post.Content = new PostContent[] { };
                        }
                        break;

                    case 3:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Video, Source = "https://storage.googleapis.com/exchangemedia/big_buck_bunny.mp4", PreviewSource = "https://lh3.googleusercontent.com/MtiCoKx0-vGhJscK-lNorESy40stxTzcgz1jxZFtjhXy87Lt2jAAFVRflQjkTmN_sotJ" },
                                                               new PostContent() { ContentType = ContentType.Image, Source = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?ixlib=rb-1.2.1&w=1000&q=80" }};
                        }
                        break;

                    case 4:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Image, Source = "https://www.thespruce.com/thmb/wHTqgbcdVb1dyayLjmVdtSN5cCM=/1000x562/smart/filters:no_upscale()/tiny-house-mini-motives-56a887b95f9b58b7d0f3184e.jpg" },
                                                               new PostContent() { ContentType = ContentType.Image, Source = "https://www.huf-haus.com/fileadmin/_processed_/8/5/csm_Show_House_Riverview_HUF_HAUS_549af80fe1.jpg" },
                                                               new PostContent() { ContentType = ContentType.Image, Source = "https://cdn-p.cian.site/images/4/750/786/klubnyy-dom-koncept-house-moskva-jk-687057433-6.jpg" },
                                                               new PostContent() { ContentType = ContentType.Image, Source = "https://images.adsttc.com/media/images/5efe/1f7f/b357/6540/5400/01d7/newsletter/archdaily-houses-104.jpg?1593712501" } };
                        }
                        break;
                    case 5:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Video, Source = "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/Sintel.mp4", PreviewSource = "https://miro.medium.com/max/2800/1*jKqyEbQrhXBjy_4SrUjplw.jpeg" } };
                        }
                        break;

                    //data 1-5
                    case 6:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Video, Source = "https://storage.googleapis.com/exchangemedia/big_buck_bunny.mp4", PreviewSource = "https://lh3.googleusercontent.com/MtiCoKx0-vGhJscK-lNorESy40stxTzcgz1jxZFtjhXy87Lt2jAAFVRflQjkTmN_sotJ" } };
                        }
                        break;

                    case 7:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Image, Source = "https://cdn.aarp.net/content/dam/aarp/home-and-family/your-home/2018/06/1140-house-inheriting.imgcache.rev68c065601779c5d76b913cf9ec3a977e.jpg" } };
                        }
                        break;

                    case 8:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Video, Source = "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4", PreviewSource = "https://expertdr.files.wordpress.com/2019/02/444-1.jpg" } };
                        }
                        break;

                    case 9:
                        {
                            post.Content = new PostContent[] { new PostContent() { ContentType = ContentType.Image, Source = "https://cdn.aarp.net/content/dam/aarp/home-and-family/your-home/2018/06/1140-house-inheriting.imgcache.rev68c065601779c5d76b913cf9ec3a977e.jpg" },
                                                               new PostContent() { ContentType = ContentType.Image, Source = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?ixlib=rb-1.2.1&w=1000&q=80" }};
                        }
                        break;

                    default:
                        {

                        }
                        break;
                }

                (response.Data.Posts as List<Post>).Add(post);
            }


            return response;
        }

        //todo: remove hardcode
        public async Task<ResponseModel> CreatePost(CreatePostModel postModel)
        {
            Thread.Sleep(500);

            var result = new ResponseModel();
            result.IsSucceed = true;

            return result;
        }
    }
}
