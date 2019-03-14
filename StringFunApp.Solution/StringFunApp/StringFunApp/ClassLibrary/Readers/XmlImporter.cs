using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace StringFunApp.ClassLibrary
{
    class XmlImporter
    {
        static HttpClient client = new HttpClient();

        public static XmlReader GetReader(string uri, bool validated)
        {
            //validate xml
            Stream stream;
            if (validated)
            {
                stream = GetValidatedReader(uri);
            }
            else
            {
                stream = GetUnvalidatedReader(uri);
            }
            return XmlReader.Create(stream);
        }

        private static Stream GetValidatedReader(string uri)
        {
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            //validate xml
            return MakeReader(uri, settings);
        }

        private static Stream GetUnvalidatedReader(string uri)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            //validate xml
            return MakeReader(uri, settings);
        }

        private static Stream MakeReader(string uri, XmlReaderSettings settings)
        {
            var makerReaderTask = client.GetStreamAsync(uri); ;
            Stream stream = makerReaderTask.Result;
            return stream;

        }

        // Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);
        }


    }

}
