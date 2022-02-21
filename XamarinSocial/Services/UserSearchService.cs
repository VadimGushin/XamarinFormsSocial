using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XamarinSocial.Enums;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class UserSearchService : BaseApiService, IUserSearchService
    {
        //todo: remove hardcode data
        public async Task<ResponseModel<GetSearchUserModel>> GetSearchUserList(int take, int offset)
        {
            await Task.Delay(600);

            //Hardcode
            var response = new ResponseModel<GetSearchUserModel>()
            {
                IsSucceed = true,
                StatusCode = 200
            };

            response.Data = new GetSearchUserModel()
            {
                SearchUsers = new List<SearchUserModel>()
            };

            var index = offset;
            for (int i = index; i < take + index; i++)
            {
                if (index >= 10)
                {
                    return response;
                }
                var searchedUser = new SearchUserModel()
                {              
                };

                switch (i)
                {
                    case 0:
                        {
                            searchedUser.FirstName = "Maria";
                            searchedUser.Age = 19;
                            searchedUser.City = "Los Angeles";
                            searchedUser.IsLiked = true;
                            searchedUser.IsPopular = true;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 2;
                            searchedUser.MatchPercent = 97;
                            //searchedUser.UserGender = Gender.Female;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/maria-ozawa-grey-shirt.jpg";   
                        }
                        break;

                    case 1:
                        {
                            searchedUser.FirstName = "Eva";
                            searchedUser.Age = 28;
                            searchedUser.City = "Los Angeles";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = false;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 3;
                            searchedUser.MatchPercent = 80;
                            //searchedUser.UserGender = Gender.Female;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/1456623-1171470.jpg";
                        }
                        break;
                    case 2:
                        {
                            searchedUser.FirstName = "Quentin";
                            searchedUser.Age = 49;
                            searchedUser.City = "New York";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = true;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 5;
                            searchedUser.MatchPercent = 73;
                            //searchedUser.UserGender = Gender.Male;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/0_35.jpg";
                        }
                        break;
                    case 3:
                        {
                            searchedUser.FirstName = "Alison";
                            searchedUser.Age = 29;
                            searchedUser.City = "Toronto";
                            searchedUser.IsLiked = true;
                            searchedUser.IsPopular = false;
                            searchedUser.IsNew = false;
                            searchedUser.LastVisitAgoInHours = 8;
                            searchedUser.MatchPercent = 67;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/1458934-1153131.jpg";
                        }
                        break;
                    case 4:
                        {
                            searchedUser.FirstName = "Jessica";
                            searchedUser.Age = 39;
                            searchedUser.City = "California";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = true;
                            searchedUser.IsNew = false;
                            searchedUser.LastVisitAgoInHours = 3;
                            searchedUser.MatchPercent = 67;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/photo-1_7.jpg";
                        }
                        break; 
                    
                    case 5:
                        {
                            searchedUser.FirstName = "John";
                            searchedUser.Age = 50;
                            searchedUser.City = "New Jersey";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = false;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 3;
                            searchedUser.MatchPercent = 67;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/1459151-1200486.jpg";
                        }
                        break;
                    case 6:
                        {
                            searchedUser.FirstName = "Maria";
                            searchedUser.Age = 19;
                            searchedUser.City = "Los Angeles";
                            searchedUser.IsLiked = true;
                            searchedUser.IsPopular = true;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 2;
                            searchedUser.MatchPercent = 97;
                            //searchedUser.UserGender = Gender.Female;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/maria-ozawa-grey-shirt.jpg";
                        }
                        break;

                    case 7:
                        {
                            searchedUser.FirstName = "Eva";
                            searchedUser.Age = 28;
                            searchedUser.City = "Los Angeles";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = false;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 3;
                            searchedUser.MatchPercent = 80;
                            //searchedUser.UserGender = Gender.Female;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/1456623-1171470.jpg";
                        }
                        break;
                    case 8:
                        {
                            searchedUser.FirstName = "Quentin";
                            searchedUser.Age = 49;
                            searchedUser.City = "New York";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = true;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 5;
                            searchedUser.MatchPercent = 73;
                            //searchedUser.UserGender = Gender.Male;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/0_35.jpg";
                        }
                        break;

                    case 9:
                        {
                            searchedUser.FirstName = "Eva";
                            searchedUser.Age = 28;
                            searchedUser.City = "Los Angeles";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = false;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 3;
                            searchedUser.MatchPercent = 80;
                            //searchedUser.UserGender = Gender.Female;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/1456623-1171470.jpg";
                        }
                        break;
                    case 10:
                        {
                            searchedUser.FirstName = "Quentin";
                            searchedUser.Age = 49;
                            searchedUser.City = "New York";
                            searchedUser.IsLiked = false;
                            searchedUser.IsPopular = true;
                            searchedUser.IsNew = true;
                            searchedUser.LastVisitAgoInHours = 5;
                            searchedUser.MatchPercent = 73;
                            //searchedUser.UserGender = Gender.Male;
                            searchedUser.ImgURL = "https://www.film.ru/sites/default/files/people/0_35.jpg";
                        }
                        break;

                    default:
                        {
                        }
                        break;
                }

                (response.Data.SearchUsers as List<SearchUserModel>).Add(searchedUser);
            }

            return response;
        }
    }
}
