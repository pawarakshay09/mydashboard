namespace Hotal_Managment_Syatem
{
    partial class Purchase_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Purchase_Form));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_v_name = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Qty = new System.Windows.Forms.TextBox();
            this.cmb_unit = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_rate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_amt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_desc = new System.Windows.Forms.TextBox();
            this.txt_invoiceno = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lbl_erormsg = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.txt_material_nm = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.chk_drink = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_amount = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_paid = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_balance = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_rate = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxTax = new System.Windows.Forms.TextBox();
            this.textBox_Discount = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label_grandTotal = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_qtyML = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Invoice Number :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(299, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(264, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Supplier Name :";
            // 
            // cmb_v_name
            // 
            this.cmb_v_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_v_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_v_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_v_name.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_v_name.FormattingEnabled = true;
            this.cmb_v_name.Location = new System.Drawing.Point(376, 78);
            this.cmb_v_name.Name = "cmb_v_name";
            this.cmb_v_name.Size = new System.Drawing.Size(204, 27);
            this.cmb_v_name.TabIndex = 2;
            this.cmb_v_name.SelectedIndexChanged += new System.EventHandler(this.cmb_v_name_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Material Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(313, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 19);
            this.label6.TabIndex = 8;
            this.label6.Text = "Quantity :";
            // 
            // txt_Qty
            // 
            this.txt_Qty.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Qty.Location = new System.Drawing.Point(313, 154);
            this.txt_Qty.Name = "txt_Qty";
            this.txt_Qty.Size = new System.Drawing.Size(62, 27);
            this.txt_Qty.TabIndex = 5;
            this.txt_Qty.Text = "0";
            this.txt_Qty.TextChanged += new System.EventHandler(this.txt_Qty_TextChanged);
            this.txt_Qty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Qty_KeyPress);
            this.txt_Qty.Leave += new System.EventHandler(this.txt_Qty_Leave);
            // 
            // cmb_unit
            // 
            this.cmb_unit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_unit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_unit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_unit.FormattingEnabled = true;
            this.cmb_unit.Location = new System.Drawing.Point(385, 153);
            this.cmb_unit.Name = "cmb_unit";
            this.cmb_unit.Size = new System.Drawing.Size(60, 27);
            this.cmb_unit.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(384, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 19);
            this.label7.TabIndex = 9;
            this.label7.Text = "Unit :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(140, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Rate Per Unit :";
            this.label8.Visible = false;
            // 
            // txt_rate
            // 
            this.txt_rate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_rate.Location = new System.Drawing.Point(262, 256);
            this.txt_rate.Name = "txt_rate";
            this.txt_rate.Size = new System.Drawing.Size(89, 24);
            this.txt_rate.TabIndex = 6;
            this.txt_rate.Text = "0";
            this.txt_rate.Visible = false;
            this.txt_rate.TextChanged += new System.EventHandler(this.txt_rate_TextChanged);
            this.txt_rate.Leave += new System.EventHandler(this.txt_rate_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(514, 425);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 19);
            this.label11.TabIndex = 19;
            this.label11.Text = "Amount :";
            // 
            // txt_amt
            // 
            this.txt_amt.Enabled = false;
            this.txt_amt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amt.Location = new System.Drawing.Point(586, 421);
            this.txt_amt.Name = "txt_amt";
            this.txt_amt.ReadOnly = true;
            this.txt_amt.Size = new System.Drawing.Size(121, 27);
            this.txt_amt.TabIndex = 7;
            this.txt_amt.Text = "0";
            this.txt_amt.TextChanged += new System.EventHandler(this.txt_amt_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(33, 437);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 16);
            this.label9.TabIndex = 63;
            this.label9.Text = "Discription :";
            // 
            // txt_desc
            // 
            this.txt_desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_desc.Location = new System.Drawing.Point(36, 456);
            this.txt_desc.Multiline = true;
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_desc.Size = new System.Drawing.Size(171, 51);
            this.txt_desc.TabIndex = 12;
            // 
            // txt_invoiceno
            // 
            this.txt_invoiceno.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_invoiceno.Location = new System.Drawing.Point(120, 80);
            this.txt_invoiceno.Name = "txt_invoiceno";
            this.txt_invoiceno.Size = new System.Drawing.Size(113, 27);
            this.txt_invoiceno.TabIndex = 1;
            this.txt_invoiceno.TextChanged += new System.EventHandler(this.txt_invoiceno_TextChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lbl_erormsg
            // 
            this.lbl_erormsg.AutoSize = true;
            this.lbl_erormsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_erormsg.ForeColor = System.Drawing.Color.Red;
            this.lbl_erormsg.Location = new System.Drawing.Point(48, 556);
            this.lbl_erormsg.Name = "lbl_erormsg";
            this.lbl_erormsg.Size = new System.Drawing.Size(0, 15);
            this.lbl_erormsg.TabIndex = 68;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(565, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 19);
            this.label12.TabIndex = 69;
            this.label12.Text = "Date :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(614, 40);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(119, 27);
            this.dateTimePicker1.TabIndex = 70;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Qty});
            this.dataGridView1.Location = new System.Drawing.Point(12, 188);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.Size = new System.Drawing.Size(733, 204);
            this.dataGridView1.TabIndex = 18;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Material Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 230;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Quantity";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Unit";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Rate";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Amount";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Qty(ML)";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(271, 276);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 73;
            this.label10.Text = "label10";
            this.label10.Visible = false;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(674, 17);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(59, 19);
            this.linkLabel2.TabIndex = 90;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Refresh";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // txt_material_nm
            // 
            this.txt_material_nm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txt_material_nm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txt_material_nm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txt_material_nm.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_material_nm.FormattingEnabled = true;
            this.txt_material_nm.Location = new System.Drawing.Point(12, 153);
            this.txt_material_nm.Name = "txt_material_nm";
            this.txt_material_nm.Size = new System.Drawing.Size(168, 27);
            this.txt_material_nm.TabIndex = 3;
            this.txt_material_nm.SelectedIndexChanged += new System.EventHandler(this.txt_material_nm_SelectedIndexChanged);
            this.txt_material_nm.TextChanged += new System.EventHandler(this.txt_material_nm_TextChanged);
            this.txt_material_nm.Leave += new System.EventHandler(this.txt_material_nm_Leave);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(423, 130);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(17, 19);
            this.linkLabel1.TabIndex = 26;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "+";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.Location = new System.Drawing.Point(7, 606);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(70, 16);
            this.linkLabel3.TabIndex = 93;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Add New";
            this.linkLabel3.Visible = false;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // chk_drink
            // 
            this.chk_drink.AutoSize = true;
            this.chk_drink.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_drink.Location = new System.Drawing.Point(19, 112);
            this.chk_drink.Name = "chk_drink";
            this.chk_drink.Size = new System.Drawing.Size(62, 23);
            this.chk_drink.TabIndex = 94;
            this.chk_drink.Text = "Drink";
            this.chk_drink.UseVisualStyleBackColor = true;
            this.chk_drink.CheckedChanged += new System.EventHandler(this.chk_drink_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(548, 131);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 19);
            this.label13.TabIndex = 96;
            this.label13.Text = "Amount :";
            // 
            // txt_amount
            // 
            this.txt_amount.BackColor = System.Drawing.Color.White;
            this.txt_amount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amount.Location = new System.Drawing.Point(549, 154);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.ReadOnly = true;
            this.txt_amount.Size = new System.Drawing.Size(91, 27);
            this.txt_amount.TabIndex = 18;
            this.txt_amount.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(479, 503);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 19);
            this.label14.TabIndex = 98;
            this.label14.Text = "Paid Amount  :";
            // 
            // txt_paid
            // 
            this.txt_paid.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_paid.Location = new System.Drawing.Point(586, 499);
            this.txt_paid.Name = "txt_paid";
            this.txt_paid.Size = new System.Drawing.Size(121, 27);
            this.txt_paid.TabIndex = 13;
            this.txt_paid.Text = "0";
            this.txt_paid.TextChanged += new System.EventHandler(this.txt_paid_TextChanged);
            this.txt_paid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_paid_KeyPress);
            this.txt_paid.Leave += new System.EventHandler(this.txt_paid_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(509, 533);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 19);
            this.label15.TabIndex = 102;
            this.label15.Text = "Balance  :";
            // 
            // txt_balance
            // 
            this.txt_balance.Enabled = false;
            this.txt_balance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_balance.Location = new System.Drawing.Point(586, 530);
            this.txt_balance.Name = "txt_balance";
            this.txt_balance.ReadOnly = true;
            this.txt_balance.Size = new System.Drawing.Size(121, 27);
            this.txt_balance.TabIndex = 101;
            this.txt_balance.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(453, 131);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 19);
            this.label16.TabIndex = 104;
            this.label16.Text = "Rate :";
            // 
            // textBox_rate
            // 
            this.textBox_rate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_rate.Location = new System.Drawing.Point(454, 154);
            this.textBox_rate.Name = "textBox_rate";
            this.textBox_rate.Size = new System.Drawing.Size(91, 27);
            this.textBox_rate.TabIndex = 7;
            this.textBox_rate.Text = "0";
            this.textBox_rate.TextChanged += new System.EventHandler(this.textBox_rate_TextChanged);
            this.textBox_rate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_rate_KeyPress);
            this.textBox_rate.Leave += new System.EventHandler(this.textBox_rate_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(72, 408);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 19);
            this.label18.TabIndex = 106;
            this.label18.Text = "Tax :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(239, 408);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 19);
            this.label19.TabIndex = 107;
            this.label19.Text = "Discount :";
            // 
            // textBoxTax
            // 
            this.textBoxTax.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTax.Location = new System.Drawing.Point(114, 405);
            this.textBoxTax.MaxLength = 4;
            this.textBoxTax.Name = "textBoxTax";
            this.textBoxTax.Size = new System.Drawing.Size(58, 27);
            this.textBoxTax.TabIndex = 10;
            this.textBoxTax.Text = "0";
            this.textBoxTax.TextChanged += new System.EventHandler(this.textBoxTax_TextChanged);
            this.textBoxTax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxTax_KeyPress);
            // 
            // textBox_Discount
            // 
            this.textBox_Discount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Discount.Location = new System.Drawing.Point(313, 405);
            this.textBox_Discount.MaxLength = 4;
            this.textBox_Discount.Name = "textBox_Discount";
            this.textBox_Discount.Size = new System.Drawing.Size(77, 27);
            this.textBox_Discount.TabIndex = 11;
            this.textBox_Discount.Text = "0";
            this.textBox_Discount.TextChanged += new System.EventHandler(this.textBox_Discount_TextChanged);
            this.textBox_Discount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Discount_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(708, 424);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 19);
            this.label21.TabIndex = 112;
            this.label21.Text = "Rs";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(708, 502);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(25, 19);
            this.label22.TabIndex = 113;
            this.label22.Text = "Rs";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(708, 532);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(25, 19);
            this.label23.TabIndex = 114;
            this.label23.Text = "Rs";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(456, 475);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 19);
            this.label17.TabIndex = 115;
            this.label17.Text = "Payment Details";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(484, 456);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(97, 19);
            this.label24.TabIndex = 116;
            this.label24.Text = "Grand Total :";
            // 
            // label_grandTotal
            // 
            this.label_grandTotal.AutoSize = true;
            this.label_grandTotal.Font = new System.Drawing.Font("Californian FB", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_grandTotal.ForeColor = System.Drawing.Color.Teal;
            this.label_grandTotal.Location = new System.Drawing.Point(586, 456);
            this.label_grandTotal.Name = "label_grandTotal";
            this.label_grandTotal.Size = new System.Drawing.Size(18, 19);
            this.label_grandTotal.TabIndex = 117;
            this.label_grandTotal.Text = "0";
            this.label_grandTotal.Click += new System.EventHandler(this.label_grandTotal_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(176, 408);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(20, 19);
            this.label20.TabIndex = 118;
            this.label20.Text = "%";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(396, 408);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(20, 19);
            this.label25.TabIndex = 119;
            this.label25.Text = "%";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(222, 131);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(80, 19);
            this.label26.TabIndex = 122;
            this.label26.Text = "Qty in ML :";
            // 
            // txt_qtyML
            // 
            this.txt_qtyML.Enabled = false;
            this.txt_qtyML.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_qtyML.Location = new System.Drawing.Point(222, 154);
            this.txt_qtyML.Name = "txt_qtyML";
            this.txt_qtyML.Size = new System.Drawing.Size(85, 27);
            this.txt_qtyML.TabIndex = 4;
            this.txt_qtyML.Text = "0";
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Image = global::Hotal_Managment_Syatem.Properties.Resources.images__2_;
            this.button2.Location = new System.Drawing.Point(586, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 33);
            this.button2.TabIndex = 120;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_delete;
            this.button1.Location = new System.Drawing.Point(268, 577);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 42);
            this.button1.TabIndex = 15;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_add
            // 
            this.btn_add.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_add.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_add;
            this.btn_add.Location = new System.Drawing.Point(644, 136);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(107, 42);
            this.btn_add.TabIndex = 9;
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_clear;
            this.btn_clear.Location = new System.Drawing.Point(387, 577);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(113, 42);
            this.btn_clear.TabIndex = 16;
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_close
            // 
            this.btn_close.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btn_close.Location = new System.Drawing.Point(506, 577);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(113, 42);
            this.btn_close.TabIndex = 17;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_save
            // 
            this.btn_save.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_save;
            this.btn_save.Location = new System.Drawing.Point(149, 577);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(113, 42);
            this.btn_save.TabIndex = 14;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Navy;
            this.label27.Location = new System.Drawing.Point(7, 9);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(163, 29);
            this.label27.TabIndex = 123;
            this.label27.Text = "Purchase Entry";
            // 
            // btnAddNew
            // 
            this.btnAddNew.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.plus__1_;
            this.btnAddNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNew.Location = new System.Drawing.Point(186, 152);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(30, 29);
            this.btnAddNew.TabIndex = 124;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // Purchase_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(757, 631);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txt_qtyML);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label_grandTotal);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.textBox_Discount);
            this.Controls.Add(this.textBoxTax);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBox_rate);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_balance);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_paid);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_amount);
            this.Controls.Add(this.chk_drink);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.txt_material_nm);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbl_erormsg);
            this.Controls.Add(this.txt_invoiceno);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_desc);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_amt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_rate);
            this.Controls.Add(this.cmb_unit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Qty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_v_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Purchase_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Entry";
            this.Load += new System.EventHandler(this.Purchase_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_v_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Qty;
        private System.Windows.Forms.ComboBox cmb_unit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_rate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_amt;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_desc;
        private System.Windows.Forms.TextBox txt_invoiceno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lbl_erormsg;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox txt_material_nm;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chk_drink;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_amount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_balance;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_paid;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_rate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_Discount;
        private System.Windows.Forms.TextBox textBoxTax;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label_grandTotal;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_qtyML;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnAddNew;
    }
}

