using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace EMS_System.Util
{
    public static class XMLReader
    {
        public const string FILELOCATION = @"\languages.xml";

        public static string GetText(string searchParam)
        {
            string result = "";
            XmlDocument doc = new XmlDocument();
            XmlNodeList languagesNodes;


            doc.Load(AppDomain.CurrentDomain.BaseDirectory + FILELOCATION);

            switch (CultureInfo.CurrentCulture.ToString())
            {
                case "nl-L":
                    languagesNodes = doc.SelectNodes("/Languages/Dutch/child::*");
                    break;

                default:
                    languagesNodes = doc.SelectNodes("/Languages/English/child::*");
                    break;
            }

            foreach (XmlNode node in languagesNodes)
            {
                if (node.Name == searchParam)
                    result = node.InnerText;
            }
            return result;
        }
    }
}
