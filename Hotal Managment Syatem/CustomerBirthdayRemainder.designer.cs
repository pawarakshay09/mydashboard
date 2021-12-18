namespace Hotal_Managment_Syatem
{
    partial class customerBirthdayRemainder
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
            this.buttonReminderSnooz = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_birthday = new System.Windows.Forms.DataGridView();
            this.dgv_stock = new System.Windows.Forms.DataGridView();
            this.panel_birthday = new System.Windows.Forms.Panel();
            this.panelStock = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_birthday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_stock)).BeginInit();
            this.panel_birthday.SuspendLayout();
            this.panelStock.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReminderSnooz
            // 
            this.buttonReminderSnooz.Location = new System.Drawing.Point(245, 108);
            this.buttonReminderSnooz.Name = "buttonReminderSnooz";
            this.buttonReminderSnooz.Size = new System.Drawing.Size(75, 23);
            this.buttonReminderSnooz.TabIndex = 1;
            this.buttonReminderSnooz.Text = "After 2 hr ";
            this.buttonReminderSnooz.UseVisualStyleBackColor = true;
            this.buttonReminderSnooz.Visible = false;
            // 
            // btn_close
            // 
            this.btn_close.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btn_close.Location = new System.Drawing.Point(174, 484);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(110, 42);
            this.btn_close.TabIndex = 2;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Purple;
            this.label1.Location = new System.Drawing.Point(15, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Today\'s Birthday";
            // 
            // dgv_birthday
            // 
            this.dgv_birthday.AllowUserToAddRows = false;
            this.dgv_birthday.BackgroundColor = System.Drawing.Color.White;
            this.dgv_birthday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_birthday.Location = new System.Drawing.Point(12, 34);
            this.dgv_birthday.Name = "dgv_birthday";
            this.dgv_birthday.ReadOnly = true;
            this.dgv_birthday.Size = new System.Drawing.Size(427, 175);
            this.dgv_birthday.TabIndex = 0;
            // 
            // dgv_stock
            // 
            this.dgv_stock.AllowUserToAddRows = false;
            this.dgv_stock.AllowUserToDeleteRows = false;
            this.dgv_stock.BackgroundColor = System.Drawing.Color.White;
            this.dgv_stock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_stock.Location = new System.Drawing.Point(15, 47);
            this.dgv_stock.Name = "dgv_stock";
            this.dgv_stock.ReadOnly = true;
            this.dgv_stock.Size = new System.Drawing.Size(427, 164);
            this.dgv_stock.TabIndex = 4;
            // 
            // panel_birthday
            // 
            this.panel_birthday.Controls.Add(this.dgv_birthday);
            this.panel_birthday.Controls.Add(this.label1);
            this.panel_birthday.Location = new System.Drawing.Point(14, 12);
            this.panel_birthday.Name = "panel_birthday";
            this.panel_birthday.Size = new System.Drawing.Size(455, 223);
            this.panel_birthday.TabIndex = 5;
            // 
            // panelStock
            // 
            this.panelStock.Controls.Add(this.label2);
            this.panelStock.Controls.Add(this.dgv_stock);
            this.panelStock.Location = new System.Drawing.Point(12, 241);
            this.panelStock.Name = "panelStock";
            this.panelStock.Size = new System.Drawing.Size(454, 234);
            this.panelStock.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Purple;
            this.label2.Location = new System.Drawing.Point(17, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Stock Reminder";
            // 
            // customerBirthdayRemainder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(485, 536);
            this.Controls.Add(this.panelStock);
            this.Controls.Add(this.panel_birthday);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.buttonReminderSnooz);
            this.Name = "customerBirthdayRemainder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "customerBirthdayRemainder";
            this.Load += new System.EventHandler(this.customerBirthdayRemainder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_birthday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_stock)).EndInit();
            this.panel_birthday.ResumeLayout(false);
            this.panel_birthday.PerformLayout();
            this.panelStock.ResumeLayout(false);
            this.panelStock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonReminderSnooz;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_birthday;
        private System.Windows.Forms.DataGridView dgv_stock;
        private System.Windows.Forms.Panel panel_birthday;
        private System.Windows.Forms.Panel panelStock;
        private System.Windows.Forms.Label label2;
    }
}