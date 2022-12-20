namespace C4._5_Exercise.CSharp
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFileAddress = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.dgInfoModel = new System.Windows.Forms.DataGridView();
            this.btnCreateTree = new System.Windows.Forms.Button();
            this.tvResultTree = new System.Windows.Forms.TreeView();
            this.lbResultExcpressions = new System.Windows.Forms.ListBox();
            this.clTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clTarget = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgInfoModel)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFileAddress
            // 
            this.lblFileAddress.AutoSize = true;
            this.lblFileAddress.Location = new System.Drawing.Point(93, 16);
            this.lblFileAddress.Name = "lblFileAddress";
            this.lblFileAddress.Size = new System.Drawing.Size(68, 15);
            this.lblFileAddress.TabIndex = 0;
            this.lblFileAddress.Text = "File address";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(12, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xlsx";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "New excel|*.xlsx|Old excel|*.xls";
            // 
            // cmbMethod
            // 
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "ID3",
            "C4.5"});
            this.cmbMethod.Location = new System.Drawing.Point(93, 52);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(121, 23);
            this.cmbMethod.TabIndex = 2;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(12, 55);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(52, 15);
            this.lblMethod.TabIndex = 3;
            this.lblMethod.Text = "Method:";
            // 
            // dgInfoModel
            // 
            this.dgInfoModel.AllowUserToAddRows = false;
            this.dgInfoModel.AllowUserToDeleteRows = false;
            this.dgInfoModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInfoModel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clTitle,
            this.clType,
            this.clTarget});
            this.dgInfoModel.Location = new System.Drawing.Point(12, 81);
            this.dgInfoModel.Name = "dgInfoModel";
            this.dgInfoModel.RowTemplate.Height = 25;
            this.dgInfoModel.Size = new System.Drawing.Size(450, 292);
            this.dgInfoModel.TabIndex = 4;
            // 
            // btnCreateTree
            // 
            this.btnCreateTree.Location = new System.Drawing.Point(12, 539);
            this.btnCreateTree.Name = "btnCreateTree";
            this.btnCreateTree.Size = new System.Drawing.Size(860, 23);
            this.btnCreateTree.TabIndex = 5;
            this.btnCreateTree.Text = "Create Tree";
            this.btnCreateTree.UseVisualStyleBackColor = true;
            this.btnCreateTree.Click += new System.EventHandler(this.btnCreateTree_Click);
            // 
            // tvResultTree
            // 
            this.tvResultTree.Location = new System.Drawing.Point(468, 81);
            this.tvResultTree.Name = "tvResultTree";
            this.tvResultTree.Size = new System.Drawing.Size(404, 292);
            this.tvResultTree.TabIndex = 6;
            // 
            // lbResultExcpressions
            // 
            this.lbResultExcpressions.FormattingEnabled = true;
            this.lbResultExcpressions.ItemHeight = 15;
            this.lbResultExcpressions.Location = new System.Drawing.Point(12, 379);
            this.lbResultExcpressions.Name = "lbResultExcpressions";
            this.lbResultExcpressions.Size = new System.Drawing.Size(860, 154);
            this.lbResultExcpressions.TabIndex = 7;
            // 
            // clTitle
            // 
            this.clTitle.DataPropertyName = "ColumnTitle";
            this.clTitle.HeaderText = "Column";
            this.clTitle.Name = "clTitle";
            this.clTitle.ReadOnly = true;
            this.clTitle.Width = 200;
            // 
            // clType
            // 
            this.clType.DataPropertyName = "DataTypeValue";
            this.clType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.clType.HeaderText = "Type";
            this.clType.Name = "clType";
            // 
            // clTarget
            // 
            this.clTarget.DataPropertyName = "IsTarget";
            this.clTarget.HeaderText = "Is Target";
            this.clTarget.Name = "clTarget";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 569);
            this.Controls.Add(this.lbResultExcpressions);
            this.Controls.Add(this.tvResultTree);
            this.Controls.Add(this.btnCreateTree);
            this.Controls.Add(this.dgInfoModel);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblFileAddress);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgInfoModel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblFileAddress;
        private Button btnBrowse;
        private OpenFileDialog openFileDialog1;
        private ComboBox cmbMethod;
        private Label lblMethod;
        private DataGridView dgInfoModel;
        private Button btnCreateTree;
        private TreeView tvResultTree;
        private ListBox lbResultExcpressions;
        private DataGridViewTextBoxColumn clTitle;
        private DataGridViewComboBoxColumn clType;
        private DataGridViewCheckBoxColumn clTarget;
    }
}