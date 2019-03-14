using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class InstrumentReader
    {
        public List<Instrument> Read(string uri)
        {
            XmlReader reader = XmlImporter.GetReader(uri, true);
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
