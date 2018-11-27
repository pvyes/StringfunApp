using System;
using System.Collections.Generic;
using System.Text;

namespace StringFunApp.ClassLibrary.Models
{
    public abstract class Instrument
    {
        public static Instrument IsViool()
        {
            return new Viool() { Naam = "Viool" };
        }

        public static Instrument IsAltviool()
        {
            return new Altviool() { Naam = "Altviool" };
        }

        public static Instrument IsCello()
        {
            return new Cello() { Naam = "Cello" };
        }
    }
}
