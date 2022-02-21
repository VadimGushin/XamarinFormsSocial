using System.Collections.Generic;

namespace XamarinSocial.Models.Api.Responses
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            Errors = new List<string>();
        }

        public bool IsSucceed { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }

    public class ResponseModel<TResult> : ResponseModel
    {
        public TResult Data { get; set; }
    }
}
