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
    public sealed class Stringfun : INotifyPropertyChanged
    {
        public const string INIT_URI = "https://www.staproeselare.be/stringfun/xml/stringfuninit.xml";
        public const string VIDEOS_URI = "https://www.staproeselare.be/stringfun/xml/stringfunvideos.xml";
        public const string STEPS_URI = "https://www.staproeselare.be/stringfun/xml/stringfunsteps.xml";
        public const string STEPS_URI_UNVALIDATED = "https://www.staproeselare.be/stringfun/xml/stringfunstepsUnvalidated.xml";
        public const string VIDEOS_URI_UNVALIDATED = "https://www.staproeselare.be/stringfun/xml/stringfunvideosUnvalidated.xml";

        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly Lazy<Stringfun> _instance = new Lazy<Stringfun>(() => new Stringfun());

        private Stringfun()
        {
            books = ReadBooks(INIT_URI);
            instruments = ReadInstruments(INIT_URI);
        }

        public static Stringfun Instance
        {
            get { return _instance.Value; }
        }

        private static List<Instrument> ReadInstruments(string uri)
        {
            InstrumentReader instrReader = new InstrumentReader();
            return instrReader.Read(uri);
        }

        private static List<Boek> ReadBooks(string uri)
        {
            BookReader bookReader = new BookReader();
            return bookReader.Read(uri);
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

        public Stap getStap(string stap, string instrumentname)
        {
            int stapNumber = Convert.ToInt32(stap.Replace("Stap ", ""));
            Instrument instrument = GetInstrument(instrumentname);
            Stap inMemoryStap = StapFactory.CreateStap(stapNumber, instrument);
            return inMemoryStap;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
