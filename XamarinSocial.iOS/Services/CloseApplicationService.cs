using System.Threading;
using XamarinSocial.Services.Interfaces;

namespace XamarinSocial.iOS.Services
{
    public class CloseApplicationService : ICloseApplicationService
    {
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort(); 
        }
    }
}