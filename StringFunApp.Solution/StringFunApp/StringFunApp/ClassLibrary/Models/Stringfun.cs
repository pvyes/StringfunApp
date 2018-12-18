using FormsVideoLibrary;
using StringFunApp.ClassLibrary.Readers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml;

namespace StringFunApp.ClassLibrary.Models
{
    public class Stringfun : INotifyPropertyChanged
    {
        public const string INIT_URI = "https://www.staproeselare.be/stringfun/xml/stringfuninit.xml";
        public const string VIDEOS_URI = "https://www.staproeselare.be/stringfun/xml/stringfunvideos.xml";
        public const string STEPS_URI = "https://www.staproeselare.be/stringfun/xml/stringfunsteps.xml";

        public event PropertyChangedEventHandler PropertyChanged;
        private XmlReader reader;

        public Stringfun()
        {
            ReadBooks(INIT_URI);
            ReadInstruments(INIT_URI);
        }

        private void ReadInstruments(string iNIT_URI)
        {
            InstrumentReader instrReader = new InstrumentReader();
            instruments = instrReader.ReadAllObjects(INIT_URI);
        }

        private void ReadBooks(string iNIT_URI)
        {
            BookReader bookReader = new BookReader();
            books = bookReader.ReadAllObjects(INIT_URI);
        }

        private List<VideoInfo> videos;
        public List<VideoInfo> Videos
        {
            get { return videos; }
            set { videos = value; }
        }

        private List<Instrument> instruments;
        public List<Instrument> Instruments
        {
            get { return instruments; }
            set { instruments = value; }
        }

        private List<Boek> books;
        public List<Boek> Books
        {
            get { return books; }
            set { books = value; }
        }

        public IEnumerable<Instrument> GetInstruments()
        {
            return instruments;
        }

        public IEnumerable<Boek> GetBooks()
        {
            return books;
        }

        public Boek GetBook(int booknumber)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Nummer == booknumber)
                {
                    return books[i];
                }
            }
            return null;
        }

        public Instrument GetInstrument(string instrumentname)
        {
            for (int i = 0; i < instruments.Count; i++)
            {
                if (instruments[i].Naam.Contains(instrumentname))
                {
                    return instruments[i];
                }
            }
            return null;
        }

        public List<VideoInfo> GetVideos(List<string> videoIds)
        {
            VideoReader videoreader = new VideoReader();
            List<VideoInfo> InMemoryVideos = videoreader.ReadListOfObjects(VIDEOS_URI, videoIds);
            return InMemoryVideos;
        }

        public ObservableCollection<string> GetStappen(int boeknummer)
        {
            ObservableCollection<string> StappenLijst = new ObservableCollection<string>();
            var boek = GetBook(boeknummer);
            for (int i = boek.FirstStep; i <= boek.LastStep; i++)
            {
                StappenLijst.Add("Stap " + i);
            }
            return StappenLijst;
        }

        public async Task<Stap> CreateStap(string stap, string instrumentname)
        {
            StapReader reader = new StapReader();
            String stapId = stap.Replace("Stap ", "");
            Instrument instrument = GetInstrument(instrumentname);
            List<string> videoIds = reader.ReadVideoIdsByStapId(STEPS_URI, instrument, stapId);
            List<VideoInfo> videoInfosList = GetVideos(videoIds);
            ObservableCollection<VideoInfo> videoInfos = new ObservableCollection<VideoInfo>();
            foreach (VideoInfo vi in videoInfosList)
            {
                videoInfos.Add(vi);
            }

            Stap inMemoryStap = new Stap { Instrument = instrument, Nummer = Convert.ToInt32(stap.Replace("Stap ", "")), VideoLijst = videoInfos };
            return inMemoryStap;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
