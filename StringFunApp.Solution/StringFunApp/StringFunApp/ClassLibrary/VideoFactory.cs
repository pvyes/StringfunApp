﻿using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StringFunApp.ClassLibrary
{
    public class VideoFactory
    {
        private XmlReader reader;

        public async Task<IEnumerable<VideoInfo>> GetVideos(List<string> videoIds)
        {
            reader = XmlImporter.getReader("https://www.staproeselare.be/stringfun/xml/stringfunvideos.xml");
            List<VideoInfo> InMemoryVideos = new List<VideoInfo>();
            while (await reader.ReadAsync())
            {
                reader.ReadToFollowing("video");
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "video")
                {
                    foreach (var id in videoIds)
                    {
                        if (reader.GetAttribute("id") == id)
                        {
                            string uniekenaam;
                            string displayname;
                            string videosource;
                            var inner = reader.ReadSubtree();
                            inner.ReadToFollowing("videoname");
                            uniekenaam = inner.ReadString();
                            inner.ReadToFollowing("title");
                            displayname = inner.ReadString();
                            inner.ReadToFollowing("source");
                            videosource = inner.ReadString();
                            VideoInfo video = new VideoInfo { UniekeNaam = uniekenaam, DisplayName = displayname, VideoSource = VideoSource.FromUri(videosource) };
                            InMemoryVideos.Add(video);
                        }
                    }
                }
            }
            return InMemoryVideos;
        }
    }
}
