using System;
using System.Collections.Generic;
using System.Text;

namespace StringFunApp.ClassLibrary.Models
{
    public class Stap
    {
        public Guid Id { get; set; }
        public Instrument Instrument { get; set; }
        public int BoekNummer { get; set; }
        public List<VideoInfo> Videos { get; set; }
    }
}
