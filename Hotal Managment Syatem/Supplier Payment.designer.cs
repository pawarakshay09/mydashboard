namespace Hotal_Managment_Syatem
{
    partial class Purchase_Bill_Payment
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
            this.label10 = new System.Windows.Forms.Label();
            this.txt_paid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_bill = new System.Windows.Forms.TextBox();
            this.rdb_cash = new System.Windows.Forms.RadioButton();
            this.rdb_cheque = new System.Windows.Forms.RadioButton();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.textBox_reference = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_banknm = new System.Windows.Forms.ComboBox();
            this.txt_cheque = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txt_total = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_payment = new System.Windows.Forms.DateTimePicker();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbal_amt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkcol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmb_vendor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_voucherno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(469, 463);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 19);
            this.label10.TabIndex = 23;
            this.label10.Text = "Payment Date :";
            // 
            // txt_paid
            // 
            this.txt_paid.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_paid.Location = new System.Drawing.Point(314, 459);
            this.txt_paid.Name = "txt_paid";
            this.txt_paid.Size = new System.Drawing.Size(133, 27);
            this.txt_paid.TabIndex = 9;
            this.txt_paid.Text = "0";
            this.txt_paid.TextChanged += new System.EventHandler(this.txt_paid_TextChanged);
            this.txt_paid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_paid_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(213, 459);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 19);
            this.label8.TabIndex = 19;
            this.label8.Text = "Paid Amount :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(473, 421);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 19);
            this.label7.TabIndex = 18;
            this.label7.Text = "Selected Bills :";
            // 
            // txt_bill
            // 
            this.txt_bill.BackColor = System.Drawing.Color.White;
            this.txt_bill.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bill.Location = new System.Drawing.Point(578, 419);
            this.txt_bill.Name = "txt_bill";
            this.txt_bill.ReadOnly = true;
            this.txt_bill.Size = new System.Drawing.Size(150, 27);
            this.txt_bill.TabIndex = 8;
            // 
            // rdb_cash
            // 
            this.rdb_cash.AutoSize = true;
            this.rdb_cash.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_cash.Location = new System.Drawing.Point(335, 419);
            this.rdb_cash.Name = "rdb_cash";
            this.rdb_cash.Size = new System.Drawing.Size(119, 23);
            this.rdb_cash.TabIndex = 7;
            this.rdb_cash.Text = "Cash Payment";
            this.rdb_cash.UseVisualStyleBackColor = true;
            // 
            // rdb_cheque
            // 
            this.rdb_cheque.AutoSize = true;
            this.rdb_cheque.Checked = true;
            this.rdb_cheque.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_cheque.Location = new System.Drawing.Point(195, 420);
            this.rdb_cheque.Name = "rdb_cheque";
            this.rdb_cheque.Size = new System.Drawing.Size(136, 23);
            this.rdb_cheque.TabIndex = 6;
            this.rdb_cheque.TabStop = true;
            this.rdb_cheque.Text = "Cheque Payment";
            this.rdb_cheque.UseVisualStyleBackColor = true;
            this.rdb_cheque.CheckedChanged += new System.EventHandler(this.rdb_cheque_CheckedChanged);
            // 
            // btn_clear
            // 
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_clear;
            this.btn_clear.Location = new System.Drawing.Point(392, 556);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(110, 42);
            this.btn_clear.TabIndex = 15;
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btn_close.Location = new System.Drawing.Point(518, 556);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(110, 42);
            this.btn_close.TabIndex = 16;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // textBox_reference
            // 
            this.textBox_reference.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_reference.Location = new System.Drawing.Point(315, 497);
            this.textBox_reference.Multiline = true;
            this.textBox_reference.Name = "textBox_reference";
            this.textBox_reference.Size = new System.Drawing.Size(184, 24);
            this.textBox_reference.TabIndex = 13;
            this.textBox_reference.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(227, 499);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 19);
            this.label12.TabIndex = 33;
            this.label12.Text = "Reference :";
            this.label12.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.cmb_banknm);
            this.panel2.Controls.Add(this.txt_cheque);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(203, 486);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 45);
            this.panel2.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 19);
            this.label9.TabIndex = 21;
            this.label9.Text = "Cheque No :";
            // 
            // cmb_banknm
            // 
            this.cmb_banknm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_banknm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_banknm.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_banknm.FormattingEnabled = true;
            this.cmb_banknm.Location = new System.Drawing.Point(379, 11);
            this.cmb_banknm.Name = "cmb_banknm";
            this.cmb_banknm.Size = new System.Drawing.Size(179, 27);
            this.cmb_banknm.TabIndex = 12;
            this.cmb_banknm.Text = "0";
            // 
            // txt_cheque
            // 
            this.txt_cheque.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cheque.Location = new System.Drawing.Point(117, 11);
            this.txt_cheque.Name = "txt_cheque";
            this.txt_cheque.Size = new System.Drawing.Size(133, 27);
            this.txt_cheque.TabIndex = 11;
            this.txt_cheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cheque_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(286, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 19);
            this.label11.TabIndex = 25;
            this.label11.Text = "Bank Name :";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_save;
            this.btnSave.Location = new System.Drawing.Point(267, 556);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 42);
            this.btnSave.TabIndex = 14;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txt_total
            // 
            this.txt_total.BackColor = System.Drawing.Color.White;
            this.txt_total.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.ForeColor = System.Drawing.Color.BlueViolet;
            this.txt_total.Location = new System.Drawing.Point(676, 384);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(111, 27);
            this.txt_total.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtp_payment);
            this.panel1.Controls.Add(this.textBox_reference);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.linkLabel2);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txt_paid);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txt_bill);
            this.panel1.Controls.Add(this.rdb_cash);
            this.panel1.Controls.Add(this.rdb_cheque);
            this.panel1.Controls.Add(this.txt_total);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtbal_amt);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.cmb_vendor);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_voucherno);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(915, 534);
            this.panel1.TabIndex = 36;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 29);
            this.label3.TabIndex = 42;
            this.label3.Text = "Supplier Payment";
            // 
            // dtp_payment
            // 
            this.dtp_payment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_payment.Location = new System.Drawing.Point(578, 459);
            this.dtp_payment.Name = "dtp_payment";
            this.dtp_payment.Size = new System.Drawing.Size(156, 27);
            this.dtp_payment.TabIndex = 41;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(276, 75);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(67, 18);
            this.linkLabel2.TabIndex = 40;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Refresh";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(569, 387);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Total Amount :";
            // 
            // txtbal_amt
            // 
            this.txtbal_amt.BackColor = System.Drawing.Color.White;
            this.txtbal_amt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbal_amt.Location = new System.Drawing.Point(181, 381);
            this.txtbal_amt.Name = "txtbal_amt";
            this.txtbal_amt.ReadOnly = true;
            this.txtbal_amt.Size = new System.Drawing.Size(111, 27);
            this.txtbal_amt.TabIndex = 4;
            this.txtbal_amt.TextChanged += new System.EventHandler(this.txtbal_amt_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 383);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Selected Total Amount :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkcol});
            this.dataGridView1.Location = new System.Drawing.Point(17, 159);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.Size = new System.Drawing.Size(888, 209);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // chkcol
            // 
            this.chkcol.HeaderText = "Select";
            this.chkcol.Name = "chkcol";
            this.chkcol.Width = 60;
            // 
            // cmb_vendor
            // 
            this.cmb_vendor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_vendor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_vendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_vendor.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_vendor.FormattingEnabled = true;
            this.cmb_vendor.Location = new System.Drawing.Point(121, 114);
            this.cmb_vendor.Name = "cmb_vendor";
            this.cmb_vendor.Size = new System.Drawing.Size(247, 27);
            this.cmb_vendor.Sorted = true;
            this.cmb_vendor.TabIndex = 2;
            this.cmb_vendor.SelectedIndexChanged += new System.EventHandler(this.cmb_vendor_SelectedIndexChanged);
            this.cmb_vendor.TextChanged += new System.EventHandler(this.cmb_vendor_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Supplier Name :";
            // 
            // txt_voucherno
            // 
            this.txt_voucherno.BackColor = System.Drawing.Color.White;
            this.txt_voucherno.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_voucherno.Location = new System.Drawing.Point(121, 76);
            this.txt_voucherno.Name = "txt_voucherno";
            this.txt_voucherno.ReadOnly = true;
            this.txt_voucherno.Size = new System.Drawing.Size(126, 27);
            this.txt_voucherno.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Voucher No :";
            // 
            // Purchase_Bill_Payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(929, 606);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Name = "Purchase_Bill_Payment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier Payment";
            this.Load += new System.EventHandler(this.Purchase_Bill_Payment_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_paid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_bill;
        private System.Windows.Forms.RadioButton rdb_cash;
        private System.Windows.Forms.RadioButton rdb_cheque;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox textBox_reference;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_banknm;
        private System.Windows.Forms.TextBox txt_cheque;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtbal_amt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmb_vendor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_voucherno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.DateTimePicker dtp_payment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkcol;
        private System.Windows.Forms.Label label3;
    }
}