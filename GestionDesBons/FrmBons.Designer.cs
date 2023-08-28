namespace GestionDesBons
{
    partial class FrmBons
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.BonsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new GestionDesBons.DataSet1();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxNumBon = new System.Windows.Forms.ComboBox();
            this.BonsTableAdapter = new GestionDesBons.DataSet1TableAdapters.BonsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.BonsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BonsBindingSource
            // 
            this.BonsBindingSource.DataMember = "Bons";
            this.BonsBindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.reportViewer1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(46, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1210, 623);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bon de soins";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.BonsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GestionDesBons.R_Bons.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 21);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1204, 599);
            this.reportViewer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(546, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selectionner le numero de bon";
            // 
            // cbxNumBon
            // 
            this.cbxNumBon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNumBon.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cbxNumBon.FormattingEnabled = true;
            this.cbxNumBon.Location = new System.Drawing.Point(538, 41);
            this.cbxNumBon.Name = "cbxNumBon";
            this.cbxNumBon.Size = new System.Drawing.Size(242, 26);
            this.cbxNumBon.TabIndex = 3;
            this.cbxNumBon.SelectedIndexChanged += new System.EventHandler(this.cbxNumBon_SelectedIndexChanged);
            // 
            // BonsTableAdapter
            // 
            this.BonsTableAdapter.ClearBeforeFill = true;
            // 
            // FrmBons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 726);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxNumBon);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmBons";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attribuer un bon de soins";
            this.Load += new System.EventHandler(this.FrmBons_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BonsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxNumBon;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource BonsBindingSource;
        private DataSet1 DataSet1;
        private DataSet1TableAdapters.BonsTableAdapter BonsTableAdapter;
    }
}