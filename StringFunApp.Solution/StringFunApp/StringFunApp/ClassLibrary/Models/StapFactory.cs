﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using StringFunApp.ClassLibrary.Converters;
using StringFunApp.ClassLibrary.Readers;

namespace StringFunApp.ClassLibrary.Models
{
    class StapFactory
    {
        public const string STEPS_URI = "https://www.staproeselare.be/stringfun/xml/stringfunsteps.xml";
        public const string STEPS_URI_UNVALIDATED = "https://www.staproeselare.be/stringfun/xml/stringfunstepsUnvalidated.xml";

        private static readonly Lazy<StapFactory> _instance = new Lazy<StapFactory>(() => new StapFactory());

        public static StapFactory Instance
        {
            get { return _instance.Value; }
        }

        public static Stap CreateStap(int stapNumber, Instrument instrument, Boek boek)
        {
            StapReader reader = new StapReader();
            List<string> videoIds = reader.ReadVideoIds(STEPS_URI_UNVALIDATED, stapNumber.ToString(), instrument.Naam);
            ObservableCollection<VideoInfo> videoInfos = new ObservableCollection<VideoInfo>();
            if (videoIds != null)
            {
                foreach (VideoInfo vi in GetVideoInfos(videoIds))
                {
                    videoInfos.Add(vi);
                }
           }
            Stap inMemoryStap = new Stap { Instrument = instrument, Nummer = stapNumber, Boek = boek, VideoLijst = videoInfos };
            return inMemoryStap;
        }

        private static List<VideoInfo> GetVideoInfos(List<string> videoIds)
        {
            List<VideoInfo> videoInfos = new List<VideoInfo>();
            for (int i = 0; i < videoIds.Count; i++)
            {
                string vid = videoIds[i];
                VideoInfo vInfo = VideoFactory.CreateVideoInfo(vid);
                videoInfos.Add(VideoFactory.CreateVideoInfo(vid));
            }
            return videoInfos;
        }
    }
}
