using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SystemSoftwareDevProject1.HelperClasses.Stocks;

namespace SystemSoftwareDevProject1.HelperFunctions.FileHandlers
{
    /// <summary>
    /// Use to maintain, download, delete, manage all files related to Stocks int he CSV format.
    /// </summary>
    public static class StocksCSVHandler
    {

        public enum RetrievalMode { File, URL };

        #region file I/O handlers

        //Format
        //"http://ichart.yahoo.com/table.csv?s=MSFT&a=0&b=1&c=2000&d=11&e=24&f=2014&g=w&ignore=.csv";

        public static string urlBuilder(string name, DateTime start, DateTime end, aStock.PeriodType resolution)
        {
            //Need to put in the company name
            string toReturn = "http://ichart.yahoo.com/table.csv?s=";

            //Build the day month year section
            toReturn += name + "&a=" + (start.Month - 1) + "&b=" + start.Day + "&c=" +start.Year;
            toReturn += "&d=" + (end.Month - 1) + "&e=" + end.Day + "&f=" + end.Year;

            //Build the resolution section
            toReturn += "&g=" + (char)resolution + "&ignore=.csv";

            return toReturn;
        }

        /// <summary>
        /// Download the specified file, into the specified path
        /// </summary>
        /// <param name="clientString"></param>
        /// <param name="path"></param>
        public static async Task downloadFile(string clientString, string path)
        {
            byte[] info = await getContentAsync(clientString);
            saveCsvFromByteArray(info, path);
        }

        /// <summary>
        /// Async method for getting the CSV file.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<byte[]> getContentAsync(string url)
        {
            var content = new MemoryStream();

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            using(WebResponse response = await webRequest.GetResponseAsync())
            {
                using(Stream responseStream = response.GetResponseStream())
                {
                    await responseStream.CopyToAsync(content);
                }
            }

            return content.ToArray();
        }

        /// <summary>
        /// Save the byte array
        /// </summary>
        /// <param name="toSave"></param>
        /// <param name="path"></param>
        private static void saveCsvFromByteArray(byte[] toSave, string path)
        {
            File.WriteAllText(path, System.Text.Encoding.Default.GetString(toSave));
        }

        /// <summary>
        /// Get the CSV string from the yahoo URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<string> csvStringFromUrl(string url)
        {
            byte[] data = await getContentAsync(url);
            return System.Text.Encoding.Default.GetString(data);
        }

        /// <summary>
        /// Ensure that all files are downloaded, and accurate (up to date).
        /// </summary>
        public static async Task<bool> verifyDefaultDownloadedFiles()
        {

            verifyDirectories();

            //List of the abbreviation of stock ticker names
            List<string> defaultStocks = XmlInformation.defaultStocks();
            List<string> listOfPaths = XmlInformation.defaultPaths();

            DateTime start = XmlInformation.defaultStartDate();
            DateTime end = XmlInformation.defaultEndDate();

            await fileDownloader(listOfPaths, defaultStocks, start, end);

            return true;
        }

        public static async Task fileDownloader(List<string> listOfPaths, List<string> stocks, DateTime start, DateTime end)
        {
            foreach (string item in listOfPaths)
            {
                foreach (string subitem in stocks)
                {
                    string path = item + "\\" + subitem + ".csv";

                    if (File.Exists(path))
                    {
                        //File exists, check to see if file is up to date.
                        if (verifyFileUpToDate(subitem, getResolutionByPath(item)))
                        {
                            //File is up to date!
                        }

                        else
                        {
                            await downloadFile(urlBuilder(subitem, start, end, getResolutionByPath(item)), path);
                        }
                    }

                    else
                    {
                        //File does not exist, we need to download it and put it in the directory.
                        await downloadFile(urlBuilder(subitem, start, end, getResolutionByPath(item)), path);
                    }
                }
            }
        }

        /// <summary>
        /// Ensure that default directories exist.
        /// </summary>
        public static void verifyDirectories()
        {
            List<String> paths = XmlInformation.defaultPaths();

            foreach(string item in paths)
            {
                if(!Directory.Exists(item))
                {
                    Directory.CreateDirectory(item);
                }
            }
        }

        public static async Task<List<aCandleStick>> getStockData(string name, aStock.PeriodType resolution, DateTime start, DateTime end, RetrievalMode mode)
        {

            string path = getPathByResolution(resolution) + "\\" + name + ".csv";

            List<aCandleStick> toReturn;

            if (mode == RetrievalMode.File)
            {

                if (File.Exists(path))
                {
                    toReturn = readStockDataFromFile(name, resolution, start, end);
                }

                else
                {
                    toReturn = await readStockDataFromURL(name, resolution, start, end);
                }
            }

            else
            {
                toReturn = await readStockDataFromURL(name, resolution, start, end);
            }

            return toReturn;

        }

        /// <summary>
        /// Use to find saved data about stocks.
        /// </summary>
        /// <param name="name"> The name of the stock eg: GOOG</param>
        /// <param name="resolution"> The resolution scope eg: m, w, d</param>
        /// <returns></returns>
        private static List<aCandleStick> readStockDataFromFile(string name, aStock.PeriodType resolution, DateTime start, DateTime end)
        {
            string path = getPathByResolution(resolution) + "\\" + name + ".csv";

            List<aCandleStick> toReturn;

            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                string str = System.Text.Encoding.Default.GetString(data);

                toReturn = fileCsvToStockList(str, name);
            }
            else
            {
                //File does not exist.
                toReturn = null;
            }

            return StockFilters.filterByTime(toReturn,start, end);
        }

