namespace Hotal_Managment_Syatem
{
    partial class itemWiseNCReport
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
            this.label22 = new System.Windows.Forms.Label();
            this.dgvMenuReport = new System.Windows.Forms.DataGridView();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rptlistcomb = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuReport)).BeginInit();
            this.SuspendLayout();
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Navy;
            this.label22.Location = new System.Drawing.Point(35, 14);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(125, 29);
            this.label22.TabIndex = 139;
            this.label22.Text = "MIS Report";
            // 
            // dgvMenuReport
            // 
            this.dgvMenuReport.AllowUserToAddRows = false;
            this.dgvMenuReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvMenuReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenuReport.Location = new System.Drawing.Point(40, 119);
            this.dgvMenuReport.Name = "dgvMenuReport";
            this.dgvMenuReport.Size = new System.Drawing.Size(1052, 368);
            this.dgvMenuReport.TabIndex = 131;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(804, 65);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(122, 27);
            this.dtpToDate.TabIndex = 130;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(644, 64);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(115, 27);
            this.dtpFromDate.TabIndex = 129;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(765, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 19);
            this.label2.TabIndex = 128;
            this.label2.Text = "To :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(523, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 19);
            this.label1.TabIndex = 127;
            this.label1.Text = "Date From :";
            // 
            // btn_export
            // 
            this.btn_export.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_export;
            this.btn_export.Location = new System.Drawing.Point(42, 504);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(110, 42);
            this.btn_export.TabIndex = 140;
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_close.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btn_close.Location = new System.Drawing.Point(167, 504);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(110, 42);
            this.btn_close.TabIndex = 132;
            this.btn_close.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(98, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 19);
            this.label3.TabIndex = 141;
            this.label3.Text = "Report List :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // rptlistcomb
            // 
            this.rptlistcomb.FormattingEnabled = true;
            this.rptlistcomb.Location = new System.Drawing.Point(195, 75);
            this.rptlistcomb.Name = "rptlistcomb";
            this.rptlistcomb.Size = new System.Drawing.Size(248, 21);
            this.rptlistcomb.TabIndex = 142;
            this.rptlistcomb.SelectedIndexChanged += new System.EventHandler(this.rptlistcomb_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(976, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 27);
            this.button1.TabIndex = 143;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // itemWiseNCReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 572);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rptlistcomb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_export);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.dgvMenuReport);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "itemWiseNCReport";
            this.Text = "itemWiseNCReport";
            this.Load += new System.EventHandler(this.itemWiseNCReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataGridView dgvMenuReport;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox rptlistcomb;
        private System.Windows.Forms.Button button1;
    }
}