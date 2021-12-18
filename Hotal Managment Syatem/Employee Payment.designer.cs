namespace Hotal_Managment_Syatem
{
    partial class waitr_payment_1
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
            this.cmb_empname = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowData = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_salaryDtls = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_AdvPaidDtls = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgv_AdvRecivedDtls = new System.Windows.Forms.DataGridView();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.radioButtonAdvs = new System.Windows.Forms.RadioButton();
            this.radioButton_pay = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_prsenty = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_absent = new System.Windows.Forms.TextBox();
            this.txt_salary = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCutAdvnce = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPayAmt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxRemark = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panelSalaryDtls = new System.Windows.Forms.Panel();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.checkBoxAdvPay = new System.Windows.Forms.CheckBox();
            this.textBoxTotalshow = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label_total = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labelEmpSalary = new System.Windows.Forms.Label();
            this.label_empPaidAdv = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dateTimePickerPayDate = new System.Windows.Forms.DateTimePicker();
            this.btn_delete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_salaryDtls)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AdvPaidDtls)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AdvRecivedDtls)).BeginInit();
            this.panelSalaryDtls.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_empname
            // 
            this.cmb_empname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_empname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_empname.DisplayMember = "w_id";
            this.cmb_empname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_empname.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_empname.FormattingEnabled = true;
            this.cmb_empname.Location = new System.Drawing.Point(179, 73);
            this.cmb_empname.Name = "cmb_empname";
            this.cmb_empname.Size = new System.Drawing.Size(241, 27);
            this.cmb_empname.TabIndex = 1;
            this.cmb_empname.ValueMember = "w_id";
            this.cmb_empname.SelectedIndexChanged += new System.EventHandler(this.cmb_empname_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 19);
            this.label5.TabIndex = 160;
            this.label5.Text = "Select Employee Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Indigo;
            this.label1.Location = new System.Drawing.Point(23, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 23);
            this.label1.TabIndex = 162;
            this.label1.Text = "New Payment :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Indigo;
            this.label2.Location = new System.Drawing.Point(475, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 23);
            this.label2.TabIndex = 163;
            this.label2.Text = "Previous Payement :";
            // 
            // btnShowData
            // 
            this.btnShowData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowData.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_show;
            this.btnShowData.Location = new System.Drawing.Point(438, 63);
            this.btnShowData.Name = "btnShowData";
            this.btnShowData.Size = new System.Drawing.Size(110, 42);
            this.btnShowData.TabIndex = 2;
            this.btnShowData.UseVisualStyleBackColor = true;
            this.btnShowData.Click += new System.EventHandler(this.btnShowData_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(478, 160);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(672, 307);
            this.tabControl1.TabIndex = 165;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_salaryDtls);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(664, 278);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Salary Paid Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_salaryDtls
            // 
            this.dgv_salaryDtls.AllowUserToAddRows = false;
            this.dgv_salaryDtls.BackgroundColor = System.Drawing.Color.White;
            this.dgv_salaryDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_salaryDtls.Location = new System.Drawing.Point(6, 3);
            this.dgv_salaryDtls.Name = "dgv_salaryDtls";
            this.dgv_salaryDtls.ReadOnly = true;
            this.dgv_salaryDtls.Size = new System.Drawing.Size(652, 272);
            this.dgv_salaryDtls.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_AdvPaidDtls);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(664, 278);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Adv. Paid Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_AdvPaidDtls
            // 
            this.dgv_AdvPaidDtls.AllowUserToAddRows = false;
            this.dgv_AdvPaidDtls.BackgroundColor = System.Drawing.Color.White;
            this.dgv_AdvPaidDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AdvPaidDtls.Location = new System.Drawing.Point(6, 4);
            this.dgv_AdvPaidDtls.Name = "dgv_AdvPaidDtls";
            this.dgv_AdvPaidDtls.ReadOnly = true;
            this.dgv_AdvPaidDtls.Size = new System.Drawing.Size(701, 272);
            this.dgv_AdvPaidDtls.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgv_AdvRecivedDtls);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(664, 278);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Adv. Received";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgv_AdvRecivedDtls
            // 
            this.dgv_AdvRecivedDtls.AllowUserToAddRows = false;
            this.dgv_AdvRecivedDtls.BackgroundColor = System.Drawing.Color.White;
            this.dgv_AdvRecivedDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AdvRecivedDtls.Location = new System.Drawing.Point(6, 4);
            this.dgv_AdvRecivedDtls.Name = "dgv_AdvRecivedDtls";
            this.dgv_AdvRecivedDtls.ReadOnly = true;
            this.dgv_AdvRecivedDtls.Size = new System.Drawing.Size(701, 272);
            this.dgv_AdvRecivedDtls.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(229, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(32, 19);
            this.label19.TabIndex = 168;
            this.label19.Text = "To :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(48, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 19);
            this.label18.TabIndex = 167;
            this.label18.Text = "From :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(7, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 19);
            this.label17.TabIndex = 166;
            this.label17.Text = "Select Date ";
            // 
            // radioButtonAdvs
            // 
            this.radioButtonAdvs.AutoSize = true;
            this.radioButtonAdvs.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAdvs.Location = new System.Drawing.Point(246, 154);
            this.radioButtonAdvs.Name = "radioButtonAdvs";
            this.radioButtonAdvs.Size = new System.Drawing.Size(82, 23);
            this.radioButtonAdvs.TabIndex = 4;
            this.radioButtonAdvs.Text = "Advance";
            this.radioButtonAdvs.UseVisualStyleBackColor = true;
            this.radioButtonAdvs.CheckedChanged += new System.EventHandler(this.radioButtonAdvs_CheckedChanged);
            // 
            // radioButton_pay
            // 
            this.radioButton_pay.AutoSize = true;
            this.radioButton_pay.Checked = true;
            this.radioButton_pay.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_pay.Location = new System.Drawing.Point(137, 154);
            this.radioButton_pay.Name = "radioButton_pay";
            this.radioButton_pay.Size = new System.Drawing.Size(83, 23);
            this.radioButton_pay.TabIndex = 3;
            this.radioButton_pay.TabStop = true;
            this.radioButton_pay.Text = "Payment";
            this.radioButton_pay.UseVisualStyleBackColor = true;
            this.radioButton_pay.CheckedChanged += new System.EventHandler(this.radioButton_pay_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(33, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 19);
            this.label10.TabIndex = 171;
            this.label10.Text = "Payment Type :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 434);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 19);
            this.label3.TabIndex = 174;
            this.label3.Text = "Payment Date :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(32, 61);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 19);
            this.label16.TabIndex = 177;
            this.label16.Text = "Present :";
            // 
            // textBox_prsenty
            // 
            this.textBox_prsenty.BackColor = System.Drawing.Color.White;
            this.textBox_prsenty.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_prsenty.Location = new System.Drawing.Point(101, 58);
            this.textBox_prsenty.MaxLength = 3;
            this.textBox_prsenty.Multiline = true;
            this.textBox_prsenty.Name = "textBox_prsenty";
            this.textBox_prsenty.Size = new System.Drawing.Size(81, 23);
            this.textBox_prsenty.TabIndex = 15;
            this.textBox_prsenty.Text = "0";
            this.textBox_prsenty.TextChanged += new System.EventHandler(this.textBox_prsenty_TextChanged);
            this.textBox_prsenty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_prsenty_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(199, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 19);
            this.label20.TabIndex = 179;
            this.label20.Text = "Absent :";
            // 
            // textBox_absent
            // 
            this.textBox_absent.BackColor = System.Drawing.Color.White;
            this.textBox_absent.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_absent.Location = new System.Drawing.Point(265, 58);
            this.textBox_absent.MaxLength = 3;
            this.textBox_absent.Multiline = true;
            this.textBox_absent.Name = "textBox_absent";
            this.textBox_absent.Size = new System.Drawing.Size(90, 23);
            this.textBox_absent.TabIndex = 16;
            this.textBox_absent.Text = "0";
            this.textBox_absent.TextChanged += new System.EventHandler(this.textBox_absent_TextChanged);
            this.textBox_absent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_absent_KeyPress);
            // 
            // txt_salary
            // 
            this.txt_salary.BackColor = System.Drawing.Color.White;
            this.txt_salary.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_salary.Location = new System.Drawing.Point(102, 93);
            this.txt_salary.MaxLength = 7;
            this.txt_salary.Name = "txt_salary";
            this.txt_salary.ReadOnly = true;
            this.txt_salary.Size = new System.Drawing.Size(131, 27);
            this.txt_salary.TabIndex = 181;
            this.txt_salary.Text = "0";
            this.txt_salary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_salary_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(5, 96);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(92, 19);
            this.label24.TabIndex = 180;
            this.label24.Text = "Total Salary :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 19);
            this.label6.TabIndex = 183;
            this.label6.Text = "Less :";
            // 
            // textBoxCutAdvnce
            // 
            this.textBoxCutAdvnce.BackColor = System.Drawing.Color.White;
            this.textBoxCutAdvnce.Enabled = false;
            this.textBoxCutAdvnce.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCutAdvnce.Location = new System.Drawing.Point(126, 167);
            this.textBoxCutAdvnce.MaxLength = 7;
            this.textBoxCutAdvnce.Name = "textBoxCutAdvnce";
            this.textBoxCutAdvnce.Size = new System.Drawing.Size(131, 27);
            this.textBoxCutAdvnce.TabIndex = 8;
            this.textBoxCutAdvnce.Text = "0";
            this.textBoxCutAdvnce.TextChanged += new System.EventHandler(this.textBoxCutAdvnce_TextChanged);
            this.textBoxCutAdvnce.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCutAdvnce_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 19);
            this.label7.TabIndex = 185;
            this.label7.Text = "Cut Adv. Amount :";
            // 
            // textBoxPayAmt
            // 
            this.textBoxPayAmt.BackColor = System.Drawing.Color.White;
            this.textBoxPayAmt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPayAmt.Location = new System.Drawing.Point(147, 465);
            this.textBoxPayAmt.MaxLength = 7;
            this.textBoxPayAmt.Name = "textBoxPayAmt";
            this.textBoxPayAmt.Size = new System.Drawing.Size(142, 27);
            this.textBoxPayAmt.TabIndex = 10;
            this.textBoxPayAmt.Text = "0";
            this.textBoxPayAmt.TextChanged += new System.EventHandler(this.textBoxPayAmt_TextChanged);
            this.textBoxPayAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPayAmt_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 468);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 19);
            this.label8.TabIndex = 187;
            this.label8.Text = "Payble Amount :";
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_save;
            this.buttonSave.Location = new System.Drawing.Point(11, 536);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(110, 42);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_clear;
            this.buttonClear.Location = new System.Drawing.Point(243, 536);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(110, 42);
            this.buttonClear.TabIndex = 13;
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.buttonClose.Location = new System.Drawing.Point(359, 536);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(110, 42);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxRemark
            // 
            this.textBoxRemark.BackColor = System.Drawing.Color.White;
            this.textBoxRemark.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRemark.Location = new System.Drawing.Point(148, 498);
            this.textBoxRemark.Name = "textBoxRemark";
            this.textBoxRemark.Size = new System.Drawing.Size(266, 27);
            this.textBoxRemark.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(73, 501);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 19);
            this.label11.TabIndex = 192;
            this.label11.Text = "Remark :";
            // 
            // panelSalaryDtls
            // 
            this.panelSalaryDtls.Controls.Add(this.dateTimePickerTo);
            this.panelSalaryDtls.Controls.Add(this.dateTimePickerFrom);
            this.panelSalaryDtls.Controls.Add(this.checkBoxAdvPay);
            this.panelSalaryDtls.Controls.Add(this.label17);
            this.panelSalaryDtls.Controls.Add(this.label18);
            this.panelSalaryDtls.Controls.Add(this.label19);
            this.panelSalaryDtls.Controls.Add(this.textBox_prsenty);
            this.panelSalaryDtls.Controls.Add(this.label16);
            this.panelSalaryDtls.Controls.Add(this.textBox_absent);
            this.panelSalaryDtls.Controls.Add(this.textBoxCutAdvnce);
            this.panelSalaryDtls.Controls.Add(this.label20);
            this.panelSalaryDtls.Controls.Add(this.label7);
            this.panelSalaryDtls.Controls.Add(this.label24);
            this.panelSalaryDtls.Controls.Add(this.txt_salary);
            this.panelSalaryDtls.Controls.Add(this.label6);
            this.panelSalaryDtls.Location = new System.Drawing.Point(25, 190);
            this.panelSalaryDtls.Name = "panelSalaryDtls";
            this.panelSalaryDtls.Size = new System.Drawing.Size(395, 210);
            this.panelSalaryDtls.TabIndex = 194;
            this.panelSalaryDtls.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSalaryDtls_Paint);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTo.Location = new System.Drawing.Point(262, 26);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(121, 27);
            this.dateTimePickerTo.TabIndex = 6;
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.dateTimePickerTo_ValueChanged);
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(100, 27);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(121, 27);
            this.dateTimePickerFrom.TabIndex = 5;
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.dateTimePickerFrom_ValueChanged);
            // 
            // checkBoxAdvPay
            // 
            this.checkBoxAdvPay.AutoSize = true;
            this.checkBoxAdvPay.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAdvPay.Location = new System.Drawing.Point(262, 170);
            this.checkBoxAdvPay.Name = "checkBoxAdvPay";
            this.checkBoxAdvPay.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAdvPay.TabIndex = 7;
            this.checkBoxAdvPay.UseVisualStyleBackColor = true;
            this.checkBoxAdvPay.CheckedChanged += new System.EventHandler(this.checkBoxAdvPay_CheckedChanged);
            // 
            // textBoxTotalshow
            // 
            this.textBoxTotalshow.BackColor = System.Drawing.Color.White;
            this.textBoxTotalshow.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotalshow.ForeColor = System.Drawing.Color.Navy;
            this.textBoxTotalshow.Location = new System.Drawing.Point(1002, 474);
            this.textBoxTotalshow.Name = "textBoxTotalshow";
            this.textBoxTotalshow.ReadOnly = true;
            this.textBoxTotalshow.Size = new System.Drawing.Size(142, 27);
            this.textBoxTotalshow.TabIndex = 196;
            this.textBoxTotalshow.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(952, 478);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 19);
            this.label12.TabIndex = 195;
            this.label12.Text = "Total :";
            this.label12.Visible = false;
            // 
            // label_total
            // 
            this.label_total.AutoSize = true;
            this.label_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_total.Location = new System.Drawing.Point(1005, 596);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(12, 16);
            this.label_total.TabIndex = 197;
            this.label_total.Text = "-";
            this.label_total.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(599, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(153, 23);
            this.label14.TabIndex = 199;
            this.label14.Text = "Employee  Salary :";
            // 
            // labelEmpSalary
            // 
            this.labelEmpSalary.AutoSize = true;
            this.labelEmpSalary.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmpSalary.ForeColor = System.Drawing.Color.Purple;
            this.labelEmpSalary.Location = new System.Drawing.Point(761, 73);
            this.labelEmpSalary.Name = "labelEmpSalary";
            this.labelEmpSalary.Size = new System.Drawing.Size(20, 23);
            this.labelEmpSalary.TabIndex = 200;
            this.labelEmpSalary.Text = "0";
            // 
            // label_empPaidAdv
            // 
            this.label_empPaidAdv.AutoSize = true;
            this.label_empPaidAdv.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_empPaidAdv.ForeColor = System.Drawing.Color.Purple;
            this.label_empPaidAdv.Location = new System.Drawing.Point(1004, 73);
            this.label_empPaidAdv.Name = "label_empPaidAdv";
            this.label_empPaidAdv.Size = new System.Drawing.Size(20, 23);
            this.label_empPaidAdv.TabIndex = 202;
            this.label_empPaidAdv.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(862, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 23);
            this.label15.TabIndex = 201;
            this.label15.Text = "Employee Adv. :";
            // 
            // dateTimePickerPayDate
            // 
            this.dateTimePickerPayDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerPayDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerPayDate.Location = new System.Drawing.Point(148, 430);
            this.dateTimePickerPayDate.Name = "dateTimePickerPayDate";
            this.dateTimePickerPayDate.Size = new System.Drawing.Size(141, 27);
            this.dateTimePickerPayDate.TabIndex = 9;
            // 
            // btn_delete
            // 
            this.btn_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_delete;
            this.btn_delete.Location = new System.Drawing.Point(127, 536);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(110, 42);
            this.btn_delete.TabIndex = 203;
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 29);
            this.label4.TabIndex = 204;
            this.label4.Text = "Employee  Payment";
            // 
            // waitr_payment_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(1028, 612);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.dateTimePickerPayDate);
            this.Controls.Add(this.label_empPaidAdv);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.labelEmpSalary);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label_total);
            this.Controls.Add(this.textBoxTotalshow);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panelSalaryDtls);
            this.Controls.Add(this.textBoxRemark);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPayAmt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButtonAdvs);
            this.Controls.Add(this.radioButton_pay);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnShowData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_empname);
            this.Controls.Add(this.label5);
            this.Name = "waitr_payment_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Payment";
            this.Load += new System.EventHandler(this.waitr_payment_1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_salaryDtls)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AdvPaidDtls)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AdvRecivedDtls)).EndInit();
            this.panelSalaryDtls.ResumeLayout(false);
            this.panelSalaryDtls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_empname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShowData;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RadioButton radioButtonAdvs;
        private System.Windows.Forms.RadioButton radioButton_pay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_prsenty;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox_absent;
        private System.Windows.Forms.TextBox txt_salary;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCutAdvnce;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPayAmt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxRemark;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panelSalaryDtls;
        private System.Windows.Forms.TextBox textBoxTotalshow;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.CheckBox checkBoxAdvPay;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelEmpSalary;
        private System.Windows.Forms.DataGridView dgv_salaryDtls;
        private System.Windows.Forms.DataGridView dgv_AdvPaidDtls;
        private System.Windows.Forms.DataGridView dgv_AdvRecivedDtls;
        private System.Windows.Forms.Label label_empPaidAdv;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerPayDate;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Label label4;
    }
}