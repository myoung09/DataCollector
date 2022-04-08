using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models.DataModels
{
    public class ValueDate
    {
        int checkNumber;
        string valueName;
        double value;
        DateTime checkTime;
        string productCode;

        public ValueDate(int checkNumber, string valueName, double value, DateTime checkTime, string productCode)
        {
            this.checkNumber = checkNumber;
            this.valueName = valueName;
            this.value = value;
            this.checkTime = checkTime;
            this.productCode = productCode;
        }

        public int CheckNumber { get => checkNumber; set => checkNumber = value; }
        public string ValueName { get => valueName; set => valueName = value; }
        public double Value { get => value; set => this.value = value; }
        public DateTime CheckTime { get => checkTime; set => checkTime = value; }
        public string ProductCode { get => productCode; set => productCode = value; }
    }
}
