using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    internal class Check
    {
        protected DateTime timeOfCheck;
        protected string huNumber;
        protected List<double> weights;
        protected double minTolWeight;
        protected double maxTolWeight;
        protected List<double> diametersMin;
        protected List<double> diametersMax;
        protected double minTolDiam;
        protected double maxTolDiam;
        protected string productCode;
        protected string lotCode;

        protected List<Defect> defects;
        protected List<Packaging> packaging;
        private int checkNumber;

        public Check(Worksheet excelWorksheet, string timeOfCheck, string huNumber, string weights, string diametersMin, string diametersMax, string productCode, string lotCode, string defects, string packaging, string minTolWeight, string maxTolWeight, string minTolDiam, string maxTolDiam, int checkNumber)
        {
            this.weights = new List<double>();
            this.diametersMin = new List<double>();
            this.diametersMax = new List<double>();
            this.defects = new List<Defect>();
            this.packaging = new List<Packaging>();
            this.checkNumber = checkNumber;


            this.timeOfCheck   = Convert.ToDateTime(excelWorksheet.Range[timeOfCheck].Cells[1,1].Value);
            this.huNumber = Convert.ToDouble((excelWorksheet.Range[huNumber].Cells[1, 1].Value)).ToString();
            //weights
            for (int i = 0; i < excelWorksheet.Range[weights].Rows.Count; i++)
            {
                bool debugMode = false;
                var rand = new Random();

                if (excelWorksheet.Range[weights].Rows[i] is Range rItem)
                {
                    double value = Convert.ToDouble(rItem.Cells[i, 1].Value);
                    this.weights.Add(debugMode ? 7 + rand.NextDouble() : value);
                }

            }
            //diametersMin
            for (int i = 0; i < excelWorksheet.Range[diametersMin].Rows.Count; i++)
            {

                if (excelWorksheet.Range[weights].Rows[i] is Range rItem)
                {
                    double value = Convert.ToDouble(rItem.Cells[i, 1].Value);
                    this.diametersMin.Add(value);
                }
            }
            //diametersMax
            for (int i = 0; i < excelWorksheet.Range[diametersMax].Rows.Count; i++)
            {

                if (excelWorksheet.Range[weights].Rows[i] is Range rItem)
                {
                    double value = Convert.ToDouble(rItem.Cells[i, 1].Value);
                    this.diametersMax.Add(value);
                }
            }

            this.productCode = (string)(excelWorksheet.Range[productCode].Cells[1,1]).Value;
            this.lotCode = (string)(excelWorksheet.Range[lotCode].Cells[1,1]).Value;
             this.minTolWeight = Convert.ToDouble(excelWorksheet.Range[minTolWeight].Cells[1, 1].Value);
            this.maxTolWeight = Convert.ToDouble(excelWorksheet.Range[maxTolWeight].Cells[1, 1].Value);
            this.minTolDiam = Convert.ToDouble(excelWorksheet.Range[minTolDiam].Cells[1, 1].Value);
            this.maxTolDiam = Convert.ToDouble(excelWorksheet.Range[maxTolDiam].Cells[1, 1].Value);
            //defects
            for (int i = 0; i < excelWorksheet.Range[weights].Rows.Count; i++)
            {

                if (excelWorksheet.Range[weights].Rows[i] is Range rItem)
                {
                    string name = "unknown";
                    int value = Convert.ToInt32(rItem.Cells[i, 1].Value);

                    name = (string)(rItem.Cells[i, 2]).Value;
                    this.defects.Add(new Defect(name,value));
                }
            }

            //packaging
            for (int i = 0; i < excelWorksheet.Range[weights].Rows.Count; i++)
            {

                if (excelWorksheet.Range[weights].Rows[i] is Range rItem)
                {
                    string name = "unknown";
                    bool value = Convert.ToBoolean(rItem.Cells[i, 1].Value);

                    name = (string)(rItem.Cells[i,2]).Value;
                    this.packaging.Add(new Packaging(name,value));
                }
            }

        }

        public DateTime TimeOfCheck { get => timeOfCheck; set => timeOfCheck = value; }
        public string HuNumber { get => huNumber; set => huNumber = value; }
        public List<double> Weights { get => weights; set => weights = value; }
        public List<double> DiametersMin { get => diametersMin; set => diametersMin = value; }
        public List<double> DiametersMax { get => diametersMax; set => diametersMax = value; }
        public string ProductCode { get => productCode; set => productCode = value; }
        public string LotCode { get => lotCode; set => lotCode = value; }
        internal List<Defect> Defects { get => defects; set => defects = value; }
        internal List<Packaging> Packaging { get => packaging; set => packaging = value; }
        public int CheckNumber { get => checkNumber; set => checkNumber = value; }
        public double MinTolWeight { get => minTolWeight; set => minTolWeight = value; }
        public double MaxTolWeight { get => maxTolWeight; set => maxTolWeight = value; }
        public double MinTolDiam { get => minTolDiam; set => minTolDiam = value; }
        public double MaxTolDiam { get => maxTolDiam; set => maxTolDiam = value; }

        public double GetAverageWeight()
        {
            int divisor = 10;
            foreach (double item in weights)
            {
                if (item <= 0)
                {
                    divisor--;
                }
            }
            return divisor > 0? weights.Sum() / divisor:0;
        }
        public double GetAverageDiameter()
        {

            return (diametersMin.Sum() + diametersMax.Sum())/20;
        }

    }
}
