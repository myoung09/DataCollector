using DataCollector.Models;
using DataCollector.Models.DataModels;
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
        string rootFolderPath = @"C:\Users\michael.young\Documents\Taxes\Quality Checks";
        CheckYear checkCollection;

        public Form1()
        {
            InitializeComponent();
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

        private void btnRun_Click(object sender, EventArgs e)
        {
            GetYear();
            
        }

        private void GetYear()
        {
            int year = dtpYear.Value.Year;
            string archivePath = rootFolderPath + @"\Archive";
            if (Directory.Exists(archivePath))
            {
                string yearPath = archivePath + "\\" + year.ToString();
                if (Directory.Exists(yearPath))
                {
                    checkCollection = new CheckYear(yearPath,year);
                }
            }

        }

        private List<ValueDate> GetWeightsByProductCode(string productCode)
        {
          return checkCollection.GetWeightsByProductCode(productCode);
        }

        private void btnShowWeights_Click(object sender, EventArgs e)
        {
            if (checkCollection != null)
            {
                ChartForm form = new ChartForm(GetWeightsByProductCode("025501-0594"));
                form.Show();
            }
        }
    }
}
