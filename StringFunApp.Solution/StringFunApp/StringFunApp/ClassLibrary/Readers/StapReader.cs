using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class StapReader
    {
        public List<Stap> Read(string uri)
        {
            throw new NotImplementedException();
        }

        public List<string> Read(string uri, Instrument instrument, string id)
        {
            XmlReader reader = XmlImporter.getReader(uri, false);
            reader.ReadToFollowing("instrument");
            do
            {
                string instrumentname = reader.GetAttribute("name");
                if (instrumentname.Contains(instrument.Naam))
                {
                    var inner = reader.ReadSubtree();
                    return ReadStapVideoIds(inner, id);
                }
            } while (reader.ReadToFollowing("instrument"));
            return null;
        }

        private List<string> ReadStapVideoIds(XmlReader reader, string id)
        {
            reader.ReadToFollowing("step");
            do
            {
                string stepnumber = reader.GetAttribute("number");
                if (stepnumber.Equals(id))
                {
                    var inner = reader.ReadSubtree();
                    List<string> videoIds = ReadVideoIds(inner);
                    return videoIds;
                }
            } while (reader.ReadToFollowing("step"));
            return null;
        }

        private List<string> ReadVideoIds(XmlReader reader)
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
