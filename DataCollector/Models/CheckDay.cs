using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.Models.DataModels;
using Microsoft.Office.Interop.Excel;
namespace DataCollector.Models
{
    internal class CheckDay
    {

        private string dayPath;
        private int year;
        private int month;
        private int day;
        private List<ProductCheck> productChecks;
        public CheckDay(string dayPath, int year, int month, int day)
        {
            this.dayPath = dayPath;
            this.year = year;
            this.month = month;
            this.day = day;
            productChecks = new List<ProductCheck>();
            GetProductChecks();
        }

        private void GetProductChecks()
        {
            Application excelApp = new Application();

            foreach (string item in Directory.GetFiles(dayPath))
            {
                Workbook excelWorkbook = excelApp.Workbooks.Open(item);
                try
                {


                    Sheets excelSheets = excelWorkbook.Worksheets;
                    Worksheet excelWorksheet = (Worksheet)excelSheets.get_Item(1);
                    // var cell = (Range)excelWorksheet.Cells[10, 2];

                    productChecks.Add(new ProductCheck(excelWorksheet,year,month,day));


                }
                catch (Exception)
                {
                    _ = 0;

                }
                finally
                {
                    excelWorkbook.Close();
                }
            }
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

        }

        internal List<ValueDate> GetWeightsByProductCode(string productCode)
        {
            List<ValueDate> weights = new List<ValueDate>();
            var counter = 0;
            foreach (ProductCheck item in productChecks.Where(x => x.ProductCode == productCode))
            {
                weights.AddRange(item.GetWeightsByProductCode(productCode,new DateTime(year,month,day+ counter++)));
            }
            return weights;
        }
    }
}
