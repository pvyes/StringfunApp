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
    class XmlImporterAsync
    {
        public static async Task<XmlReader> getReaderAsync(string uri, bool validated)
        {
            //validate xml
            if (validated)
            {
                return await getValidatedReaderAsync(uri);
            }
            else
            {
                return await GetUnvalidatedReader(uri);
            }
        }

        private static async Task<XmlReader> getValidatedReaderAsync(string uri)
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
            return await MakeReaderAsync(uri, settings);
        }

        private async static Task<XmlReader> GetUnvalidatedReader(string uri)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            //validate xml
            return await MakeReaderAsync(uri, settings);
        }

        private static async Task<XmlReader> MakeReaderAsync(string uri, XmlReaderSettings settings)
        {
            HttpClient client = new HttpClient();
            Stream stream = await client.GetStreamAsync(uri).ConfigureAwait(false);
            return XmlReader.Create(stream, settings);
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
