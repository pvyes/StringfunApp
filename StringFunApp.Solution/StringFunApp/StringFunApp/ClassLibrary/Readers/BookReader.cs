using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using StringFunApp.ClassLibrary.Models;

namespace StringFunApp.ClassLibrary.Readers
{
    public class BookReader : Reader<Boek>
    {
        public List<Boek> ReadAllObjects(string uri)
        {
            XmlReader reader = XmlImporter.getReader(uri);
            reader = XmlImporter.getReader(uri);
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
    }
}
