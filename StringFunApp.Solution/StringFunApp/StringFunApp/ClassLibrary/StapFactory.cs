using StringFunApp.ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml;

namespace StringFunApp.ClassLibrary
{
    public class StapFactory
    {
        private XmlReader reader;
        private VideoFactory VideoFactory;

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


        public async Task<Stap> CreateStap(string stap, string instrument)
        {
            reader = XmlImporter.getReader("https://www.staproeselare.be/stringfun/xml/stringfunsteps.xml");
            List<string> videoIds = new List<string>();
            while (await reader.ReadAsync() && videoIds.Count == 0)
            {
                reader.ReadToFollowing("instrument");
                if (reader.GetAttribute("name").Contains(instrument))
                {
                    var inner = reader.ReadSubtree();
                    while (await inner.ReadAsync())
                    {
                        if (inner.MoveToContent() == XmlNodeType.Element && inner.Name == "step" && inner.GetAttribute("number") == stap.Replace("Stap ", ""))
                        {
                            inner.ReadToFollowing("videoid");
                            do
                            {
                                string videoId = reader.ReadString();
                                videoIds.Add(videoId);
                            } while (inner.ReadToNextSibling("videoid"));
                        }
                    }
                }
            }
            Videos = new List<VideoInfo>(await VideoFactory.GetVideos(videoIds));
            Stap InMemoryStap;
            ObservableCollection<VideoInfo> StapVideos = new ObservableCollection<VideoInfo>();
            foreach (VideoInfo video in Videos)
            {
                StapVideos.Add(video);
            }
            InMemoryStap = new Stap { Nummer = Convert.ToInt32(stap.Replace("Stap ", "")), VideoLijst = StapVideos };
            return InMemoryStap;
        }
    }
}
