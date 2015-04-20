using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemSoftwareDevProject1.HelperClasses.Stocks;
using SystemSoftwareDevProject1.HelperFunctions;
using SystemSoftwareDevProject1.HelperFunctions.FileHandlers;

namespace SystemSoftwareDevProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            foreach(string item in CSVParse.CsvToStringList())
            {
                cbStocks.Items.Add(item);   
            }

            cbStocks.SelectedIndex = 0;
        }

        /// <summary>
        /// Load method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Form1_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            tsslMain.Text = "Downloading and verifying Default Values...";
            //TODO: Move this elsewhere. This verifies that we have the dfault XML values
            this.Enabled = await StocksCSVHandler.verifyDefaultDownloadedFiles();

            /*
             * this is here to test the check file for date function i worte 
             * i already had the PIH daily DL
             */

            //set the max date for the datepickers
            DateTime thisDay = DateTime.Today;
            dpToDate.MaxDate = thisDay.Date;
            dpFromDate.MaxDate = thisDay.Date;

            //select the download radio button
            rbDownload.Select();
            rbDay.Select();

            DataTable companayList = Global.companyList;

            while(this.Enabled == false)
            {

            }

            tsslMain.Text = "Default information downloaded and verified.";
        }

        /// <summary>
        /// Event handler for the "Go!" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGo_Click(object sender, EventArgs e)
        {

            aStock.aPeriodType resolution = aStock.aPeriodType.DAILY;
            string stock = cbStocks.Text;

            if (rbDay.Checked)
            {
                resolution = aStock.aPeriodType.DAILY;
            }

            else if(rbMonth.Checked)
            {
                resolution = aStock.aPeriodType.MONTHLY;
            }

            else
            {
                resolution = aStock.aPeriodType.WEEKLY;
            }

            DateTime start = dpFromDate.Value;
            DateTime end = dpToDate.Value;

            aStock toSend = null;
            
            try
            {
                try
                {
                    tsslMain.Text = "Downloading new stock data...";
                    this.Enabled = false;
                    
                    //Call the magical createStock function.
                    toSend = aStock.createaStock(stock, start, end,resolution);
                }

                catch (WebException ex)
                {
                    MessageBox.Show("The stock you selected could not be found on yahoo!");
                    return;
                }
                finally
                {
                    this.Enabled = true;
                    tsslMain.Text = "New stock information acquired.";
                }
            }

            catch (Exception exTop)
            {
                MessageBox.Show("Something terrible happened \n" + exTop.ToString());
                return;
            }

            if(rbDownload.Checked)
            {
                //Download
                List<string> Stocks = new List<string>();
                List<string> Paths = new List<string>();


                string path = StocksCSVHandler.getPathByResolution(resolution);

                Stocks.Add(stock);
                Paths.Add(path);

                try
                {

                    await StocksCSVHandler.fileDownloader(Paths, Stocks, start, end);

                }

                catch
                {
                    MessageBox.Show("The selected stock/date-time span could not be found at yahoo!");
                }
            }

            else
            {

                aStockForm form = new aStockForm(toSend);
                form.Show();
                
            }
        }
       
    }
}
