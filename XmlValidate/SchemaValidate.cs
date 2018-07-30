using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using NLog;

namespace XmlValidate
{
    public class SchemaValidate
    {

        private List<string> errorList = new List<string>();
        private Logger _logger;

        public SchemaValidate()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public bool ValidateSchema(string xml, string schema)
        {
            try
            {
                XmlReader reader;
                XmlReaderSettings settings = new XmlReaderSettings();
                XmlSchemaSet schemaSet = new XmlSchemaSet();

                schemaSet.Add(null, schema);
                settings.ValidationEventHandler += new ValidationEventHandler(this.SchemaValidationHandler);

                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schemaSet;
                settings.DtdProcessing = DtdProcessing.Ignore;

                reader = XmlReader.Create(xml, settings);

                if(errorList == null || errorList.Count == 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return false;
            }
        }

        public void SchemaValidationHandler(Object sender, ValidationEventArgs args)
        {
            try
            {
                if (errorList == null)
                {
                    errorList = new List<string>();
                }
                string sError = $"<span style='font-weight:bold;'>Line:</span> {args.Exception.LineNumber.ToString()}; ";
                sError += $"<span style='font-weight:bold;'>Line Position:</span> {args.Exception.LinePosition.ToString()}; ";
                sError += $"<span style='font-weight:bold;'>Exception Message:</span> {args.Exception.Message.ToString().Replace("System.FormatException:", "")}; ";
                errorList.Add(sError);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }
    }
}
