using System;

namespace XamarinSocial.Constants
{
    public static partial class Constant
    {
        public static class MediaConfig
        {
            public const string MEDIA_DIRECTORY = "XamarinSocial";
            public const string IMAGE_SAVING_FORMAT = "dd.MM.yyyy_hh.mm.ss";
            public const string VIDEO_SAVING_FORMAT = "dd.MM.yyyy_hh.mm.ss";
            public const string IMAGE_EXTENSION = ".jpg";
            public const string VIDEO_EXTENSION = ".mp4";
            public static readonly TimeSpan MAX_VIDEO_RECORD_DURATION = TimeSpan.FromMinutes(2);
            public const int MAX_ATTACHED_VIDEOS_COUNT = 1;
        }
    }
}
