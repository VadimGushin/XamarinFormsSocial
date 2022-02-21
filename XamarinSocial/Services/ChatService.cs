using System;
using System.Threading.Tasks;
using XamarinSocial.Extensions;
using XamarinSocial.Models.Api.Responses;
using XamarinSocial.Models.Chat;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.Services
{
    public class ChatService : BaseApiService, IChatService
    {
        #region Variables

        private bool _isOnline = false;

        #endregion

        #region Hardcode Variables

        string[] _messages = { "Cras mauris nec id eget. Ligula viverra est faucibus volutpat massa aferandosa merane lorem",
                               "Cras mauris nec id ege",
                               "Test string test string Test string test string Test",
                               "Test string test string Test string test string Test Test string test string Test Test string test string Test Test string test string Test",
                               "sdfkjh adfuhv iaufdh gaoiufgyoqr89f fvsab;lsjd bsdghb sdj bh;sdljfb"};
        Random ran = new Random();
        int _counter = 0;
        DateTime[] dateTimes = { new DateTime(2018, 8, 16), new DateTime(2019, 8, 16), new DateTime(2020, 1, 19),
                                 new DateTime(2020, 10, 25), new DateTime(2020, 11, 7), new DateTime(2020, 11, 16), new DateTime(2020, 11, 16),
                                 new DateTime(2020, 11, 17), new DateTime(2020, 11, 21, 22, 35, 40), new DateTime(2020, 11, 30, 6, 28, 40),
                                 new DateTime(2020, 12, 3, 12, 10, 20), new DateTime(2020, 12, 6, 14, 19, 40), new DateTime(2020, 12, 7, 9, 5, 40)};

        #endregion

        public async Task<ResponseModel<ChatModel>> GetByIdAsync(int skip, int take, string id = "")
        {
            var result = new ResponseModel<ChatModel>();
            result.Data = new ChatModel();
            if (string.IsNullOrWhiteSpace(id))
            {
                //hardcode
                for (int i = 0; i < take; i++)
                {
                    result.Data.Items.Add(InitChatModel());
                }
            }
            //todo: ExecuteGetAsync() for API
            return result;
        }

        public async Task<ResponseModel<ChatConversationModel>> GetConservationByIdAsync(int skip, int take, string id = "")
        {
            var result = new ResponseModel<ChatConversationModel>();
            result.Data = new ChatConversationModel();

            //for testing
            if (skip == 0)
            {
                _counter = 0;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                //hardcode
                for (int i = 0; i < take; i++)
                {
                    result.Data.Items.Add(InitChatConversationItemModel());
                }
            }
            //todo: ExecuteGetAsync() for API
            return result;
        }

        #region Private Methods

        private ChatItemModel InitChatModel()
        {
            //hardcode
            _isOnline = !_isOnline;

            var dateTime = dateTimes[ran.Next(0, dateTimes.Length)];
            return new ChatItemModel()
            {
                CreationDate = dateTime,
                UserName = "UserName",
                Message = "Aliquam commodo felis accumsan, test text added",
                Count = _isOnline ? (ran.Next(0, 100)).ToString() : string.Empty,
                IsOnline = _isOnline,
                ImageSource = "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg",
                OnlineStatusText = _isOnline ? "online" : $"last seen {dateTime.GetTimeString(true)}"
            };
        }

        private ChatConversationItemModel InitChatConversationItemModel()
        {
            //hardcode
            var date = dateTimes[ran.Next(dateTimes.Length - 3, dateTimes.Length)];
            var item = new ChatConversationItemModel() 
            {
                IsOwn = _isOnline,
                Message = $"{_counter} { _messages[ran.Next(0, _messages.Length)]}",
                ImageSource = _counter % 9 == 0 ? "https://easyspeak.ru/assets/images/blog/difference/people/pexels-photo-214574.jpeg" : string.Empty,
                CreationDate = date,
                HasTopDate = false,
                TopDateString = string.Empty,
                ShortDateString = date.ToString("HH:mm")
            };
            _isOnline = !_isOnline;
            _counter++;
            return item;
        }

        #endregion
    }
}
