using StringFunApp.ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StringFunApp.ClassLibrary
{
    public class StapFactory
    {
        public StapFactory()
        {
            VideoFactory = new VideoFactory();
        }

        private List<VideoInfo> videos;
        public List<VideoInfo> Videos
        {
            get { return videos; }
            set { videos = value; }
        }

        private VideoFactory VideoFactory;

        public async Task<List<VideoInfo>> GetVideos()
        {
            Videos = new List<VideoInfo>(await this.VideoFactory.GetAll());
            return Videos;
        }

        public async Task<Stap> CreateStap(string stap, string instrument)
        {
            Stap InMemoryStap;
            ObservableCollection<VideoInfo> StapVideos = new ObservableCollection<VideoInfo>();
            await GetVideos();
            foreach (VideoInfo video in Videos)
            {
                if (video.UniekeNaam.Contains(stap.Replace(" ", "")) && video.UniekeNaam.Contains(instrument))
                {
                    StapVideos.Add(video);
                }
            }
            InMemoryStap = new Stap { Nummer = Convert.ToInt32(stap.Replace("Stap ", "")), VideoLijst = StapVideos };
            return InMemoryStap;
        }
    }
}
