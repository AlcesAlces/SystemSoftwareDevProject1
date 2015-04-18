using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SystemSoftwareDevProject1.HelperClasses.Stocks;
using SystemSoftwareDevProject1.HelperFunctions;
using SystemSoftwareDevProject1.HelperFunctions.FileHandlers;

namespace SystemSoftwareDevProject1
{
    public partial class aStockForm : Form
    {
        public aStock _stocks;

        public aStockForm(aStock stocks)
        {
            _stocks = stocks;
            InitializeComponent();
            this.Text = stocks.companyName + ", " + stocks.aPeriodType + ", " + stocks.getDateRange();
        }

        private void populateChartContent(aStock.PeriodType resolution, string stockName, StocksCSVHandler.RetrievalMode mode)
        {

            //Pulls in the stock data that we already have saved to disk.
            //aStock stocks = await StocksCSVHandler.getStockData(stockName, resolution, _stocks.StartingDate, _stocks.EndingDate, mode);
            aStock stocks = _stocks;

            //Apply a date filter on the stocks
            stocks.Candlestick = StockFilters.filterByTime(stocks.Candlestick, _stocks.StartingDate, _stocks.EndingDate);

            chart2.Series.Clear();
            //Create a new series on the chart
            chart1.Series.Add(stockName);
            chart2.Series.Add(stockName);
            //Make sure it's candlestick
            chart1.Series[stockName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            chart2.Series[stockName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            chart1.ChartAreas[0].AxisY.Minimum = ChartFunctions.determineMinimum(stocks.Candlestick);
            chart1.ChartAreas[0].AxisY.Maximum = ChartFunctions.determineMaximum(stocks.Candlestick);
            chart1.Series[stockName]["PriceUpColor"] = "Green";
            chart1.Series[stockName]["PriceDownColor"] = "Red";

            foreach (aCandleStick stock in stocks.Candlestick)
            {
                //Populate a point.
                chart1.Series[stockName].Points.AddXY(stock.StartingDate, stock.High);
                int currentIndex = chart1.Series[stockName].Points.Count - 1;
                chart1.Series[stockName].Points[currentIndex].YValues[1] = (double)stock.Low;
                chart1.Series[stockName].Points[currentIndex].YValues[2] = (double)stock.Open;
                chart1.Series[stockName].Points[currentIndex].YValues[3] = (double)stock.Close;

                chart2.Series[stockName].Points.AddXY(stock.StartingDate, (stock.Volume / 1000));

            }

            chart2.ChartAreas[0].AxisY.Title = "Volume In Thousands";
            chart1.ChartAreas[0].AxisY.Title = "Stocks";

        }

        private async void aStockForm_Load(object sender, EventArgs e)
        {
            //Display
            chart1.Series.Clear();
            try
            {
                if (await StocksCSVHandler.checkFileForDates(_stocks.companyName, _stocks.aPeriodType, _stocks.StartingDate, _stocks.EndingDate))
                {
                    try
                    {
                        populateChartContent(_stocks.aPeriodType, _stocks.companyName, StocksCSVHandler.RetrievalMode.File);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else
                {
                    try
                    {
                        populateChartContent(_stocks.aPeriodType, _stocks.companyName, StocksCSVHandler.RetrievalMode.URL);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
