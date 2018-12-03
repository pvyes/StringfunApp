using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace StringFunApp.ClassLibrary
{
    class XmlImporter
    {
        private static XmlDocument xmlfile;
        private static bool validated = true;

        public XmlImporter()
        {
            xmlfile = new XmlDocument();
        }

        public static XmlReader getReader(string uri)
        {
            //validate xml
            return validateXml(uri);
        }

        private static XmlReader validateXml(string uri)
        {
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            return XmlReader.Create(uri, settings);
        }

        // Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);

            else
                Console.WriteLine("\tValidation error: " + args.Message);
            validated = false;
        }


    }

}
