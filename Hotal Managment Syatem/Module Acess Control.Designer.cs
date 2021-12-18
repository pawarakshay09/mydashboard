namespace Hotal_Managment_Syatem
{
    partial class Module_Acess_Control
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
            this.dataGridViewAssignedContrls = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListAccessList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssignedContrls)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAssignedContrls
            // 
            this.dataGridViewAssignedContrls.AllowUserToAddRows = false;
            this.dataGridViewAssignedContrls.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAssignedContrls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAssignedContrls.Location = new System.Drawing.Point(265, 97);
            this.dataGridViewAssignedContrls.Name = "dataGridViewAssignedContrls";
            this.dataGridViewAssignedContrls.RowHeadersVisible = false;
            this.dataGridViewAssignedContrls.Size = new System.Drawing.Size(260, 361);
            this.dataGridViewAssignedContrls.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.BlueViolet;
            this.label4.Location = new System.Drawing.Point(268, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "Access Control Details";
            // 
            // checkedListAccessList
            // 
            this.checkedListAccessList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListAccessList.FormattingEnabled = true;
            this.checkedListAccessList.Location = new System.Drawing.Point(31, 97);
            this.checkedListAccessList.Name = "checkedListAccessList";
            this.checkedListAccessList.Size = new System.Drawing.Size(208, 292);
            this.checkedListAccessList.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.BlueViolet;
            this.label1.Location = new System.Drawing.Point(35, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Access Control Panel";
            // 
            // button2
            // 
            this.button2.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.button2.Location = new System.Drawing.Point(142, 416);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 42);
            this.button2.TabIndex = 14;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_add;
            this.btn_Add.Location = new System.Drawing.Point(28, 416);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(108, 42);
            this.btn_Add.TabIndex = 13;
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Navy;
            this.label22.Location = new System.Drawing.Point(12, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(238, 29);
            this.label22.TabIndex = 127;
            this.label22.Text = "Module Acess Control ";
            // 
            // Module_Acess_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(568, 475);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.dataGridViewAssignedContrls);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.checkedListAccessList);
            this.Controls.Add(this.label1);
            this.Name = "Module_Acess_Control";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Module Acess Control ";
            this.Load += new System.EventHandler(this.Module_Acess_Control_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssignedContrls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAssignedContrls;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.CheckedListBox checkedListAccessList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label22;
    }
}