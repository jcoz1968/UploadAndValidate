using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace XmlValidate
{
    public class SchemaValidate
    {
        public SchemaValidate()
        {
        }

        public bool ValidateSchema(string xml, string schema)
        {
            try
            {
                XmlReader reader;
                XmlReaderSettings settings = new XmlReaderSettings();
                XmlSchemaSet schemaSet = new XmlSchemaSet();

                schemaSet.Add(null, schema);
                //settings.ValidationEventHandler += new ValidationEventHandler()

                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schemaSet;
                settings.DtdProcessing = DtdProcessing.Ignore;

                reader = XmlReader.Create(xml, settings);




                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
