using System;
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

        public static async System.Threading.Tasks.Task<Stap> CreateStapAsync(int stapNumber, Instrument instrument)
        {
//          StapReader reader = new StapReader();
//          List<string> videoIds = new List<string>();
//          videoIds = await reader.ReadVideoIdsAsync(STEPS_URI_UNVALIDATED, stapNumber.ToString(), instrument.Naam);

            AndroidStapReader reader = new AndroidStapReader();
            ObservableCollection<VideoInfo> videoIds = new ObservableCollection<VideoInfo>();
            videoIds = reader.Execute(STEPS_URI_UNVALIDATED, stapNumber.ToString(), instrument.Naam);

            for (int i = 0; i < videoIds.Count; i++)
            {
                string vid = videoIds[i];
                VideoInfo vInfo = VideoFactory.CreateVideoInfo(vid);
                videoInfos.Add(VideoFactory.CreateVideoInfo(vid));
            }
            Stap inMemoryStap = new Stap { Instrument = instrument, Nummer = stapNumber, VideoLijst = videoInfos };
            return inMemoryStap;
        }
    }
}
