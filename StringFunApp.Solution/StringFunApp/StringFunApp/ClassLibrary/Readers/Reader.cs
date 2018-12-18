using System.Collections.Generic;
using System.Xml;

namespace StringFunApp.ClassLibrary.Readers
{
    interface Reader<T>
    {
        List<T> ReadAllObjects(string uri);
    }
}
