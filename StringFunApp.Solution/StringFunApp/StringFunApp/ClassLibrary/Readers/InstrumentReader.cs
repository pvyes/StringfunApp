using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class InstrumentReader : Reader<Instrument>
    {
        public List<Instrument> ReadAllObjects(string uri)
        {
            XmlReader reader = XmlImporter.getReader(uri);
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
    }
}
