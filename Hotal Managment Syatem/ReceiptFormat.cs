using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Hotal_Managment_Syatem
{
    public partial class ReceiptFormat : Form
    {
        Database db = new Database();
        public ReceiptFormat()
        {
            InitializeComponent();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want to Update Record?", "Confirmation Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                db.update("update tbl_receiptFormat set hotelName='" + textBoxHotelName.Text + "',tagline='" + textBox_TagLine.Text + "',address='" + textBox_address.Text + "',address2='" + txtAdd2.Text + "',mobile='" + textBox_mob.Text + "',footer1='" + textBox_footer1.Text + "',footer2='" + textBoxfooter2.Text + "',companyName='" + txtCompanyNm.Text + "',hotelNmFontSize='" + txthotelNmFontSize.Text + "',taglineFontSize='" + txttaglineFontsize.Text + "',addressFontSize='" + txtaddressfontsize.Text + "',mobileFontSize='" + txtMobFontSize.Text + "',footer2FontSize='" + txtfooter2fontSize.Text + "',companyNameFontSize='" + txtCmpnyNmFontSize.Text + "',itemNmFontSize='" + txtitemFontSize.Text + "',tblNoFontSize='" + txtTblNoFontSize.Text + "',billNoFontSize='" + txtBillNoFontSize.Text + "',dateFontSize='" + txtDateFontSize.Text + "',totalFontSize='" + txtAmtFontSize.Text + "',discFontSize='" + txtDiscFontSize.Text + "',GrandFontSize='" + txtGrandFontSize.Text + "',AddressSize2='"+txtAddres2FontSize.Text+"' ,prefix='"+txtprefix.Text+"'");
                db.msgbox();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReceiptFormat_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            //form formmating 
            db.formFix(this);

            db.cnopen();
            SqlCommand cmd = new SqlCommand("select * from tbl_receiptFormat", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBoxHotelName.Text = rd["hotelName"].ToString();
                textBox_TagLine.Text = rd["tagline"].ToString();
                textBox_address.Text = rd["address"].ToString();
                textBox_mob.Text = rd["mobile"].ToString();
                textBox_footer1.Text = rd["footer1"].ToString();
                textBoxfooter2.Text = rd["footer2"].ToString();
                txtCompanyNm.Text = rd["companyName"].ToString();
                txthotelNmFontSize.Text = rd["hotelNmFontSize"].ToString();
                txttaglineFontsize.Text = rd["taglineFontSize"].ToString();
                txtaddressfontsize.Text = rd["addressFontSize"].ToString();
                txtMobFontSize.Text = rd["mobileFontSize"].ToString();
                txtfooter2fontSize.Text = rd["footer2FontSize"].ToString();
                txtCmpnyNmFontSize.Text = rd["companyNameFontSize"].ToString();
                txtitemFontSize.Text = rd["itemNmFontSize"].ToString();
                txtTblNoFontSize.Text = rd["tblNoFontSize"].ToString();
                txtBillNoFontSize.Text = rd["billNoFontSize"].ToString();
                txtDateFontSize.Text = rd["dateFontSize"].ToString();
                txtAmtFontSize.Text = rd["totalFontSize"].ToString();
                txtDiscFontSize.Text = rd["discFontSize"].ToString();
                txtGrandFontSize.Text = rd["GrandFontSize"].ToString();
                txtAdd2.Text = rd["address2"].ToString();
                txtAddres2FontSize.Text = rd["AddressSize2"].ToString();
                txtprefix.Text = rd["prefix"].ToString();
            }
            
            db.cnclose();
        }

        private void txthotelNmFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txttaglineFontsize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtaddressfontsize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtMobFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtfooter2fontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtCmpnyNmFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtitemFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtTblNoFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtBillNoFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtDateFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtAmtFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtDiscFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtGrandFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void textBox_mob_KeyPress(object sender, KeyPressEventArgs e)
        {
            //db.onlyNumber(e);
        }
    }
}
