using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemSoftwareDevProject1.HelperFunctions.FileHandlers;

namespace SystemSoftwareDevProject1.HelperClasses.Stocks
{
    /// <summary>
    /// Container class used to hold information related to collection of stocks.
    /// </summary>
    public class aStock
    {
        public enum PeriodType { DAILY = 'd', MONTHLY='m', WEEKLY='w'}
        
        public List<aCandleStick> Candlestick = new List<aCandleStick>();
        public PeriodType aPeriodType;
        public DateTime StartingDate;
        public DateTime EndingDate;
        public string companyName { get; set; }

        /// <summary>
        /// Defunct constructor
        /// </summary>
        /// <param name="candleSticks"></param>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="res"></param>
        public aStock(List<aCandleStick> candleSticks, string name, DateTime start, DateTime end, PeriodType res)
        {
            aPeriodType = res;
            Candlestick = candleSticks;
            companyName = name;
            StartingDate = start;
            EndingDate = end;
        }

        /// <summary>
        /// Constructor to be used to create an aStock object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="res"></param>
        /// <param name="mode"></param>
        public static async Task<Tuple<aStock,bool>> aStockAsync(string name, DateTime start, DateTime end, PeriodType res, StocksCSVHandler.RetrievalMode mode)
        {
            List<aCandleStick> _candlestick = new List<aCandleStick>();

            if(mode == StocksCSVHandler.RetrievalMode.File)
            {
                _candlestick = await ReadFromFile(name,start,end,res,mode);
            }

            else
            {
                _candlestick = await ReadFromURL(name, start, end, res, mode);
            }

            return new Tuple<aStock,bool>(new aStock(_candlestick, name, start, end, res), true);
        }

        /// <summary>
        /// Create your stock data from file.
        /// </summary>
        /// <param name="mode"></param>
        public static async Task<List<aCandleStick>> ReadFromFile(string name, DateTime start, DateTime end, PeriodType res, StocksCSVHandler.RetrievalMode mode)
        {
            List<aCandleStick> _Candlestick = null;
            try
            {
                _Candlestick = await StocksCSVHandler.getStockData(name, res, start, end, mode);
            }
            catch
            {
                MessageBox.Show("Something went wrong! No, you don't get to know what it is");
            }

            return _Candlestick;
        }

        /// <summary>
        /// Create your stock data from URL.
        /// </summary>
        /// <param name="mode"></param>
        public static async Task<List<aCandleStick>> ReadFromURL(string name, DateTime start, DateTime end, PeriodType res, StocksCSVHandler.RetrievalMode mode)
        {
            List<aCandleStick> _Candlestick = null;
            try
            {
                _Candlestick = await StocksCSVHandler.getStockData(name, res, start, end, mode);
            }
            catch
            {
                MessageBox.Show("Something went wrong! No, you don't get to know what it is");
            }

            return _Candlestick;
        }

        public string getDateRange()
        {
            StringBuilder toReturn = new StringBuilder();

            toReturn.Append(StartingDate.Month + "/" + StartingDate.Day + "/" + StartingDate.Year);
            toReturn.Append(" - ");
            toReturn.Append(EndingDate.Month + "/" + EndingDate.Day + "/" + EndingDate.Year);
            return toReturn.ToString(); ;
        }
    }
}
