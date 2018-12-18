using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class StapReader : Reader<Stap>
    {
        public List<Stap> ReadAllObjects(string uri)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadVideoIdsByStapId(string uri, Instrument instrument, string id)
        {
            XmlReader reader = XmlImporter.getReader(uri);
            reader = XmlImporter.getReader(uri);
            reader.ReadToFollowing("instrument");
            do
            {
                string instrumentname = reader.GetAttribute("name");
                if (instrumentname.Contains(instrument.Naam))
                {
                    var inner = reader.ReadSubtree();
                    return readStapVideoIds(inner, id);
                }
            } while (reader.ReadToFollowing("instrument"));
            return null;
        }

        private List<string> readStapVideoIds(XmlReader reader, string id)
        {
            reader.ReadToFollowing("step");
            do
            {
                string stepnumber = reader.GetAttribute("number");
                if (stepnumber.Equals(id))
                {
                    var inner = reader.ReadSubtree();
                    return readVideoIds(inner);
                }
            } while (reader.ReadToFollowing("step"));
            return null;
        }

        private List<string> readVideoIds(XmlReader reader)
        {
            List<string> videoIds = new List<string>();
            reader.ReadToFollowing("videoid");
            do
            {
                string videoid = reader.ReadElementContentAsString();
                videoIds.Add(videoid);
            } while (reader.ReadToFollowing("videoid"));
            return videoIds;
        }
    }
}
