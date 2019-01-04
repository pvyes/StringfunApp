using System;
using System.Collections.Generic;
using System.Text;
using FormsVideoLibrary;
using StringFunApp.ClassLibrary.Converters;
using StringFunApp.ClassLibrary.Readers;

namespace StringFunApp.ClassLibrary.Models
{
    class VideoFactory
    {
        public const string VIDEOS_URI = "https://www.staproeselare.be/stringfun/xml/stringfunvideos.xml";
        public const string VIDEOS_URI_UNVALIDATED = "https://www.staproeselare.be/stringfun/xml/stringfunvideosUnvalidated.xml";

        private static readonly Lazy<VideoFactory> _instance = new Lazy<VideoFactory>(() => new VideoFactory());

        public static VideoFactory Instance
        {
            get { return _instance.Value; }
        }

        public static VideoInfo CreateVideoInfo(string videoId)
        {
            VideoReader reader = new VideoReader();
            List<string> videoData = Cast<List<string>>.perform(reader.Execute(VIDEOS_URI_UNVALIDATED, videoId).Get());
            string videoName = videoData[0];
            string videoTitle = videoData[1];
            string videoSource = videoData[2];
            VideoInfo videoInfo = new VideoInfo { Id = videoId, Title = videoName, DisplayName = videoTitle, VideoSource = VideoSource.FromUri(videoSource) };
            return videoInfo;
        }
    }
}
