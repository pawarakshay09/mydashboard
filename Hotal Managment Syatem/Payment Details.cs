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
    
    public partial class Payment_Details : Form
    {
        Database db = new Database();
        public Payment_Details()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
            else
            {
                dtp_from.Enabled = false;
                dtp_to.Enabled = false;
            }
        }

        private void Payment_Details_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button1;
            dataGridView1.DataSource = db.Displaygrid("SELECT   lodge_payment.name, lodge_payment.room_no, lodge_payment.total, lodge_payment.paid, lodge_payment.status, tbl_chk_out_dtls.chk_out_date,   tbl_chk_out_dtls.chk_out_time FROM            lodge_payment INNER JOIN      tbl_chk_out_dtls ON lodge_payment.book_id = tbl_chk_out_dtls.book_id");
        }

        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            string datefrm = dtp_from.Value.ToString("MM-dd-yyyy");
            string dateto = dtp_to.Value.ToString("MM-dd-yyyy");
            dataGridView1.DataSource = db.Displaygrid("SELECT   lodge_payment.name, lodge_payment.room_no, lodge_payment.total, lodge_payment.paid, lodge_payment.status, tbl_chk_out_dtls.chk_out_date,   tbl_chk_out_dtls.chk_out_time FROM            lodge_payment INNER JOIN      tbl_chk_out_dtls ON lodge_payment.book_id = tbl_chk_out_dtls.book_id where tbl_chk_out_dtls.chk_out_date between '" + datefrm + "' and '" + dateto + "'");
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            string datefrm = dtp_from.Value.ToString("MM-dd-yyyy");
            string dateto = dtp_to.Value.ToString("MM-dd-yyyy");
            dataGridView1.DataSource = db.Displaygrid("SELECT   lodge_payment.name, lodge_payment.room_no, lodge_payment.total, lodge_payment.paid, lodge_payment.status, tbl_chk_out_dtls.chk_out_date,   tbl_chk_out_dtls.chk_out_time FROM            lodge_payment INNER JOIN      tbl_chk_out_dtls ON lodge_payment.book_id = tbl_chk_out_dtls.book_id where tbl_chk_out_dtls.chk_out_date between '" + datefrm + "' and '" + dateto + "'");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT   lodge_payment.name, lodge_payment.room_no, lodge_payment.total, lodge_payment.paid, lodge_payment.status, tbl_chk_out_dtls.chk_out_date,   tbl_chk_out_dtls.chk_out_time FROM            lodge_payment INNER JOIN      tbl_chk_out_dtls ON lodge_payment.book_id = tbl_chk_out_dtls.book_id");

        }
    }
}
