using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace Doc2OpenXml
{
    class ValidateXML
    {

        public void Parser(String Xmlpath)
        {
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(Xmlpath, settings);

            // Parse the file.
            while (reader.Read()) ;
        }

        // Display any validation errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation Error: {0}", e.Message);
        }
    }
}