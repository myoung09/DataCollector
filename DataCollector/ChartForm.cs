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
            chartControl.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartControl.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chartControl.ChartAreas[1].AxisX.ScaleView.Zoomable = true;
            chartControl.ChartAreas[1].AxisY.ScaleView.Zoomable = true;
            SplineChartExample();
            chartControl.MouseWheel += ChartControl_MouseWheel;
        }

        private void ChartControl_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            for (int i = 0; i < chart.ChartAreas.Count; i++)
            {
                
                var xAxis = chart.ChartAreas[i].AxisX;
                var yAxis = chart.ChartAreas[i].AxisY;



                try
                {

                    if (e.Delta < 0) // Scrolled down.
                    {
                        xAxis.ScaleView.ZoomReset();
                        yAxis.ScaleView.ZoomReset();
                    }
                    else if (e.Delta > 0) // Scrolled up.
                    {
                        var xMin = xAxis.ScaleView.ViewMinimum;
                        var xMax = xAxis.ScaleView.ViewMaximum;
                        var yMin = yAxis.ScaleView.ViewMinimum;
                        var yMax = yAxis.ScaleView.ViewMaximum;

                        var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                        var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                        var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                        var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                        xAxis.ScaleView.Zoom(posXStart, posXFinish);
                        yAxis.ScaleView.Zoom(posYStart, posYFinish);
                    }

                }
                catch { }
            }
        }

        private void SplineChartExample()
        {



            var min = values.Average(x => x.Min);
            var max = values.Average(x => x.Max);


            this.chartControl.Series.Clear();
            chartControl.ChartAreas[0].AxisY.Maximum = max + (max - min);
            chartControl.ChartAreas[0].AxisY.Minimum = min - (max - min);

            chartControl.ChartAreas[1].AxisY.Maximum = 23;
            chartControl.ChartAreas[1].AxisY.Minimum = 0;

            this.chartControl.Titles.Add("Checks");
            if (values != null && values.Count() > 0)
            {
                Series check1 = this.chartControl.Series.Add("Check");
                check1.ChartArea = "ChartArea1";
                check1.ChartType = SeriesChartType.Spline;

                Series time = this.chartControl.Series.Add("time");
                time.ChartArea = "ChartArea2";
                time.ChartType = SeriesChartType.Spline;
                foreach (var item in values.OrderBy(x => x.CheckTime))
                {
                    if (item.Value > 0)
                    {
                        check1.Points.AddXY(item.CheckTime, item.Value);
                        time.Points.AddXY(item.CheckTime, item.CheckTime.Hour);
                    }
                }

            }
            chartControl.Invalidate();

        }
    }
}
