using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FormsVideoLibrary;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class VideoReader
    {
        public List<VideoInfo> Read(string uri)
        {
            XmlReader reader = XmlImporter.getReader(uri, false);
            List<VideoInfo> videos = new List<VideoInfo>();
            reader.ReadToFollowing("video");
            do
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
                videos.Add(video);
            } while (reader.ReadToFollowing("video"));
            return videos;
        }

        public List<VideoInfo> ReadListOfObjects(string uri, List<string> videoIds)
        {
            XmlReader reader = XmlImporter.getReader(uri, false);
            List<VideoInfo> videos = new List<VideoInfo>();
            reader.ReadToFollowing("video");
            do
            {
                string uniekenaam;
                string displayname;
                string videosource;
                var inner = reader.ReadSubtree();
                inner.ReadToFollowing("videoname");
                uniekenaam = inner.ReadString();
                if (CompareId(uniekenaam, videoIds))
                {
                    inner.ReadToFollowing("title");
                    displayname = inner.ReadString();
                    inner.ReadToFollowing("source");
                    videosource = inner.ReadString();
                    VideoInfo video = new VideoInfo { UniekeNaam = uniekenaam, DisplayName = displayname, VideoSource = VideoSource.FromUri(videosource) };
                    videos.Add(video);
                }
            } while (reader.ReadToFollowing("video"));
            return videos;
        }

        private bool CompareId(string uniekenaam, List<string> videoIds)
        {
            if (videoIds.Contains(uniekenaam))
            {
                return true;
            }
            return false;
        }
    } 
}
