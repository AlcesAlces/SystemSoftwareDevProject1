using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemSoftwareDevProject1.HelperClasses.Stocks
{

    //Generate format of the CSV file.
    //Date,Open,High,Low,Close,Volume,Adj Close
    //2014-12-22,47.78,48.80,47.71,48.14,20550600,47.80

    /// <summary>
    /// A single "Stock" entry.
    /// </summary>
    public class aCandleStick
    {
        /// <summary>
        /// It should be noted that since the setters are private, these are readonly.
        /// </summary>
        public DateTime StartingDate { get; private set; }
        public decimal Open { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Close { get; private set; }
        public double Volume { get; private set; }
        public decimal adjClose { get; private set; }

        public aCandleStick(string CSVInput, string name)
        {
            try
            {
                var parsedCsv = CSVInput.Split(',');
                var parsedDateTime = parsedCsv[0].Split('-');
                StartingDate = new DateTime(Int32.Parse(parsedDateTime[0]), Int32.Parse(parsedDateTime[1]), Int32.Parse(parsedDateTime[2]));
                Open = decimal.Parse(parsedCsv[1].ToString());
                High = decimal.Parse(parsedCsv[2].ToString());
                Low = decimal.Parse(parsedCsv[3].ToString());
                Close = decimal.Parse(parsedCsv[4].ToString());
                Volume = double.Parse(parsedCsv[5].ToString());
                adjClose = decimal.Parse(parsedCsv[6].ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
