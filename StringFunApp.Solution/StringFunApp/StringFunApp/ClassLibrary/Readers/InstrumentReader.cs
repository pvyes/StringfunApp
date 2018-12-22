using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class InstrumentReader : Reader<Instrument>
    {
         public List<Instrument> ReadAllObjects(string uri)
            {
                List<Instrument> instrumentlist = ReadAllObjectsAsync(uri).Result;
                return instrumentlist;
            }

    private async Task<List<Instrument>> ReadAllObjectsAsync(string uri)
        {
            XmlReader reader = await XmlImporter.getReader(uri);
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
