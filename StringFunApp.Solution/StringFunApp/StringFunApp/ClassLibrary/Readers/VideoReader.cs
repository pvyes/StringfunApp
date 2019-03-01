﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Android.OS;
using FormsVideoLibrary;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class VideoReader
    {

        public List<string> ReadVideo(string uri, string videoId)
        {
            Task<List<string>> ReadVideoDataTask = 
                Task.Run(
                    () => ReadVideoData(uri, videoId)
                );
            ReadVideoDataTask.Wait();

            return ReadVideoDataTask.Result;
        }

        private List<string> ReadVideoData(string uri, string videoId)
        {
            XmlReader reader = XmlImporter.GetReader(uri, false);
            List<String> videoData = new List<String>();
            try
            {
                while (videoData.Count == 0 && reader.ReadToFollowing("video")) {
                    string id = reader.GetAttribute("id");
                    if (id.Equals(videoId))
                    {
                        XmlReader inner = reader.ReadSubtree();
                        videoData.AddRange(ReadInnerData(inner, videoId));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\tReader error: " + e.Message);
            }
            return videoData;
        }

        private List<String> ReadInnerData(XmlReader inner, string id)
        {
            List<String> innerData = new List<string>(); ;
            inner.ReadToFollowing("videoname");
            innerData.Add(inner.ReadString());
            inner.ReadToFollowing("title");
            innerData.Add(inner.ReadString());
            inner.ReadToFollowing("source");
            innerData.Add(inner.ReadString());
            return innerData;
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
