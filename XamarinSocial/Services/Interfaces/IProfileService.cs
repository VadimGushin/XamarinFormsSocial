using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSocial.Models;

namespace XamarinSocial.Services.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfile> GetByIdAsync(string id = "");
        Task SaveAsync(UserProfile profile);
    }
}
