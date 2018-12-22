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
        private static bool validated = true;

        public async static Task<XmlReader> getReader(string uri)
        {
            //validate xml
            return await SetValidateXml(uri);
        }

        public async static Task<XmlReader> getUnvalidatedReader(string uri)
        {
            //validate xml
            return await SetUnvalidateXml(uri);
        }

        private async static Task<XmlReader> SetUnvalidateXml(string uri)
        {
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            // Create the XmlReader object.
            HttpClient client = new HttpClient();
            Stream stream = await client.GetStreamAsync(uri);
            return XmlReader.Create(stream, settings);
        }

        private async static Task<XmlReader> SetValidateXml(string uri)
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

            // Create the XmlReader object.
            var client = new HttpClient();
            var stream = await client.GetStreamAsync(uri);
            return XmlReader.Create(stream, settings);
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
