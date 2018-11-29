using System;
using System.Collections.Generic;
using System.Text;

namespace StringFunApp.ClassLibrary.Models
{
    public class Stap
    {
        public int Nummer { get; set; }
        public Instrument Instrument { get; set; }
        //public int BoekNummer { get; set; }
        public List<VideoInfo> VideoLijst { get; set; }
    }
}
