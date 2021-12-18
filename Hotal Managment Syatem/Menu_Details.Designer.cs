namespace Hotal_Managment_Syatem
{
    partial class Menu_Details
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DTP1 = new System.Windows.Forms.DateTimePicker();
            this.DTP2 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.lblGrand = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAppliedDisc = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelDiscShow = new System.Windows.Forms.Panel();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelDiscShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date From :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(255, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "To :";
            // 
            // DTP1
            // 
            this.DTP1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTP1.Location = new System.Drawing.Point(134, 59);
            this.DTP1.Name = "DTP1";
            this.DTP1.Size = new System.Drawing.Size(115, 27);
            this.DTP1.TabIndex = 2;
            this.DTP1.ValueChanged += new System.EventHandler(this.DTP1_ValueChanged);
            // 
            // DTP2
            // 
            this.DTP2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTP2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTP2.Location = new System.Drawing.Point(294, 60);
            this.DTP2.Name = "DTP2";
            this.DTP2.Size = new System.Drawing.Size(122, 27);
            this.DTP2.TabIndex = 3;
            this.DTP2.ValueChanged += new System.EventHandler(this.DTP2_ValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 145);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(498, 342);
            this.dataGridView1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(259, 505);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 82;
            this.label4.Text = "Total";
            // 
            // txt_total
            // 
            this.txt_total.BackColor = System.Drawing.Color.White;
            this.txt_total.Location = new System.Drawing.Point(306, 505);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(125, 20);
            this.txt_total.TabIndex = 83;
            // 
            // lblGrand
            // 
            this.lblGrand.AutoSize = true;
            this.lblGrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrand.Location = new System.Drawing.Point(110, 35);
            this.lblGrand.Name = "lblGrand";
            this.lblGrand.Size = new System.Drawing.Size(17, 17);
            this.lblGrand.TabIndex = 87;
            this.lblGrand.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 17);
            this.label8.TabIndex = 86;
            this.label8.Text = "Grand Total :";
            // 
            // lblAppliedDisc
            // 
            this.lblAppliedDisc.AutoSize = true;
            this.lblAppliedDisc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppliedDisc.Location = new System.Drawing.Point(109, 7);
            this.lblAppliedDisc.Name = "lblAppliedDisc";
            this.lblAppliedDisc.Size = new System.Drawing.Size(17, 17);
            this.lblAppliedDisc.TabIndex = 85;
            this.lblAppliedDisc.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 17);
            this.label6.TabIndex = 84;
            this.label6.Text = "Applied Disc :";
            // 
            // cmb_type
            // 
            this.cmb_type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_type.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Items.AddRange(new object[] {
            "All",
            "Menu",
            "Drink"});
            this.cmb_type.Location = new System.Drawing.Point(133, 98);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(174, 27);
            this.cmb_type.TabIndex = 91;
            this.cmb_type.Text = "All";
            this.cmb_type.SelectedIndexChanged += new System.EventHandler(this.cmb_type_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 19);
            this.label5.TabIndex = 90;
            this.label5.Text = "Menu Type :";
            // 
            // panelDiscShow
            // 
            this.panelDiscShow.Controls.Add(this.label6);
            this.panelDiscShow.Controls.Add(this.lblAppliedDisc);
            this.panelDiscShow.Controls.Add(this.label8);
            this.panelDiscShow.Controls.Add(this.lblGrand);
            this.panelDiscShow.Location = new System.Drawing.Point(205, 304);
            this.panelDiscShow.Name = "panelDiscShow";
            this.panelDiscShow.Size = new System.Drawing.Size(200, 57);
            this.panelDiscShow.TabIndex = 92;
            this.panelDiscShow.Visible = false;
            // 
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_print.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_print;
            this.btn_print.Location = new System.Drawing.Point(140, 559);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(110, 42);
            this.btn_print.TabIndex = 80;
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_close.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btn_close.Location = new System.Drawing.Point(279, 559);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(110, 42);
            this.btn_close.TabIndex = 79;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Navy;
            this.label22.Location = new System.Drawing.Point(16, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(199, 29);
            this.label22.TabIndex = 126;
            this.label22.Text = "Sales Item  Report ";
            // 
            // Menu_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(537, 613);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.panelDiscShow);
            this.Controls.Add(this.cmb_type);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.DTP2);
            this.Controls.Add(this.DTP1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Menu_Details";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Item Report";
            this.Load += new System.EventHandler(this.Menu_Details_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelDiscShow.ResumeLayout(false);
            this.panelDiscShow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DTP1;
        private System.Windows.Forms.DateTimePicker DTP2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Label lblGrand;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAppliedDisc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_type;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelDiscShow;
        private System.Windows.Forms.Label label22;
    }
}