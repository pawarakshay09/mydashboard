namespace Hotal_Managment_Syatem
{
    partial class Material_Transfer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label39 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.cbtransferfrom = new System.Windows.Forms.ComboBox();
            this.cbdrink = new System.Windows.Forms.CheckBox();
            this.label36 = new System.Windows.Forms.Label();
            this.cbunit = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.paneltransfer = new System.Windows.Forms.Panel();
            this.cbtransferto = new System.Windows.Forms.ComboBox();
            this.txtVoucherNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblGodaunStock = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.Newqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subcategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currentQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTransferQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCounterLimit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShopStock = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGodaunStock = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            this.cmbCounterNm = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictBarcode = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.paneltransfer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBarcode)).BeginInit();
            this.SuspendLayout();
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(7, 81);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(94, 17);
            this.label39.TabIndex = 409;
            this.label39.Text = "SubCategory:";
            // 
            // cbtransferfrom
            // 
            this.cbtransferfrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtransferfrom.FormattingEnabled = true;
            this.cbtransferfrom.Location = new System.Drawing.Point(68, 20);
            this.cbtransferfrom.Name = "cbtransferfrom";
            this.cbtransferfrom.Size = new System.Drawing.Size(198, 24);
            this.cbtransferfrom.TabIndex = 406;
            this.cbtransferfrom.Text = "warehouse";
            // 
            // cbdrink
            // 
            this.cbdrink.AutoSize = true;
            this.cbdrink.Location = new System.Drawing.Point(37, 72);
            this.cbdrink.Name = "cbdrink";
            this.cbdrink.Size = new System.Drawing.Size(96, 17);
            this.cbdrink.TabIndex = 448;
            this.cbdrink.Text = "Drink Products";
            this.cbdrink.UseVisualStyleBackColor = true;
            this.cbdrink.CheckedChanged += new System.EventHandler(this.cbdrink_CheckedChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(-2, 23);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(69, 17);
            this.label36.TabIndex = 408;
            this.label36.Text = "Category:";
            // 
            // cbunit
            // 
            this.cbunit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbunit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbunit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbunit.FormattingEnabled = true;
            this.cbunit.Location = new System.Drawing.Point(359, 137);
            this.cbunit.Name = "cbunit";
            this.cbunit.Size = new System.Drawing.Size(99, 24);
            this.cbunit.TabIndex = 447;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(316, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 17);
            this.label12.TabIndex = 446;
            this.label12.Text = "Unit :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(640, 125);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 20);
            this.checkBox1.TabIndex = 445;
            this.checkBox1.Text = "IsDirectTo";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // paneltransfer
            // 
            this.paneltransfer.Controls.Add(this.label39);
            this.paneltransfer.Controls.Add(this.cbtransferfrom);
            this.paneltransfer.Controls.Add(this.label36);
            this.paneltransfer.Controls.Add(this.cbtransferto);
            this.paneltransfer.Location = new System.Drawing.Point(499, 49);
            this.paneltransfer.Name = "paneltransfer";
            this.paneltransfer.Size = new System.Drawing.Size(286, 69);
            this.paneltransfer.TabIndex = 444;
            this.paneltransfer.Visible = false;
            // 
            // cbtransferto
            // 
            this.cbtransferto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtransferto.FormattingEnabled = true;
            this.cbtransferto.Location = new System.Drawing.Point(107, 78);
            this.cbtransferto.Name = "cbtransferto";
            this.cbtransferto.Size = new System.Drawing.Size(198, 24);
            this.cbtransferto.TabIndex = 407;
            this.cbtransferto.Text = "Counter";
            // 
            // txtVoucherNo
            // 
            this.txtVoucherNo.BackColor = System.Drawing.Color.White;
            this.txtVoucherNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVoucherNo.Location = new System.Drawing.Point(124, 39);
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.ReadOnly = true;
            this.txtVoucherNo.Size = new System.Drawing.Size(77, 23);
            this.txtVoucherNo.TabIndex = 442;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(27, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 17);
            this.label11.TabIndex = 441;
            this.label11.Text = "Voucher No :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Purple;
            this.label10.Location = new System.Drawing.Point(170, 577);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(399, 17);
            this.label10.TabIndex = 440;
            this.label10.Text = " Here you can transfer material for particular counter ";
            // 
            // lblGodaunStock
            // 
            this.lblGodaunStock.AutoSize = true;
            this.lblGodaunStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGodaunStock.Location = new System.Drawing.Point(531, 34);
            this.lblGodaunStock.Name = "lblGodaunStock";
            this.lblGodaunStock.Size = new System.Drawing.Size(94, 17);
            this.lblGodaunStock.TabIndex = 438;
            this.lblGodaunStock.Text = "GodaunStock";
            this.lblGodaunStock.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(475, 8);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(157, 20);
            this.dtpDate.TabIndex = 437;
            // 
            // Newqty
            // 
            this.Newqty.HeaderText = "New Qty";
            this.Newqty.Name = "Newqty";
            this.Newqty.ReadOnly = true;
            this.Newqty.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Purple;
            this.label9.Location = new System.Drawing.Point(113, 577);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 17);
            this.label9.TabIndex = 439;
            this.label9.Text = "Note :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(424, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 17);
            this.label8.TabIndex = 436;
            this.label8.Text = "Date";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Teal;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.Subcategory,
            this.ProductNm,
            this.TransferQty,
            this.currentQty,
            this.Newqty});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.MenuText;
            this.dataGridView1.Location = new System.Drawing.Point(-10, 218);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.Size = new System.Drawing.Size(698, 251);
            this.dataGridView1.TabIndex = 435;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // Subcategory
            // 
            this.Subcategory.HeaderText = "Subcategory";
            this.Subcategory.Name = "Subcategory";
            this.Subcategory.ReadOnly = true;
            // 
            // ProductNm
            // 
            this.ProductNm.HeaderText = "Product Name";
            this.ProductNm.Name = "ProductNm";
            this.ProductNm.ReadOnly = true;
            this.ProductNm.Width = 180;
            // 
            // TransferQty
            // 
            this.TransferQty.HeaderText = "Transfer Qty";
            this.TransferQty.Name = "TransferQty";
            this.TransferQty.ReadOnly = true;
            // 
            // currentQty
            // 
            this.currentQty.HeaderText = "Current Qty";
            this.currentQty.Name = "currentQty";
            this.currentQty.ReadOnly = true;
            this.currentQty.Visible = false;
            // 
            // txtTransferQty
            // 
            this.txtTransferQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransferQty.Location = new System.Drawing.Point(124, 149);
            this.txtTransferQty.Name = "txtTransferQty";
            this.txtTransferQty.Size = new System.Drawing.Size(126, 23);
            this.txtTransferQty.TabIndex = 422;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 17);
            this.label7.TabIndex = 431;
            this.label7.Text = "Transfer Stock :";
            // 
            // txtCounterLimit
            // 
            this.txtCounterLimit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCounterLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounterLimit.Location = new System.Drawing.Point(718, 196);
            this.txtCounterLimit.Name = "txtCounterLimit";
            this.txtCounterLimit.ReadOnly = true;
            this.txtCounterLimit.Size = new System.Drawing.Size(11, 23);
            this.txtCounterLimit.TabIndex = 434;
            this.txtCounterLimit.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(613, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 17);
            this.label6.TabIndex = 428;
            this.label6.Text = "Counter Limit :";
            this.label6.Visible = false;
            // 
            // txtShopStock
            // 
            this.txtShopStock.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtShopStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShopStock.Location = new System.Drawing.Point(713, 178);
            this.txtShopStock.Name = "txtShopStock";
            this.txtShopStock.ReadOnly = true;
            this.txtShopStock.Size = new System.Drawing.Size(37, 23);
            this.txtShopStock.TabIndex = 433;
            this.txtShopStock.Text = "0";
            this.txtShopStock.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(625, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 426;
            this.label5.Text = "Shop Stock :";
            this.label5.Visible = false;
            // 
            // txtGodaunStock
            // 
            this.txtGodaunStock.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtGodaunStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGodaunStock.Location = new System.Drawing.Point(756, 156);
            this.txtGodaunStock.Name = "txtGodaunStock";
            this.txtGodaunStock.ReadOnly = true;
            this.txtGodaunStock.Size = new System.Drawing.Size(10, 23);
            this.txtGodaunStock.TabIndex = 432;
            this.txtGodaunStock.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(644, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 17);
            this.label4.TabIndex = 423;
            this.label4.Text = "Godaun Stock :";
            this.label4.Visible = false;
            // 
            // cmbProductName
            // 
            this.cmbProductName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbProductName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(124, 97);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(196, 24);
            this.cmbProductName.TabIndex = 421;
            this.cmbProductName.SelectedIndexChanged += new System.EventHandler(this.cmbProductName_SelectedIndexChanged);
            // 
            // cmbCounterNm
            // 
            this.cmbCounterNm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCounterNm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCounterNm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCounterNm.FormattingEnabled = true;
            this.cmbCounterNm.Location = new System.Drawing.Point(561, 124);
            this.cmbCounterNm.Name = "cmbCounterNm";
            this.cmbCounterNm.Size = new System.Drawing.Size(71, 24);
            this.cmbCounterNm.TabIndex = 420;
            this.cmbCounterNm.Text = "Admin";
            this.cmbCounterNm.Visible = false;
            this.cmbCounterNm.SelectedIndexChanged += new System.EventHandler(this.cmbCounterNm_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 419;
            this.label3.Text = "Product Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(448, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 418;
            this.label2.Text = "Counter Name :";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 20);
            this.label1.TabIndex = 417;
            this.label1.Text = "Material Transfer to Counter";
            // 
            // pictBarcode
            // 
            this.pictBarcode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictBarcode.Location = new System.Drawing.Point(642, 14);
            this.pictBarcode.Name = "pictBarcode";
            this.pictBarcode.Size = new System.Drawing.Size(147, 35);
            this.pictBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictBarcode.TabIndex = 443;
            this.pictBarcode.TabStop = false;
            this.pictBarcode.Visible = false;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_delete;
            this.button1.Location = new System.Drawing.Point(244, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 40);
            this.button1.TabIndex = 427;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btnClose.Location = new System.Drawing.Point(503, 479);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(113, 40);
            this.btnClose.TabIndex = 430;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_clear;
            this.btnClear.Location = new System.Drawing.Point(371, 479);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(113, 40);
            this.btnClear.TabIndex = 429;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_save;
            this.btnSave.Location = new System.Drawing.Point(115, 479);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 40);
            this.btnSave.TabIndex = 425;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_add;
            this.btnAdd.Location = new System.Drawing.Point(330, 169);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(132, 42);
            this.btnAdd.TabIndex = 424;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Material_Transfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.bck;
            this.ClientSize = new System.Drawing.Size(751, 527);
            this.Controls.Add(this.cbdrink);
            this.Controls.Add(this.cbunit);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.paneltransfer);
            this.Controls.Add(this.pictBarcode);
            this.Controls.Add(this.txtVoucherNo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblGodaunStock);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTransferQty);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCounterLimit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtShopStock);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtGodaunStock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbProductName);
            this.Controls.Add(this.cmbCounterNm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Material_Transfer";
            this.Text = "Material_Transfer";
            this.Load += new System.EventHandler(this.Material_Transfer_Load);
            this.paneltransfer.ResumeLayout(false);
            this.paneltransfer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBarcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label39;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ComboBox cbtransferfrom;
        private System.Windows.Forms.CheckBox cbdrink;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cbunit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel paneltransfer;
        private System.Windows.Forms.ComboBox cbtransferto;
        private System.Windows.Forms.PictureBox pictBarcode;
        private System.Windows.Forms.TextBox txtVoucherNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblGodaunStock;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Newqty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subcategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNm;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn currentQty;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtTransferQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCounterLimit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShopStock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGodaunStock;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbProductName;
        private System.Windows.Forms.ComboBox cmbCounterNm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}