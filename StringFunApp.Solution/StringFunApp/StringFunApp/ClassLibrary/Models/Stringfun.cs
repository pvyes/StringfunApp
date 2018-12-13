﻿using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StringFunApp.ClassLibrary.Models
{
    public class Stringfun : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private XmlReader reader;

        private List<VideoInfo> videos;
        public List<VideoInfo> Videos
        {
            get { return videos; }
            set { videos = value; }
        }

        public IEnumerable<Instrument> GetInstruments()
        {
            reader = XmlImporter.getReader("https://www.staproeselare.be/stringfun/xml/stringfuninit.xml");
            List<Instrument> instruments = new List<Instrument>();
            reader.ReadToFollowing("instrument");
            do
            {
                string naam;
                string imagesource;
                reader.ReadToDescendant("name");
                naam = reader.ReadString();
                reader.ReadToNextSibling("imagesource");
                imagesource = reader.ReadString();
                Instrument instrument = new Instrument { Naam = naam, ImageSource = imagesource };
                instruments.Add(instrument);
            } while (reader.ReadToFollowing("instrument"));
            return instruments;
        }

        public IEnumerable<Boek> GetBooks()
        {
            reader = XmlImporter.getReader("https://www.staproeselare.be/stringfun/xml/stringfuninit.xml");
            List<Boek> boeken = new List<Boek>();
            reader.ReadToFollowing("book");
            do
            {
                int nummer;
                string kleur;
                int firststap;
                int laststap;
                reader.ReadToDescendant("number");
                nummer = Convert.ToInt32(reader.ReadString());
                reader.ReadToNextSibling("color");
                kleur = reader.ReadString();
                reader.ReadToFollowing("first");
                firststap = Convert.ToInt32(reader.ReadString());
                reader.ReadToNextSibling("last");
                laststap = Convert.ToInt32(reader.ReadString());
                Boek boek = new Boek { Nummer = nummer, Kleur = kleur, FirstStep = firststap, LastStep = laststap };
                boeken.Add(boek);
            } while (reader.ReadToFollowing("book"));
            return boeken;
        }

        public Boek GetBook(int boeknummer)
        {
            reader = XmlImporter.getReader("https://www.staproeselare.be/stringfun/xml/stringfuninit.xml");
            Boek boek = new Boek();
            while (reader.ReadToFollowing("book"))
            {
                if (reader.GetAttribute("id") == boeknummer.ToString())
                {
                    int nummer;
                    string kleur;
                    int firststap;
                    int laststap;
                    reader.ReadToDescendant("number");
                    nummer = Convert.ToInt32(reader.ReadString());
                    reader.ReadToNextSibling("color");
                    kleur = reader.ReadString();
                    reader.ReadToFollowing("first");
                    firststap = Convert.ToInt32(reader.ReadString());
                    reader.ReadToNextSibling("last");
                    laststap = Convert.ToInt32(reader.ReadString());
                    boek.Nummer = nummer;
                    boek.Kleur = kleur;
                    boek.FirstStep = firststap;
                    boek.LastStep = laststap;
                }
            }
            return boek;
        }

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
            Videos = new List<VideoInfo>(await GetVideos(videoIds));
            Stap InMemoryStap;
            ObservableCollection<VideoInfo> StapVideos = new ObservableCollection<VideoInfo>();
            foreach (VideoInfo video in Videos)
            {
                StapVideos.Add(video);
            }
            InMemoryStap = new Stap { Nummer = Convert.ToInt32(stap.Replace("Stap ", "")), VideoLijst = StapVideos };
            return InMemoryStap;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
