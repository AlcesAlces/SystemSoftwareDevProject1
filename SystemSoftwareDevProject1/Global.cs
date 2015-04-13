﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemSoftwareDevProject1.HelperFunctions;

namespace SystemSoftwareDevProject1
{
    static class Global
    {
        public static string dailyDirectory = @"\daily";
        public static DataTable companyList = CSVParse.CsvToDataSet();
    }
}