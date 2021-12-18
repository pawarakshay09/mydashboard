namespace Hotal_Managment_Syatem
{
    partial class Table_Swap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Table_Swap));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_current_tbl = new System.Windows.Forms.ComboBox();
            this.cmb_emty_tbl = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_current_tbl = new System.Windows.Forms.TextBox();
            this.txt_empty_tbl = new System.Windows.Forms.TextBox();
            this.btn_change = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Table";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(254, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Empty Table";
            // 
            // cmb_current_tbl
            // 
            this.cmb_current_tbl.BackColor = System.Drawing.Color.Red;
            this.cmb_current_tbl.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_current_tbl.ForeColor = System.Drawing.Color.White;
            this.cmb_current_tbl.FormattingEnabled = true;
            this.cmb_current_tbl.Location = new System.Drawing.Point(78, 106);
            this.cmb_current_tbl.Name = "cmb_current_tbl";
            this.cmb_current_tbl.Size = new System.Drawing.Size(121, 37);
            this.cmb_current_tbl.TabIndex = 2;
            this.cmb_current_tbl.SelectedIndexChanged += new System.EventHandler(this.cmb_current_tbl_SelectedIndexChanged);
            // 
            // cmb_emty_tbl
            // 
            this.cmb_emty_tbl.BackColor = System.Drawing.Color.DarkGreen;
            this.cmb_emty_tbl.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_emty_tbl.ForeColor = System.Drawing.Color.White;
            this.cmb_emty_tbl.FormattingEnabled = true;
            this.cmb_emty_tbl.Location = new System.Drawing.Point(254, 106);
            this.cmb_emty_tbl.Name = "cmb_emty_tbl";
            this.cmb_emty_tbl.Size = new System.Drawing.Size(121, 37);
            this.cmb_emty_tbl.TabIndex = 3;
            this.cmb_emty_tbl.SelectedIndexChanged += new System.EventHandler(this.cmb_emty_tbl_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Selected Table";
            // 
            // txt_current_tbl
            // 
            this.txt_current_tbl.Enabled = false;
            this.txt_current_tbl.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_current_tbl.Location = new System.Drawing.Point(78, 195);
            this.txt_current_tbl.Name = "txt_current_tbl";
            this.txt_current_tbl.Size = new System.Drawing.Size(121, 40);
            this.txt_current_tbl.TabIndex = 5;
            this.txt_current_tbl.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_empty_tbl
            // 
            this.txt_empty_tbl.Enabled = false;
            this.txt_empty_tbl.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empty_tbl.Location = new System.Drawing.Point(254, 195);
            this.txt_empty_tbl.Name = "txt_empty_tbl";
            this.txt_empty_tbl.Size = new System.Drawing.Size(121, 40);
            this.txt_empty_tbl.TabIndex = 6;
            // 
            // btn_change
            // 
            this.btn_change.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_change.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_change.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_change.Location = new System.Drawing.Point(45, 271);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(112, 42);
            this.btn_change.TabIndex = 7;
            this.btn_change.Text = "Apply";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_close.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btn_close.Location = new System.Drawing.Point(281, 271);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(110, 42);
            this.btn_close.TabIndex = 9;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_clear.Image = global::Hotal_Managment_Syatem.Properties.Resources.btn_clear;
            this.btn_clear.Location = new System.Drawing.Point(165, 271);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(110, 42);
            this.btn_clear.TabIndex = 8;
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Navy;
            this.label12.Location = new System.Drawing.Point(12, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 29);
            this.label12.TabIndex = 96;
            this.label12.Text = "Swap Table";
            // 
            // Table_Swap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(471, 354);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_change);
            this.Controls.Add(this.txt_empty_tbl);
            this.Controls.Add(this.txt_current_tbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_emty_tbl);
            this.Controls.Add(this.cmb_current_tbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Table_Swap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Swap Table";
            this.Load += new System.EventHandler(this.Table_Swap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_current_tbl;
        private System.Windows.Forms.ComboBox cmb_emty_tbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_current_tbl;
        private System.Windows.Forms.TextBox txt_empty_tbl;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label12;
    }
}