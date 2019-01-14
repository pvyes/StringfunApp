using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Android.OS;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class StapReader : AsyncTask<string, int, List<string>>
    {
        Stap stap;

        public StapReader()
        {
            stap = new Stap();
        }

        private List<string> ReadVideoIds(string uri, string id, string instrument)
        {
            XmlReader reader = XmlImporter.getReader(uri, false);
            try
            {
                reader.ReadToFollowing("instrument");
                do
                {
                    string instrumentname = reader.GetAttribute("name");
                    if (instrumentname.Contains(instrument))
                    {
                        var inner = reader.ReadSubtree();
                        return ReadStapVideoIds(inner, id);
                    }
                } while (reader.ReadToFollowing("instrument"));
            }
            catch (Exception e)
            {
                Console.WriteLine("\tReader error: " + e.Message);
            }
           
            return null;
        }

        protected override List<string> RunInBackground(params string[] @params)
        {
            List<string> videoIds = ReadVideoIds(@params[0], @params[1], @params[2]);
            return videoIds;
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
