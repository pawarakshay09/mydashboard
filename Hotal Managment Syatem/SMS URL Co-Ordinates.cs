using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotal_Managment_Syatem
{
    public partial class SMS_URL_Co_Ordinates : Form
    {
        Database db = new Database();
        public SMS_URL_Co_Ordinates()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_url.Text == "")
                errorProvider1.SetError(txt_url,"Please Enter URL");
            else if(txt_senderName.Text=="")
                errorProvider1.SetError(txt_senderName, "Please Enter URL");
            else if(txtSmsType.Text=="")
                errorProvider1.SetError(txtSmsType, "Please Enter URL");
            else if (txtApiKey.Text == "")
                errorProvider1.SetError(txtApiKey, "Please Enter URL");
            else
            {
                db.insert("insert into URLSetting values('" + txt_url.Text + "','" + txt_senderName.Text + "','" + txtSmsType.Text + "','" + txtApiKey.Text + "')");
                txt_url.Text = "";
                txt_senderName.Text = "";
                txtSmsType.Text = "";
                txtApiKey.Text = "";
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SMS_URL_Co_Ordinates_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
        }
    }
}
