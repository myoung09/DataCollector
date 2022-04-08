using DataCollector.Models.DataModels;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    internal class ProductCheck
    {
        string productCode;
        DateTime date;
        string checkedBy;
        double weightRangeMin, weightRangeMax;
        double diameterRangeMin, diameterRangeMax;
        List<Check> checks;

        private const int pageShift = 16;
        private  int year;
        private  int month;
        private  int day;

        public ProductCheck(Worksheet excelWorksheet,int year, int month,int day)
        {
            //   Excel.Range usedRange = excelWorkbookWorksheet.UsedRange;
            // var cellValue = (string)(excelWorksheet.Range[2, 3] as Excel.Range).Value;
          

            productCode = (string)(excelWorksheet.Range["D6:G6"].Cells[1,1]).Value;
            date = Convert.ToDateTime((excelWorksheet.Range["C7:F7"].Cells[1, 1]).Value);
            checkedBy = (string)(excelWorksheet.Range["D8:F8"].Cells[1, 1]).Value;
            weightRangeMin = Convert.ToDouble((excelWorksheet.Range["J8"].Cells[1, 1]).Value);
            weightRangeMax = Convert.ToDouble((excelWorksheet.Range["K8"].Cells[1, 1]).Value);
            diameterRangeMin = Convert.ToDouble((excelWorksheet.Range["M8:N8"].Cells[1, 1]).Value);
            diameterRangeMax = Convert.ToDouble((excelWorksheet.Range["O8"].Cells[1, 1]).Value);
            
          

            checks = new List<Check>();
            for (int i = 1; i < 4; i++)
            {

                checks.Add(new Check(excelWorksheet, $"H{11 + i*pageShift}", $"K{11 + i * pageShift}", $"H{14 + i * pageShift}:H{23 + i * pageShift}", $"J{14 + i * pageShift}:J{23 + i * pageShift}", $"K{14 + i * pageShift}:K{23 + i * pageShift}", $"A{11 + i * pageShift}:E{12 + i * pageShift}",
                     $"A{14 + i * pageShift}:E{15 + i * pageShift}", $"M11{11 + i * pageShift}:N{18 + i * pageShift}", $"M{20 + i * pageShift}:N{24 + i * pageShift}",i)); 
            }

            this.year = year;
            this.month = month;
            this.day = day;
        }
#if DEBUG
        internal IEnumerable<ValueDate> GetWeightsByProductCode(string productCode,DateTime time)
        {
            List<ValueDate> weights = new List<ValueDate>();
            foreach (Check item in checks)
            {
                weights.Add(new ValueDate(item.CheckNumber,"Weight",item.GetAverageWeight(),time,item.ProductCode));
            }
            return weights;
        }
#else
        internal IEnumerable<ValueDate> GetWeightsByProductCode(string productCode)
        {
            List<ValueDate> weights = new List<ValueDate>();
            foreach (Check item in checks)
            {
                weights.Add(new ValueDate(item.CheckNumber,"Weight",item.GetAverageWeight(),new DateTime(year,month,day),item.ProductCode));
            }
            return weights;
        }
#endif

        public string ProductCode { get => productCode; set => productCode = value; }
        public DateTime Date { get => date; set => date = value; }
        public string CheckedBy { get => checkedBy; set => checkedBy = value; }
        public double WeightRangeMin { get => weightRangeMin; set => weightRangeMin = value; }
        public double WeightRangeMax { get => weightRangeMax; set => weightRangeMax = value; }
        public double DiameterRangeMin { get => diameterRangeMin; set => diameterRangeMin = value; }
        public double DiameterRangeMax { get => diameterRangeMax; set => diameterRangeMax = value; }
        internal List<Check> Checks { get => checks; set => checks = value; }
        public int Year { get => year; set => year = value; }
        public int Month { get => month; set => month = value; }
        public int Day { get => day; set => day = value; }
    }
}
