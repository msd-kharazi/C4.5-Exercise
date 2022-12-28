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
        private List<string> targetAcceptableValues = new List<string>{"true","false"};
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
                    Title = "Include",
                    Value = DataType.Include
                }, 
                new TypeComboModel
                {
                    Title = "Exclude",
                    Value = DataType.Exclude
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
                    DataTypeValue = DataType.Include,
                    IsTarget = false
                }; 

                this.infoModel.Add(info);
            }

            dgInfoModel.DataSource = infoModel;
            dgInfoModel.Refresh();
        }

        private void btnCreateTree_Click(object sender, EventArgs e)
        {
            if (infoModel.Count(x => x.IsTarget) != 1)
            {
                MessageBox.Show("Please select 1 & only 1 target.");
                return;
            }

            CreateTree();
        }

        private void CreateTree()
        { 
            using var package = new ExcelPackage(openFileDialog1.FileName);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            lbResultExcpressions.Items.Clear();
            tvResultTree.Nodes.Clear();

            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.First();

            var noOfRows = workSheet.Dimension.End.Row;

            allData.Clear();
            foreach (var info in infoModel.Where(x => x.DataTypeValue != DataType.Exclude))
            { 
                var newData = new AllDataModel
                {
                    MetaData = info,
                    Data = new List<string>()
                };

                for (var rowIterator = 2; rowIterator <= noOfRows; rowIterator++)
                {
                    var value = workSheet.Cells[rowIterator, info.ColumnNumber].Value.ToString().Trim().ToLower();
                    if (info.IsTarget && !targetAcceptableValues.Contains(value))
                    {
                        MessageBox.Show($"Target value on row {rowIterator} is not 'true' or 'false'");
                        return;
                    }

                    newData.Data.Add(value);
                }

                allData.Add(newData);
            }

            TreeNode root;


            if (cmbMethod.SelectedIndex == 0)
            {
                root = CreateId3Tree(allData);
            }
            else
            {
                root = CreateC45Tree(allData);
            }

            TraverseTree(root);
            lbResultExcpressions.Items.RemoveAt(lbResultExcpressions.Items.Count-1);
            tvResultTree.Nodes.Add(root);
        }


        private TreeNode CreateId3Tree(List<AllDataModel> allDataModels)
        { 
            var targetColumn = allDataModels.First(x => x.MetaData.IsTarget);
            var totalEntropy = CalcEntropy(targetColumn);

            foreach (var column in allDataModels.Where(x=>!x.MetaData.IsTarget))
            {
                column.InformationGain = CalcInformationGain(column, targetColumn, totalEntropy);
            }

            var bestFeature = allDataModels.Where(x => !x.MetaData.IsTarget).MaxBy(x => x.InformationGain);

            var result = new TreeNode($"{bestFeature.MetaData.ColumnTitle} - ({bestFeature.InformationGain})");
            

            result.Tag = bestFeature.MetaData.ColumnTitle;

            if (allDataModels.Count > 2)
            { 
                var allPossibleValues = bestFeature.Data.Distinct().ToList();

                var remainedColumns =
                    allDataModels.Where(x => x != bestFeature).ToList();

                foreach (var possibleValue in allPossibleValues)
                {
                    var possibleValuesIndexes = bestFeature.Data.Select((item, index) => new KeyValuePair<int, string>(index, item))
                        .Where(x => string.Equals(possibleValue, x.Value))
                        .Select(x => x.Key).ToList();

                    var possibleValueAllData = new List<AllDataModel>();
                      
                    foreach (var remainedColumn in remainedColumns)
                    {
                        var remainedColumnClone = new AllDataModel
                        {
                            MetaData = remainedColumn.MetaData,
                            Data = new List<string>()
                        };
                         
                        foreach (var possibleValueIndex in possibleValuesIndexes)
                        {
                            remainedColumnClone.Data.Add(remainedColumn.Data[possibleValueIndex]);
                        }
                        possibleValueAllData.Add(remainedColumnClone);
                    }

                    var possibleValueTargetColumn = possibleValueAllData.First(x => x.MetaData.IsTarget);
                    TreeNode childNode;


                    if (possibleValueTargetColumn.Data.All(x => x.Trim().ToLower() == "true")
                        || possibleValueTargetColumn.Data.All(x => x.Trim().ToLower() == "false"))
                    {
                        childNode = new TreeNode
                        {
                            Text = $"{possibleValue} - {possibleValueTargetColumn.Data.First()}",
                            Tag = $" = {possibleValue}"
                        };
                    }
                    else
                    {
                        childNode = CreateId3Tree(possibleValueAllData);
                        childNode.Text = $"{possibleValue} - {childNode.Text}";
                        childNode.Tag = $" = {possibleValue} AND {childNode.Tag}";
                    }

                     
                    result.Nodes.Add(childNode); 
                }
            }

            return result; 
        }


        private double CalcEntropy(AllDataModel target)
        {
            double allCount = target.Data.Count;
            double allPositive = target.Data.Count(x => string.Equals(x, "true"));
            double allNegative = target.Data.Count(x => string.Equals(x, "false"));

            var positiveDivisionResult = allPositive / allCount;
            var negativeDivisionResult = allNegative / allCount;

            var entropy = -1 * ((positiveDivisionResult ==0?0:(positiveDivisionResult * Math.Log2(positiveDivisionResult))) +
                                (negativeDivisionResult == 0?0:(negativeDivisionResult * Math.Log2(negativeDivisionResult))));

            return entropy;
        }

        private double CalcInformationGain(AllDataModel feature, AllDataModel target, double totalEntropy)
        {
            var allPossibleValues = feature.Data.Distinct().ToList();
            double i = 0;

            foreach (var possibleValue in allPossibleValues)
            {
                var possibleValuesIndexes = feature.Data.Select((item, index) => new KeyValuePair<int, string>(index, item))
                    .Where(x => string.Equals(possibleValue, x.Value))
                    .Select(x => x.Key).ToList();

                double allCount = possibleValuesIndexes.Count;
                double allPositive = target.Data.Where((item, index) => possibleValuesIndexes.Contains(index) && string.Equals(item, "true")).Count();
                double allNegative = target.Data.Where((item, index) => possibleValuesIndexes.Contains(index) && string.Equals(item, "false")).Count();
                var allPositiveRate = allPositive / allCount;
                var allNegativeRate = allNegative / allCount;

                var value = (allCount / feature.Data.Count) * -1 * ((allPositiveRate * (allPositiveRate == 0?0:Math.Log2(allPositiveRate))) + (allNegativeRate * (allNegativeRate == 0 ? 0 : Math.Log2(allNegativeRate))));
                i += value;
            }

            return totalEntropy - i;
        }


        private TreeNode CreateC45Tree(List<AllDataModel> allDataModels)
        {
            var targetColumn = allDataModels.First(x => x.MetaData.IsTarget);
            var totalEntropy = CalcEntropy(targetColumn);

            foreach (var column in allDataModels.Where(x => !x.MetaData.IsTarget))
            {
                column.InformationGain = CalcInformationGain(column, targetColumn, totalEntropy);
                column.SplitInfo = CalcSplitInfo(column);
                column.GainRatio = column.InformationGain / column.SplitInfo;
            }

            var bestFeature = allDataModels.Where(x => !x.MetaData.IsTarget).MaxBy(x => x.GainRatio);

            var result = new TreeNode($"{bestFeature.MetaData.ColumnTitle} - ({bestFeature.GainRatio})");


            result.Tag = bestFeature.MetaData.ColumnTitle;

            if (allDataModels.Count > 2)
            {
                var allPossibleValues = bestFeature.Data.Distinct().ToList();

                var remainedColumns =
                    allDataModels.Where(x => x != bestFeature).ToList();

                foreach (var possibleValue in allPossibleValues)
                {
                    var possibleValuesIndexes = bestFeature.Data.Select((item, index) => new KeyValuePair<int, string>(index, item))
                        .Where(x => string.Equals(possibleValue, x.Value))
                        .Select(x => x.Key).ToList();

                    var possibleValueAllData = new List<AllDataModel>();

                    foreach (var remainedColumn in remainedColumns)
                    {
                        var remainedColumnClone = new AllDataModel
                        {
                            MetaData = remainedColumn.MetaData,
                            Data = new List<string>()
                        };

                        foreach (var possibleValueIndex in possibleValuesIndexes)
                        {
                            remainedColumnClone.Data.Add(remainedColumn.Data[possibleValueIndex]);
                        }
                        possibleValueAllData.Add(remainedColumnClone);
                    }

                    var possibleValueTargetColumn = possibleValueAllData.First(x => x.MetaData.IsTarget);
                    TreeNode childNode;


                    if (possibleValueTargetColumn.Data.All(x => x.Trim().ToLower() == "true")
                        || possibleValueTargetColumn.Data.All(x => x.Trim().ToLower() == "false"))
                    {
                        childNode = new TreeNode
                        {
                            Text = $"{possibleValue} - {possibleValueTargetColumn.Data.First()}",
                            Tag = $" = {possibleValue}"
                        };
                    }
                    else
                    {
                        childNode = CreateC45Tree(possibleValueAllData);
                        childNode.Text = $"{possibleValue} - {childNode.Text}";
                        childNode.Tag = $" = {possibleValue} AND {childNode.Tag}";
                    }


                    result.Nodes.Add(childNode);
                }
            }

            return result;
        }

        private double CalcSplitInfo(AllDataModel feature)
        {
            var allPossibleValues = feature.Data.Distinct().ToList();
            double i = 0;

            foreach (var possibleValue in allPossibleValues)
            {
                var possibleValuesIndexes = feature.Data.Select((item, index) => new KeyValuePair<int, string>(index, item))
                    .Where(x => string.Equals(possibleValue, x.Value))
                    .Select(x => x.Key).ToList();

                double allPossibleValueCount = possibleValuesIndexes.Count;
                double allPossibleValueProbability = allPossibleValueCount/feature.Data.Count;

                var value = -1 * allPossibleValueProbability * (allPossibleValueProbability == 0 ? 0 : Math.Log2(allPossibleValueProbability));
                i += value;
            }

            return i;
        }


        private void TraverseTree(TreeNode node)
        { 
            if (node.Nodes.Count==0 && node.Text.Contains("true"))
            {
                var traverseNode = node;

                string result = null;
                while (traverseNode.Parent != null)
                {
                    result = $"{traverseNode.Tag.ToString()} {result}";
                    traverseNode = traverseNode.Parent;
                }

                result = $"{traverseNode.Tag.ToString()} {result}";
                lbResultExcpressions.Items.Add(result);
                lbResultExcpressions.Items.Add("OR");
            }
            else
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    TraverseTree(childNode);
                }
            } 
        }

    }
}
