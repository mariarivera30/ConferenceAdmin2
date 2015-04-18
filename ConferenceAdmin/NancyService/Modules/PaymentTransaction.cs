using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NancyService.Modules
{
    public class PaymentTransaction
    {

        public PaymentTransaction()
        {

        }

        public string createXMLFile(){
            XElement xml = new XElement("Version", "hola");
            return xml.ToString();
        }
    }
}