        private static async Task<List<aCandleStick>> readStockDataFromURL(string name, aStock.PeriodType resolution, DateTime start, DateTime end)
        {

            string url = urlBuilder(name, start, end, resolution);

            return fileCsvToStockList(await csvStringFromUrl(url), name);
        }

        /// <summary>
        /// Creates a List of stocks from a comma seperated string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static List<aCandleStick> fileCsvToStockList(string str, string name)
        {

            List<aCandleStick> toReturn = new List<aCandleStick>();

            List<string> split = str.Split('\n').ToList();

            if (split[0].Split(',')[0] == "Date")
            {
                split.RemoveAt(0);
            }

            if (split[split.Count - 1] == "")
            {
                split.RemoveAt(split.Count - 1);
            }

            foreach (string item in split)
            {
                toReturn.Add(new aCandleStick(item, name));
            }

            return toReturn;
        }

        /// <summary>
        /// Returns true if file is up to date.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        private static bool verifyFileUpToDate(string name, aStock.PeriodType resolution)
        {

            string path = getPathByResolution(resolution) + "\\" + name + ".csv";

            var timeToTest = (DateTime.Now - File.GetLastWriteTime(path)).Days;

            if(timeToTest >= 1)
            {
                //Information is very old comparatively, update it.
                return false;
            }

            else
            {
                //Information may be relatively old.
                if (DateTime.Now.Hour >= 17)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// checks to see if the end and start dates are in the file if not DL a new file with the data needed
        /// </summary>
        /// <param name="name"> GOOL</param>
        /// <param name="resolution">w d m</param>
        /// <returns></returns>
        public static async Task<bool> checkFileForDates(string name, aStock.PeriodType resolution, DateTime start, DateTime end)
        {
            string path = getPathByResolution(resolution) + "\\" + name + ".csv";
            if (File.Exists(path))
            {
                List<aCandleStick> stockList = new List<aCandleStick>();

                byte[] data = File.ReadAllBytes(path);
                string str = System.Text.Encoding.Default.GetString(data);
                stockList = fileCsvToStockList(str, name);

                List<aCandleStick> startReturn = new List<aCandleStick>();
                List<aCandleStick> endReturn = new List<aCandleStick>();



                if (resolution == aStock.PeriodType.DAILY)
                {
                    startReturn = (from b in stockList
                                   where b.StartingDate.Equals(start)
                                   select b).ToList();

                    endReturn = (from b in stockList
                                 where b.StartingDate.Equals(end)
                                 select b).ToList();
                }
                else if (resolution.Equals(aStock.PeriodType.WEEKLY))
                {
                    //qurey for weekly
                    DateTime thing = start.AddDays(-7);

                    startReturn = (from b in stockList
                                   where b.StartingDate.CompareTo((start.AddDays(7))) <= 0
                                   select b).ToList();

                    endReturn = (from b in stockList
                                 where b.StartingDate.CompareTo(end.AddDays(-7)) >= 0
                                 select b).ToList();
                }
                else
                {
                    //must me monthly
                    //need to check if the date selected it +- a month away
                    startReturn = (from b in stockList
                                   where b.StartingDate.CompareTo(start.AddMonths(1)) <= 0
                                   select b).ToList();

                    endReturn = (from b in stockList
                                 where b.StartingDate.CompareTo(end.AddMonths(-1)) >= 0
                                 select b).ToList();
                }

                if (startReturn.Count == 0 || endReturn.Count == 0)
                {
                    //the file dosent contain the dates needed 
                    //need to DL new one
                    List<string> Stocks = new List<string>();
                    List<string> Paths = new List<string>();

                    Stocks.Add(name);
                    Paths.Add(StocksCSVHandler.getPathByResolution(resolution));

                    await downloadFile(urlBuilder(name, start, end, resolution), path);
                    return false;
                }
                else
                {
                    //nothing needs to be done use it
                    return true;
                }
            }
            else
            {//File does not exist, we need to download it and put it in the directory.
                await downloadFile(urlBuilder(name, start, end, resolution), path);
                return false;
            }
        }

        #endregion

        #region file path functions

        /// <summary>
        /// Returns the path which is associated with the resolution (eg where are daily CSV's kept)
        /// </summary>
        /// <param name="resolution">Passed in as d, w, or m</param>
        /// <returns name="toReturn">returns the path associated with the resolution. Returns empty if not found.</returns>
        public static string getPathByResolution(aStock.PeriodType resolution)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Configuration\DefaultConfig.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/defaultconfiguration/defaultfilepaths/path");

            string toReturn = "";

            foreach(XmlNode node in nodes)
            {
                string toCompare = node.SelectSingleNode("resolution").InnerText;

                if(toCompare == ((char)resolution).ToString())
                {
                    toReturn = node.SelectSingleNode("path").InnerText;
                    break;
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Returns the resolution by path.
        /// </summary>
        /// <param name="path">Argumetn passed in as a file path.</param>
        /// <returns name = "toReturn">Return based on the match. Returns empty if not found.</returns>
        public static aStock.PeriodType getResolutionByPath(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Configuration\DefaultConfig.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/defaultconfiguration/defaultfilepaths/path");

            string toReturn = "";

            foreach (XmlNode node in nodes)
            {
                string toCompare = node.SelectSingleNode("path").InnerText;

                if (toCompare == path)
                {
                    toReturn = node.SelectSingleNode("resolution").InnerText;
                    break;
                }
            }

            switch(toReturn)
            {
                case "m":
                    return aStock.PeriodType.MONTHLY;
                case "w":
                    return aStock.PeriodType.WEEKLY;
                case "d":
                    return aStock.PeriodType.DAILY;
                default:
                    return aStock.PeriodType.DAILY;
            }
        }

        #endregion

    }
}
