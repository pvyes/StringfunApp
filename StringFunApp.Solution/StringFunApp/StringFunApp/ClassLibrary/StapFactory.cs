using StringFunApp.ClassLibrary.Models;
using System;
using System.Collections.Generic;

namespace StringFunApp.ClassLibrary
{
    public class StapFactory
    {
        public StapFactory()
        {
            VideoFactory = new VideoFactory();
            Videos = new List<VideoInfo>(VideoFactory.GetAll());
        }

        private List<VideoInfo> videos;
        public List<VideoInfo> Videos
        {
            get { return videos; }
            set { videos = value; }
        }

        private VideoFactory VideoFactory;

        public Stap CreateStap(string stap, int boeknummer)
        {
            Stap InMemoryStap;
            List<VideoInfo> StapVideos = new List<VideoInfo>();
            foreach (VideoInfo video in Videos)
            {
                if (video.UniekeNaam.Contains(stap))
                {
                    StapVideos.Add(video);
                }
            }
            InMemoryStap = new Stap { Nummer = Convert.ToInt32(stap.Replace("Stap ", "")), VideoLijst = StapVideos };
            return InMemoryStap;
        }
    }
}
