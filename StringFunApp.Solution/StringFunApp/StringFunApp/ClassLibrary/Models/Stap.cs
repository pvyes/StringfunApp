using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StringFunApp.ClassLibrary.Models
{
    public class Stap
    {
        public int Nummer { get; set; }
        public string Instrument { get; set; }
        //public int BoekNummer { get; set; }
        public ObservableCollection<VideoInfo> VideoLijst { get; set; }
    }
}
