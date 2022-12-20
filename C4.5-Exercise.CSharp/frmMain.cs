using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using OfficeOpenXml;

namespace C4._5_Exercise.CSharp
{
    public partial class frmMain : Form
    {
        private List<TypeComboModel> comboboxDataSource;
        private List<InfoModel> infoModel;
        private List<AllDataModel> allData;
        public frmMain()
        {
            InitializeComponent();
            lblFileAddress.Text = null;
            cmbMethod.SelectedIndex = 0;
            infoModel = new List<InfoModel>();
            comboboxDataSource = new List<TypeComboModel>
            {
                new TypeComboModel
                {
                    Title = "Categorial",
                    Value = DataType.Categorial
                },
                new TypeComboModel
                {
                    Title = "Numerical",
                    Value = DataType.Numerical
                },
                new TypeComboModel
                {
                    Title = "Exclude",
                    Value = DataType.Excluded
                }
            };
            var typeColumn = dgInfoModel.Columns[1] as DataGridViewComboBoxColumn;
            typeColumn.DataSource = comboboxDataSource;
            typeColumn.DisplayMember = "Title";
            typeColumn.ValueMember = "Value";
            allData = new List<AllDataModel>();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            lblFileAddress.Text = openFileDialog1.FileName;
             

            using var package = new ExcelPackage(openFileDialog1.FileName);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.First();

            infoModel.Clear();

            var noOfCol = workSheet.Dimension.End.Column;
            for (var columnIterator = 1; columnIterator <= noOfCol; columnIterator++)
            {
                var info = new InfoModel
                {
                    ColumnNumber = columnIterator,
                    ColumnTitle = workSheet.Cells[1, columnIterator].Value?.ToString(),
                    IsTarget = false
                };

                var row2Value = workSheet.Cells[2, columnIterator].Value?.ToString();
                var row3Value = workSheet.Cells[3, columnIterator].Value?.ToString();

                if (string.IsNullOrEmpty(row2Value) && string.IsNullOrEmpty(row3Value))
                {
                    MessageBox.Show($"2nd and 3rd row of col {columnIterator} are both null!");
                    return;
                }

                var value = Convert.ToString(string.IsNullOrEmpty(row2Value) ? row3Value : row2Value).Trim();
                if (double.TryParse(value, out _))
                {
                    info.DataTypeValue = DataType.Numerical;
                }
                else
                {
                    info.DataTypeValue = DataType.Categorial;
                }

                this.infoModel.Add(info);
            }

            dgInfoModel.DataSource = infoModel;
        }

        private void btnCreateTree_Click(object sender, EventArgs e)
        {
            if (infoModel.Count(x => x.IsTarget) != 1)
            {
                MessageBox.Show("Please select 1 & only 1 target.");
                return;
            }

            if (infoModel.First(x => x.IsTarget).DataTypeValue != DataType.Categorial)
            {
                MessageBox.Show("Target column must be Boolean (True or False)");
                return;
            }

            CreateTree();
        }

        private void CreateTree()
        { 
            using var package = new ExcelPackage(openFileDialog1.FileName);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.First();

            var noOfRows = workSheet.Dimension.End.Row;

            allData.Clear();
            foreach (var info in infoModel.Where(x => x.DataTypeValue != DataType.Excluded))
            {
                if (info.DataTypeValue == DataType.Excluded)
                {
                    continue;
                }

                var newData = new AllDataModel
                {
                    MetaData = info,
                    Data = new List<string>()
                };

                for (var rowIterator = 1; rowIterator <= noOfRows; rowIterator++)
                {
                    newData.Data.Add(workSheet.Cells[rowIterator, info.ColumnNumber].Value.ToString());
                }

                allData.Add(newData);
            }

            if (cmbMethod.SelectedIndex == 0)
            {
                CreateId3Tree(allData);
            }
            else
            {
                CreateC45Tree(allData);
            }
        }

        private void CreateId3Tree(List<AllDataModel> allDataModels)
        {
            throw new NotImplementedException();
        }
        private void CreateC45Tree(List<AllDataModel> allDataModels)
        {
            throw new NotImplementedException();
        }

    }
}
