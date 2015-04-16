using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemSoftwareDevProject1.HelperClasses.Stocks;

namespace SystemSoftwareDevProject1.HelperFunctions
{
    static class ChartFunctions
    {

        public static int determineMaximum(List<aCandleStick> stocks)
        {
            try
            {
                decimal maximum = stocks.Max(x => x.High);

                return (int)(maximum + 1);
            }

            catch
            {
                return 0;
            }
        }

        public static int determineMinimum(List<aCandleStick> stocks)
        {
            try
            {
                decimal minimum = stocks.Min(x => x.Low);

                return (int)minimum;
            }

            catch
            {
                return 0;
            }
        }

    }
}
