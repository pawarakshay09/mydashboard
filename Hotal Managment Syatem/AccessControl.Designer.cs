namespace Hotal_Managment_Syatem
{
    partial class AccessControl
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
            this.btn_Add = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewAssignedContrls = new System.Windows.Forms.DataGridView();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxUsersList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_grp = new System.Windows.Forms.ComboBox();
            this.checkedListAccessList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssignedContrls)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Add.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_add;
            this.btn_Add.Location = new System.Drawing.Point(236, 471);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(117, 42);
            this.btn_Add.TabIndex = 4;
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btnClose.Location = new System.Drawing.Point(359, 471);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 42);
            this.btnClose.TabIndex = 5;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(398, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Access Control Details";
            // 
            // dataGridViewAssignedContrls
            // 
            this.dataGridViewAssignedContrls.AllowUserToAddRows = false;
            this.dataGridViewAssignedContrls.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAssignedContrls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAssignedContrls.Location = new System.Drawing.Point(394, 75);
            this.dataGridViewAssignedContrls.Name = "dataGridViewAssignedContrls";
            this.dataGridViewAssignedContrls.RowHeadersVisible = false;
            this.dataGridViewAssignedContrls.Size = new System.Drawing.Size(299, 369);
            this.dataGridViewAssignedContrls.TabIndex = 3;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Navy;
            this.label17.Location = new System.Drawing.Point(12, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(155, 29);
            this.label17.TabIndex = 112;
            this.label17.Text = "Acess Control ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxUsersList);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmb_grp);
            this.groupBox1.Controls.Add(this.checkedListAccessList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.BlueViolet;
            this.groupBox1.Location = new System.Drawing.Point(4, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 384);
            this.groupBox1.TabIndex = 113;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acess Control";
            // 
            // comboBoxUsersList
            // 
            this.comboBoxUsersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUsersList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUsersList.FormattingEnabled = true;
            this.comboBoxUsersList.Location = new System.Drawing.Point(160, 39);
            this.comboBoxUsersList.Name = "comboBoxUsersList";
            this.comboBoxUsersList.Size = new System.Drawing.Size(208, 27);
            this.comboBoxUsersList.TabIndex = 8;
            this.comboBoxUsersList.SelectedIndexChanged += new System.EventHandler(this.comboBoxUsersList_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(24, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Select User Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(55, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Select Group :";
            // 
            // cmb_grp
            // 
            this.cmb_grp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_grp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_grp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_grp.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_grp.FormattingEnabled = true;
            this.cmb_grp.Location = new System.Drawing.Point(160, 88);
            this.cmb_grp.Name = "cmb_grp";
            this.cmb_grp.Size = new System.Drawing.Size(208, 27);
            this.cmb_grp.TabIndex = 9;
            this.cmb_grp.SelectedIndexChanged += new System.EventHandler(this.cmb_grp_SelectedIndexChanged_1);
            // 
            // checkedListAccessList
            // 
            this.checkedListAccessList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListAccessList.FormattingEnabled = true;
            this.checkedListAccessList.Location = new System.Drawing.Point(160, 144);
            this.checkedListAccessList.Name = "checkedListAccessList";
            this.checkedListAccessList.Size = new System.Drawing.Size(208, 224);
            this.checkedListAccessList.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "Access Control Panel";
            // 
            // AccessControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(705, 525);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dataGridViewAssignedContrls);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btn_Add);
            this.Name = "AccessControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Access Control";
            this.Load += new System.EventHandler(this.AccessControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssignedContrls)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewAssignedContrls;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxUsersList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_grp;
        private System.Windows.Forms.CheckedListBox checkedListAccessList;
        private System.Windows.Forms.Label label1;
    }
}