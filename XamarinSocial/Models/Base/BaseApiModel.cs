using System;

namespace XamarinSocial.Models.Base
{
    public class BaseApiModel
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsRemoved { get; set; }

        #region Constructors

        public BaseApiModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        #endregion
    }
}
