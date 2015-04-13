using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SystemSoftwareDevProject1.HelperFunctions
{
    static class XmlInformation
    {
        #region XML interfaces

        /// <summary>
        /// Return the default stocks which are in the configuration XML
        /// </summary>
        /// <returns></returns>
        public static List<string> defaultStocks()
        {
            //Get all default information from XML file.
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Configuration\DefaultConfig.xml");

            //List of the abbreviation of stock ticker names
            List<string> defaultStocks = new List<string>();

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/defaultconfiguration/defaultstocks/stock");

            foreach (XmlNode node in nodes)
            {
                defaultStocks.Add(node.SelectSingleNode("tag").InnerText);
            }

            return defaultStocks;
        }

        /// <summary>
        /// Return the default start date which is found in the XML configuration.
        /// </summary>
        /// <returns></returns>
        public static DateTime defaultStartDate()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Configuration\DefaultConfig.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/defaultconfiguration/defaultrange");

            string toParseStart = nodes[0].SelectSingleNode("start").InnerText;
            var parsedStart = toParseStart.Split(',');

            return new DateTime(Int32.Parse(parsedStart[0]), Int32.Parse(parsedStart[1]), Int32.Parse(parsedStart[2]));
        }

        /// <summary>
        /// Return the default end date that is found in the XMl configuration
        /// </summary>
        /// <returns></returns>
        public static DateTime defaultEndDate()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Configuration\DefaultConfig.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/defaultconfiguration/defaultrange");

            string toParseStart = nodes[0].SelectSingleNode("end").InnerText;

            if (toParseStart.Trim() == "TODAY")
            {
                return DateTime.Now;
            }

            else
            {
                var parsedStart = toParseStart.Split(',');
                return new DateTime(Int32.Parse(parsedStart[0]), Int32.Parse(parsedStart[1]), Int32.Parse(parsedStart[2]));
            }
       }

        /// <summary>
        /// Default paths specified by the XML file.
        /// </summary>
        /// <returns></returns>
        public static List<string> defaultPaths()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Configuration\DefaultConfig.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/defaultconfiguration/defaultfilepaths/path");

            List<string> listOfPaths = new List<string>();

            foreach(XmlNode node in nodes)
            {
                listOfPaths.Add(node.SelectSingleNode("path").InnerText);
            }

            return listOfPaths;
        }

        #endregion
    }
}
