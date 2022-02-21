using System;
using XamarinSocial.Enums.VideoPlayer;

namespace XamarinSocial.VideoHelper.Interfaces
{ 
    public interface IVideoPlayerController
    {
        VideoStatus Status { set; get; }

        TimeSpan Duration { set; get; }
    }
}
