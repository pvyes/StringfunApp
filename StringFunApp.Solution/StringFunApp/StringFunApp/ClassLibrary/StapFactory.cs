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
            while (await reader.ReadAsync())
            {
                reader.ReadToFollowing("instrument");
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "instrument")
                {
                    if (reader.GetAttribute("name").Contains(instrument))
                    {
                        var inner = reader.ReadSubtree();
                        while (await inner.ReadAsync())
                        {
                            inner.ReadToFollowing("step");
                            if (inner.GetAttribute("number").Contains(stap.Replace("Stap ", "")))
                            {
                                inner.ReadToFollowing("videoid");
                                while (inner.ReadToNextSibling("videoid"))
                                {
                                    string videoId = reader.ReadString();
                                    videoIds.Add(videoId);
                                }
                            }
                        }
                    }
                }
            }
            Videos = new List<VideoInfo>(await VideoFactory.GetVideos(instrument, stap));
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
