using DataCollector.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    internal class CheckYear
    {
        string yearFolderPath;
        int year;
        List<CheckMonth> checkMonths;

        public CheckYear(string yearFolderPath, int year)
        {
            this.yearFolderPath = yearFolderPath;
            this.year = year;
            checkMonths = new List<CheckMonth>();
            GetMonths();
        }

        public string YearFolderPath { get => yearFolderPath; set => yearFolderPath = value; }

        private void GetMonths()
        {
            for (int i = 1; i <= 12; i++)
            {
                string month = i < 10 ? "0" + i : i.ToString();
                string monthPath = yearFolderPath + $@"\{month}-{year}";
                if (Directory.Exists(monthPath))
                {
                    checkMonths.Add(new CheckMonth(monthPath, year, i));
                }
            }

        }

        internal List<ValueDate> GetWeightsByProductCode(string productCode)
        {
            List<ValueDate> weights = new List<ValueDate>();
            foreach (CheckMonth item in checkMonths)
            {
                weights.AddRange(item.GetWeightsByProductCode(productCode));
            }
            return weights;
        }
    }
}
