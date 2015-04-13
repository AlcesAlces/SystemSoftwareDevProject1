using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemSoftwareDevProject1.HelperClasses.Stocks;

namespace SystemSoftwareDevProject1.HelperFunctions
{
    /// <summary>
    /// Use to filter a list of stocks based on a parameter. 
   /// </summary>
    static class StockFilters
    {

        public static List<aCandleStick> filterByTime(List<aCandleStick> stocks, DateTime start, DateTime end)
        {
            return (from b in stocks
                    where b.StartingDate > start && b.StartingDate < end
                    select b).ToList();
        }


    }
}
