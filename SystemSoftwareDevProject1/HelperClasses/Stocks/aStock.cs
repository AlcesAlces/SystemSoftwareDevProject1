using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        public enum aPeriodType { DAILY = 'd', MONTHLY='m', WEEKLY='w'}
        
        public List<aCandleStick> Candlestick = new List<aCandleStick>();
        public aPeriodType PeriodType;
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
        public aStock(List<aCandleStick> candleSticks, string name, DateTime start, DateTime end, aPeriodType res)
        {
            PeriodType = res;
            Candlestick = candleSticks;
            companyName = name;
            StartingDate = start;
            EndingDate = end;
        }

        public aStock()
        {

        }

        /// <summary>
        /// Function to return a stock object/
        /// </summary>
        /// <param name="name">Name of the company</param>
        /// <param name="start">Starting period</param>
        /// <param name="end">Ending period</param>
        /// <param name="res">Resolution (aPeriodType from aSTock class)</param>
        /// <returns></returns>
        public static aStock createaStock(string name, DateTime start, DateTime end, aPeriodType res)
        {
            aStock toReturn = new aStock()
            {
                PeriodType = res,
                Candlestick = null,
                companyName = name,
                StartingDate = start,
                EndingDate = end
            };

            //Daily resolution
            if(res == aPeriodType.DAILY)
            {
                toReturn = toReturn.generateDailyPeriod(toReturn.PeriodType);
            }

            //Monthly, or weekly.
            else
            {
                toReturn = toReturn.generateOtherPeriod(toReturn.PeriodType);
            }

            return toReturn;
        }

        /// <summary>
        /// Get the daily stock data. If it isn't downloaded, download it.
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public aStock generateDailyPeriod(aPeriodType period)
        {
            string path = StocksCSVHandler.getPathByResolution(PeriodType) + "\\" + companyName + ".csv";

            List<aCandleStick> toReturn = new List<aCandleStick>();

            if (!File.Exists(path))
            {
                StocksCSVHandler.fileDownloaderSync(companyName);
            }

            toReturn = StocksCSVHandler.readStockDataFromFile(companyName, PeriodType, StartingDate, EndingDate);
            Candlestick = toReturn;

            return this;
        }

        /// <summary>
        /// Get he various other kinds of stock data (Monthly, Weekly) based on daily data.
        /// If daily data does not exist, download daily data.
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public aStock generateOtherPeriod(aPeriodType period)
        {
            string dailyPath = StocksCSVHandler.getPathByResolution(aStock.aPeriodType.DAILY) + "\\" + companyName + ".csv";
            if (!File.Exists(dailyPath))
            {
                StocksCSVHandler.fileDownloaderSync(companyName);
            }

            List<aCandleStick> toReturn = new List<aCandleStick>();
            List<aCandleStick> tempSticks = StocksCSVHandler.readStockDataFromFile(companyName, aStock.aPeriodType.DAILY, new DateTime(1900, 1, 1), DateTime.Now);

            if(period == aStock.aPeriodType.MONTHLY)
            {
                //Linq statement to get all of the month data
                (from b in tempSticks
                where b.StartingDate >= StartingDate.Date && b.StartingDate <= EndingDate
                //Group everything by a starting year, creating our first container.
                group b by b.StartingDate.Year into yg
                select new
                {
                    yearGroup =
                    from c in yg
                    //Group everything in each year into a month container.
                    group c by c.StartingDate.Month into mg
                    //Select statement using the candlestick constructor, and lambda aggrigation.
                    select new aCandleStick(mg.First().StartingDate, mg.Last().Open, mg.Min(x => x.Low),
                                            mg.Max(x => x.High), mg.First().Close, mg.Sum(x => x.Volume), mg.Last().adjClose)
                   //Use the built in foreach loops to avoid creating more code than needed.
                }).ToList().ForEach(x => x.yearGroup.ToList().ForEach(y => toReturn.Add(y)));
            }

            else
            {
                (from b in tempSticks
                where b.StartingDate >= StartingDate.Date && b.StartingDate <= EndingDate
                //Group everything by year first/
                group b by b.StartingDate.Year into yg
                select new
                {
                    yearGroup =
                    from c in yg
                    //Group each year group by month.
                    group c by c.StartingDate.Month into mg
                    select new
                    {
                        monthGroup =
                        from d in mg
                        //Use the calendar class to group every month group by week of the year.
                        group d by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d.StartingDate,CalendarWeekRule.FirstFourDayWeek,DayOfWeek.Monday) into wg
                            //Use our constructor and some lambdas to get the information we need.
                            select new aCandleStick(wg.First().StartingDate,wg.Last().Open,wg.Min(x => x.Low),
                                                wg.Max(x => x.High),wg.First().Close,wg.Sum(x => x.Volume),wg.Last().adjClose)
                    }
                    //Use the built in for-loop and some more lambdas to get our information.
                }).ToList().ForEach(x=> x.yearGroup.ToList().ForEach(y=>y.monthGroup.ToList().ForEach(z=>toReturn.Add(z))));
                
            }

            Candlestick = toReturn;

            return this;
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
