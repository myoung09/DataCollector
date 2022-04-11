using DataCollector.Models;
using DataCollector.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataCollector
{
    public partial class Form1 : Form
    {
        Timer timer;
        long tiks;
        string rootFolderPath = @"C:\Users\michael.young\Documents\Taxes\Quality Checks";
        CheckYear checkCollection;

        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1000; // 1 second;
            timer.Enabled = false;
            this.FormClosing += Form1_FormClosing;
            ImportFromJSON();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (sender is Timer timer)
            {
                var time = TimeSpan.FromSeconds(tiks++);
                lblTimerDisplay.Text = string.Format("{0} hours {1} min {2} sec", time.Hours,time.Minutes,time.Seconds);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExportToJSON();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog open = new FolderBrowserDialog())
            {
                open.ShowDialog();
                rootFolderPath = open.SelectedPath;
                if (!string.IsNullOrWhiteSpace(rootFolderPath))
                {
                    lblFolderSource.Text = rootFolderPath;
                }
            }
        }

        public void Crashed()
        {
            ExportToJSON();
        }
        private void ExportToJSON()
        {
            if (checkCollection!=null)
            {
                string json = JsonConvert.SerializeObject(checkCollection);
                File.WriteAllText( "temp.txt",json); 
            }

        }
        private void ImportFromJSON()
        {
            if (File.Exists("temp.txt"))
            {
                var json = File.ReadAllText("temp.txt");
                var obj = JsonConvert.DeserializeObject<CheckYear>(json);
                checkCollection = obj;
            }
        }
        private async void btnRun_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            await Task.Run(() =>
            {
                GetYear();
            });
                
            timer.Enabled = false;
        }

        private void GetYear()
        {
            int year = dtpYear.Value.Year;
            string archivePath = rootFolderPath + @"\~Archive";
            if (Directory.Exists(archivePath))
            {
                string yearPath = archivePath + "\\" + year.ToString();
                if (Directory.Exists(yearPath))
                {
                    checkCollection = new CheckYear(yearPath, year);
                }
            }

        }

        private List<ValueDate> GetWeightsByProductCode(string productCode)
        {
            return checkCollection.GetWeightsByProductCode(productCode);
        }

        private void btnShowWeights_Click(object sender, EventArgs e)
        {
            if (checkCollection != null && !string.IsNullOrWhiteSpace(txtProductCode.Text))
            {
                var list = GetWeightsByProductCode(txtProductCode.Text);
                if (list != null && list.Count > 0)
                {

                    ChartForm form = new ChartForm(list);
                    try
                    {
                        form.Show();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
