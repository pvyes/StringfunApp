using System;
using System.Collections.Generic;
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

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
