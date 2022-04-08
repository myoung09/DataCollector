using DataCollector.Models.DataModels;
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

namespace DataCollector
{
    public partial class ChartForm : Form
    {
        private List<ValueDate> values;

        public ChartForm(List<ValueDate> values)
        {
            InitializeComponent();
            this.values = values;
            SplineChartExample();
        }

        private void SplineChartExample()
        {
            var check1List = values.Where(x => x.CheckNumber == 1);
            var check2List = values.Where(x => x.CheckNumber == 2);
            var check3List = values.Where(x => x.CheckNumber == 3);

            this.chartControl.Series.Clear();
            chartControl.ChartAreas[0].AxisY.Maximum = 8;
            chartControl.ChartAreas[0].AxisY.Minimum = 7;

            this.chartControl.Titles.Add("Checks");
            if (check1List != null && check1List.Count() > 0)
            {
                Series check1 = this.chartControl.Series.Add("Check 1");
                check1.ChartType = SeriesChartType.Spline;
                check1.IsXValueIndexed = true;
                foreach (var item in check1List)
                {
                    check1.Points.AddXY(item.CheckTime, item.Value);
                }

            }
            if (check2List != null && check2List.Count() > 0)
            {
                Series check2 = this.chartControl.Series.Add("Check 2");
                check2.ChartType = SeriesChartType.Spline;
                check2.IsXValueIndexed = true;
                foreach (var item in check2List)
                {
                    check2.Points.AddXY(item.CheckTime, item.Value);
                }
            }
            if (check3List != null && check3List.Count() > 0)
            {
                Series check3 = this.chartControl.Series.Add("Check 3");
                check3.ChartType = SeriesChartType.Spline;
                check3.IsXValueIndexed = true;
                foreach (var item in check3List)
                {
                    check3.Points.AddXY(item.CheckTime, item.Value);
                }
            }
            chartControl.Invalidate();

        }
    }
}
