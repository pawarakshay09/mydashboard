namespace Hotal_Managment_Syatem
{
    partial class Profit_Loss_Report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Profit_Loss_Report));
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewPurchaseDtls = new System.Windows.Forms.DataGridView();
            this.txt_purchaseamt = new System.Windows.Forms.TextBox();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_salesrate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_profit = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView_salesDtls = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridViewExpencessDtls = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_expencess = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.txt_stkamt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurchaseDtls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_salesDtls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpencessDtls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(144, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date :";
            // 
            // dtp_from
            // 
            this.dtp_from.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_from.Location = new System.Drawing.Point(226, 63);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(128, 27);
            this.dtp_from.TabIndex = 1;
            this.dtp_from.ValueChanged += new System.EventHandler(this.dtp_from_ValueChanged);
            // 
            // dataGridViewPurchaseDtls
            // 
            this.dataGridViewPurchaseDtls.AllowUserToAddRows = false;
            this.dataGridViewPurchaseDtls.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPurchaseDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPurchaseDtls.Location = new System.Drawing.Point(6, 177);
            this.dataGridViewPurchaseDtls.Name = "dataGridViewPurchaseDtls";
            this.dataGridViewPurchaseDtls.Size = new System.Drawing.Size(372, 264);
            this.dataGridViewPurchaseDtls.TabIndex = 2;
            this.dataGridViewPurchaseDtls.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // txt_purchaseamt
            // 
            this.txt_purchaseamt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_purchaseamt.Location = new System.Drawing.Point(521, 113);
            this.txt_purchaseamt.Name = "txt_purchaseamt";
            this.txt_purchaseamt.ReadOnly = true;
            this.txt_purchaseamt.Size = new System.Drawing.Size(134, 27);
            this.txt_purchaseamt.TabIndex = 3;
            this.txt_purchaseamt.Text = "0";
            // 
            // dtp_to
            // 
            this.dtp_to.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(398, 62);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(133, 27);
            this.dtp_to.TabIndex = 5;
            this.dtp_to.ValueChanged += new System.EventHandler(this.dtp_to_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(360, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(351, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Total Purchase Amount :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(21, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Total Sales Amount :";
            // 
            // txt_salesrate
            // 
            this.txt_salesrate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_salesrate.Location = new System.Drawing.Point(168, 112);
            this.txt_salesrate.Name = "txt_salesrate";
            this.txt_salesrate.ReadOnly = true;
            this.txt_salesrate.Size = new System.Drawing.Size(138, 27);
            this.txt_salesrate.TabIndex = 7;
            this.txt_salesrate.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(34, 466);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Total Profit Amount :";
            // 
            // txt_profit
            // 
            this.txt_profit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_profit.ForeColor = System.Drawing.Color.Navy;
            this.txt_profit.Location = new System.Drawing.Point(186, 462);
            this.txt_profit.Name = "txt_profit";
            this.txt_profit.ReadOnly = true;
            this.txt_profit.Size = new System.Drawing.Size(156, 27);
            this.txt_profit.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_show;
            this.button1.Location = new System.Drawing.Point(979, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 42);
            this.button1.TabIndex = 12;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.button2.Location = new System.Drawing.Point(599, 486);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 42);
            this.button2.TabIndex = 13;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView_salesDtls
            // 
            this.dataGridView_salesDtls.AllowUserToAddRows = false;
            this.dataGridView_salesDtls.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_salesDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_salesDtls.Location = new System.Drawing.Point(387, 177);
            this.dataGridView_salesDtls.Name = "dataGridView_salesDtls";
            this.dataGridView_salesDtls.Size = new System.Drawing.Size(381, 264);
            this.dataGridView_salesDtls.TabIndex = 14;
            this.dataGridView_salesDtls.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.BlueViolet;
            this.label7.Location = new System.Drawing.Point(7, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 19);
            this.label7.TabIndex = 15;
            this.label7.Text = "Purchase Details";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.BlueViolet;
            this.label8.Location = new System.Drawing.Point(387, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 19);
            this.label8.TabIndex = 16;
            this.label8.Text = "Sales Details";
            // 
            // button3
            // 
            this.button3.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_export;
            this.button3.Location = new System.Drawing.Point(470, 486);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 42);
            this.button3.TabIndex = 17;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridViewExpencessDtls
            // 
            this.dataGridViewExpencessDtls.AllowUserToAddRows = false;
            this.dataGridViewExpencessDtls.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewExpencessDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExpencessDtls.Location = new System.Drawing.Point(777, 177);
            this.dataGridViewExpencessDtls.Name = "dataGridViewExpencessDtls";
            this.dataGridViewExpencessDtls.ReadOnly = true;
            this.dataGridViewExpencessDtls.Size = new System.Drawing.Size(358, 264);
            this.dataGridViewExpencessDtls.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(686, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "Total Expences :";
            // 
            // txt_expencess
            // 
            this.txt_expencess.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_expencess.Location = new System.Drawing.Point(801, 114);
            this.txt_expencess.Name = "txt_expencess";
            this.txt_expencess.ReadOnly = true;
            this.txt_expencess.Size = new System.Drawing.Size(150, 27);
            this.txt_expencess.TabIndex = 19;
            this.txt_expencess.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.BlueViolet;
            this.label10.Location = new System.Drawing.Point(780, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 19);
            this.label10.TabIndex = 21;
            this.label10.Text = "Expences Details";
            // 
            // dataGridView4
            // 
            this.dataGridView4.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(824, 492);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(171, 68);
            this.dataGridView4.TabIndex = 22;
            this.dataGridView4.Visible = false;
            // 
            // txt_stkamt
            // 
            this.txt_stkamt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_stkamt.Location = new System.Drawing.Point(799, 27);
            this.txt_stkamt.Name = "txt_stkamt";
            this.txt_stkamt.Size = new System.Drawing.Size(156, 27);
            this.txt_stkamt.TabIndex = 23;
            this.txt_stkamt.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(694, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 19);
            this.label11.TabIndex = 24;
            this.label11.Text = "Stock Amount";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(830, 473);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 16);
            this.label12.TabIndex = 25;
            this.label12.Text = "Stock Details";
            this.label12.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(312, 108);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 29);
            this.label13.TabIndex = 26;
            this.label13.Text = "-";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(660, 112);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 29);
            this.label14.TabIndex = 27;
            this.label14.Text = "+";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(331, 109);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 29);
            this.label15.TabIndex = 28;
            this.label15.Text = "(";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(956, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 29);
            this.label16.TabIndex = 29;
            this.label16.Text = ")";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Navy;
            this.label17.Location = new System.Drawing.Point(12, 18);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(191, 29);
            this.label17.TabIndex = 159;
            this.label17.Text = "Profit Loss Report";
            // 
            // Profit_Loss_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(1143, 551);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_stkamt);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_expencess);
            this.Controls.Add(this.dataGridViewExpencessDtls);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridView_salesDtls);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_profit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_salesrate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtp_to);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_purchaseamt);
            this.Controls.Add(this.dataGridViewPurchaseDtls);
            this.Controls.Add(this.dtp_from);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Profit_Loss_Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profit Loss Report";
            this.Load += new System.EventHandler(this.Profit_Loss_Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurchaseDtls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_salesDtls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpencessDtls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.DataGridView dataGridViewPurchaseDtls;
        private System.Windows.Forms.TextBox txt_purchaseamt;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_salesrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_profit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView_salesDtls;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridViewExpencessDtls;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_expencess;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.TextBox txt_stkamt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}