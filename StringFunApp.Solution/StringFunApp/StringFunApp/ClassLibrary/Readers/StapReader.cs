﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Android.OS;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class StapReader
    {
        public async Task<List<string>> ReadVideoIdsAsync(string uri, string id, string instrument)
        {
            var readerTask = XmlImporterAsync.getReaderAsync(uri, false);
            var reader = await readerTask;
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
