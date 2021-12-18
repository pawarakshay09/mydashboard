namespace Hotal_Managment_Syatem
{
    partial class Supplier_Payment_Master
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_chequeDtls = new System.Windows.Forms.Panel();
            this.txt_cheque = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_chk_status = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkboxdt = new System.Windows.Forms.CheckBox();
            this.dTP2 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dTP1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_pay_type = new System.Windows.Forms.ComboBox();
            this.cmb_vendor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_unclear = new System.Windows.Forms.Button();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.txtPaid = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel_chequeDtls.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Location = new System.Drawing.Point(25, 154);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.Size = new System.Drawing.Size(1022, 360);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "Sr.No";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 50;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.button1.Location = new System.Drawing.Point(729, 577);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 42);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_export;
            this.button2.Location = new System.Drawing.Point(416, 576);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 42);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(959, 22);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(60, 19);
            this.linkLabel2.TabIndex = 24;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Refresh";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_chequeDtls);
            this.panel1.Controls.Add(this.chkboxdt);
            this.panel1.Controls.Add(this.dTP2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dTP1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmb_pay_type);
            this.panel1.Controls.Add(this.cmb_vendor);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1035, 94);
            this.panel1.TabIndex = 23;
            // 
            // panel_chequeDtls
            // 
            this.panel_chequeDtls.Controls.Add(this.txt_cheque);
            this.panel_chequeDtls.Controls.Add(this.label3);
            this.panel_chequeDtls.Controls.Add(this.cmb_chk_status);
            this.panel_chequeDtls.Controls.Add(this.label5);
            this.panel_chequeDtls.Location = new System.Drawing.Point(552, 12);
            this.panel_chequeDtls.Name = "panel_chequeDtls";
            this.panel_chequeDtls.Size = new System.Drawing.Size(469, 41);
            this.panel_chequeDtls.TabIndex = 21;
            this.panel_chequeDtls.Visible = false;
            // 
            // txt_cheque
            // 
            this.txt_cheque.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cheque.Location = new System.Drawing.Point(322, 8);
            this.txt_cheque.Name = "txt_cheque";
            this.txt_cheque.Size = new System.Drawing.Size(135, 27);
            this.txt_cheque.TabIndex = 2;
            this.txt_cheque.TextChanged += new System.EventHandler(this.txt_cheque_TextChanged);
            this.txt_cheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cheque_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cheque Status :";
            // 
            // cmb_chk_status
            // 
            this.cmb_chk_status.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_chk_status.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_chk_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_chk_status.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_chk_status.FormattingEnabled = true;
            this.cmb_chk_status.Items.AddRange(new object[] {
            "All",
            "Clear",
            "Unclear"});
            this.cmb_chk_status.Location = new System.Drawing.Point(127, 8);
            this.cmb_chk_status.Name = "cmb_chk_status";
            this.cmb_chk_status.Size = new System.Drawing.Size(96, 27);
            this.cmb_chk_status.TabIndex = 6;
            this.cmb_chk_status.SelectedIndexChanged += new System.EventHandler(this.cmb_chk_status_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(231, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 19);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cheque No :";
            // 
            // chkboxdt
            // 
            this.chkboxdt.AutoSize = true;
            this.chkboxdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkboxdt.Location = new System.Drawing.Point(9, 63);
            this.chkboxdt.Name = "chkboxdt";
            this.chkboxdt.Size = new System.Drawing.Size(15, 14);
            this.chkboxdt.TabIndex = 20;
            this.chkboxdt.UseVisualStyleBackColor = true;
            this.chkboxdt.CheckedChanged += new System.EventHandler(this.chkboxdt_CheckedChanged);
            // 
            // dTP2
            // 
            this.dTP2.Enabled = false;
            this.dTP2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTP2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTP2.Location = new System.Drawing.Point(277, 57);
            this.dTP2.Name = "dTP2";
            this.dTP2.Size = new System.Drawing.Size(114, 27);
            this.dTP2.TabIndex = 11;
            this.dTP2.ValueChanged += new System.EventHandler(this.dTP2_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(242, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 19);
            this.label7.TabIndex = 10;
            this.label7.Text = "To :";
            // 
            // dTP1
            // 
            this.dTP1.Enabled = false;
            this.dTP1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTP1.Location = new System.Drawing.Point(119, 57);
            this.dTP1.Name = "dTP1";
            this.dTP1.Size = new System.Drawing.Size(120, 27);
            this.dTP1.TabIndex = 9;
            this.dTP1.ValueChanged += new System.EventHandler(this.dTP1_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(32, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 19);
            this.label6.TabIndex = 8;
            this.label6.Text = "Date From :";
            // 
            // cmb_pay_type
            // 
            this.cmb_pay_type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_pay_type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_pay_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pay_type.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_pay_type.FormattingEnabled = true;
            this.cmb_pay_type.Items.AddRange(new object[] {
            "All",
            "Cash ",
            "Cheque"});
            this.cmb_pay_type.Location = new System.Drawing.Point(451, 20);
            this.cmb_pay_type.Name = "cmb_pay_type";
            this.cmb_pay_type.Size = new System.Drawing.Size(88, 27);
            this.cmb_pay_type.TabIndex = 5;
            this.cmb_pay_type.SelectedIndexChanged += new System.EventHandler(this.cmb_pay_type_SelectedIndexChanged);
            // 
            // cmb_vendor
            // 
            this.cmb_vendor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_vendor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_vendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_vendor.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_vendor.FormattingEnabled = true;
            this.cmb_vendor.Location = new System.Drawing.Point(116, 20);
            this.cmb_vendor.Name = "cmb_vendor";
            this.cmb_vendor.Size = new System.Drawing.Size(220, 27);
            this.cmb_vendor.Sorted = true;
            this.cmb_vendor.TabIndex = 2;
            this.cmb_vendor.SelectedIndexChanged += new System.EventHandler(this.cmb_vendor_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(342, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Payment Type :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Supplier Name :";
            // 
            // btn_delete
            // 
            this.btn_delete.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_delete;
            this.btn_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(272, 576);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(125, 42);
            this.btn_delete.TabIndex = 25;
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.ForeColor = System.Drawing.Color.Maroon;
            this.btn_clear.Location = new System.Drawing.Point(566, 576);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(141, 42);
            this.btn_clear.TabIndex = 35;
            this.btn_clear.Text = "Clear Cheque";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_unclear
            // 
            this.btn_unclear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_unclear.ForeColor = System.Drawing.Color.Maroon;
            this.btn_unclear.Location = new System.Drawing.Point(556, 576);
            this.btn_unclear.Name = "btn_unclear";
            this.btn_unclear.Size = new System.Drawing.Size(149, 42);
            this.btn_unclear.TabIndex = 36;
            this.btn_unclear.Text = "Unclear Cheque";
            this.btn_unclear.UseVisualStyleBackColor = true;
            this.btn_unclear.Visible = false;
            this.btn_unclear.Click += new System.EventHandler(this.btn_unclear_Click);
            // 
            // txtBalance
            // 
            this.txtBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalance.Location = new System.Drawing.Point(68, 542);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(98, 23);
            this.txtBalance.TabIndex = 66;
            this.txtBalance.Visible = false;
            // 
            // txtPaid
            // 
            this.txtPaid.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaid.ForeColor = System.Drawing.Color.Navy;
            this.txtPaid.Location = new System.Drawing.Point(470, 531);
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.ReadOnly = true;
            this.txtPaid.Size = new System.Drawing.Size(113, 27);
            this.txtPaid.TabIndex = 65;
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(54, 531);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(97, 23);
            this.txtTotal.TabIndex = 64;
            this.txtTotal.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(411, 534);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 19);
            this.label8.TabIndex = 67;
            this.label8.Text = "Total :";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(176, 29);
            this.label9.TabIndex = 94;
            this.label9.Text = "Payment Report";
            // 
            // Supplier_Payment_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(1066, 627);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.txtPaid);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_unclear);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Supplier_Payment_Master";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier_Payment_Master";
            this.Load += new System.EventHandler(this.Supplier_Payment_Master_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_chequeDtls.ResumeLayout(false);
            this.panel_chequeDtls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_chequeDtls;
        private System.Windows.Forms.TextBox txt_cheque;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_chk_status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkboxdt;
        private System.Windows.Forms.DateTimePicker dTP2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dTP1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_pay_type;
        private System.Windows.Forms.ComboBox cmb_vendor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_unclear;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.TextBox txtPaid;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}