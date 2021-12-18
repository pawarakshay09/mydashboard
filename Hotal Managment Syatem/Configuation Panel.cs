using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace Hotal_Managment_Syatem
{
    public partial class Configuation_Panel : Form
    {
        Database db = new Database();
        DbBackup bkup = new DbBackup();
        string ConString, content,key,integraty;
        public Configuation_Panel()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txtRD.Text != "")
            {
                string getServiceName = string.Empty;

                errorProvider1.Clear();

                if (txt_path.Text == "")
                    errorProvider1.SetError(txt_path, "Please Enter Path");
                else if (txt_dbName.Text == "")
                    errorProvider1.SetError(txt_path, "Please Enter Database Name");

                else
                {

                    if (chkDays.Checked)
                        db.insert("update TrialPeriodSet set NoOfDays= '" + txtNoOfDays.Text + "' ,TrialDate='" + dtpDate.Value.ToString("MM/dd/yyyy") + "' ");
                    else
                    {

                        if (chkIntegraty.Checked)
                            integraty = "True";
                        else
                            integraty = "False";

                        getServiceName = txt_path.Text.Trim();
                        int getLength = getServiceName.Length;

                        //update sql service name                   
                        db.insert("update tbl_option set value= '" + getServiceName.Substring(2, getLength - 2) + "' where grp='sqlServiceName' ");


                        key = "5";
                        if (txt_userNm.Text == "" && txt_pwd.Text == "")

                            ConString = @"Data Source=" + txt_path.Text + ";Initial Catalog=" + txt_dbName.Text + ";Integrated Security=" + integraty + "";
                        else

                            ConString = @"Data Source=" + txt_path.Text + ";Initial Catalog=" + txt_dbName.Text + ";Integrated Security=" + integraty + "; User ID=" + txt_userNm.Text + ";Password=" + txt_pwd.Text;

                        File.WriteAllText("Config.txt", ConString);

                        content = File.ReadAllText("Config.txt");
                        txt_connectionstring.Text = content;

                        txtEncryptedString.Text = StringCipher.Encrypt(content, key);

                        File.WriteAllText("Config.txt", txtEncryptedString.Text);


                        /////Yogesh..................19.04.2019

                        db.update("update OwnerDetails set RDetailsId='" + txtRD.Text + "' ,Name='"+txthotel.Text+"'");

                        db.update("update tbl_CompanyInfo set companyName='" + txthotel.Text + "'");

                        db.update("update ws_table_list set RDetailsId='" + txtRD.Text + "'");

                        db.update("update tbl_receiptFormat set hotelName='" + txthotel.Text + "'");

                        db.update(" update Admin_Contacts set mob='"+txtadminmob.Text+"'");
                      
                       // checkonline ck = new checkonline();

                       //// string date = ck.getDbstatus_Value("select date from RestroOwnerMembership where RDetailsId='" + txtRD.Text + "' ");
                       // //string days = ck.getDbstatus_Value("select noofDays  from RestroOwnerMembership where RDetailsId='" + txtRD.Text + "'");
                    
                       // DateTime trDt = new DateTime();
                      
                       // date = date.Replace('/' ,'-');


                       // string dt = date.Split('-')[2] +'-'+ date.Split('-')[0] +'-'+ date.Split('-')[1];
                     

                       //db.update("update TrialPeriodSet set NoOfDays='" + days + "' ,TrialDate='" + dt + "'");


                        

                    }
                    MessageBox.Show("Record Updated Successfully !!!!");
                }
            }
            else
            {
                MessageBox.Show("Plz Enter RDetails ID ....Contact To ADMIN !!!!");
            }
        }
        void clear()
        {
            txt_dbName.Text = "";
            txt_path.Text = "";
            txt_pwd.Text = "";
            txt_userNm.Text = "";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Configuation_Panel_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            if (File.Exists("Config.txt"))
            {
                content = File.ReadAllText("Config.txt");
                if (content.Length == 0)
                    MessageBox.Show("empty file");
                else
                   txt_connectionstring.Text = content;

               
            }
            else { MessageBox.Show("file not avalibale"); }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            db.DeleteData("DELETE FROM total_bill", "total_bill"); //sales_item
            db.DeleteData("DELETE FROM sales_item", "sales_item");
            db.DeleteData("DELETE FROM table_order", "table_order");

            db.DeleteData("DELETE FROM material_nm", "material_nm");
            db.DeleteData("DELETE FROM table_order", "table_order");
            db.DeleteData("DELETE FROM payment", "payment");
            db.DeleteData("DELETE FROM supplier_dtls", "supplier_dtls");

            db.DeleteData("DELETE FROM tbl_expenses", "tbl_expenses");
            db.DeleteData("DELETE FROM tbl_kichen_exp", "tbl_kichen_exp");
            db.DeleteData("DELETE FROM tbl_material_use", "tbl_material_use");
            db.DeleteData("DELETE FROM tbl_PurchaseDetail", "tbl_PurchaseDetail");
            db.DeleteData("DELETE FROM tbl_purchasemaster", "tbl_purchasemaster");
            db.DeleteData("DELETE FROM tbl_stock", "tbl_stock");
            db.DeleteData("DELETE FROM tbl_unit", "tbl_unit");
            db.DeleteData("DELETE FROM waiter_dtls", "waiter_dtls");

            db.DeleteData("DELETE FROM Waiter_Payment", "Waiter_Payment");
            db.DeleteData("DELETE FROM waiter_prsenty", "waiter_prsenty");

            db.update("update table_status set status='Empty'");
            db.insert("DBCC CHECKIDENT (sales_item, RESEED, 0)");
            db.insert("DBCC CHECKIDENT (total_bill, RESEED, 0)");
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  txtEncryptKay.Text = StringCipher.Encrypt(txtPath.Text, txtPassKey.Text);

        }

        private void chkDays_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDays.Checked)
            {
                txtNoOfDays.Enabled = true;
                dtpDate.Enabled = true;
            }
            else
            {
                txtNoOfDays.Enabled = false;
                dtpDate.Enabled = false;
            }

        }

        private void txtNoOfDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.update("update TrialPeriodSet set NoOfDays='" + txtNoOfDays.Text + "',TrialDate='" + Convert.ToDateTime(dtpDate.Text).ToString("MM/dd/yyyy") + "' ");
            MessageBox.Show(" Trial Period Updated Sucessfully !!!!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //checkonline ck = new checkonline();
            //if (ck.ChkDb_Value(" select * from RestroOwnerMembership where RDetailsId='" + txtRD.Text + "' and RName='" + txthotel.Text + "' "))
            //{
            //    lblCorrect.Text = "Correct";
            //    lblCorrect.ForeColor = Color.Green;
              
                groupBox1.Visible = true;
            //}
            //else
            //{
            //    lblCorrect.Text = "Incorrect User";
            //    groupBox1.Visible = false;
            //    lblCorrect.ForeColor = Color.Red;
            //}

           // Data Source=103.235.104.24;Initial Catalog=abmstech_myRestrosoft;User ID=myrestrosoft; Password=abms@2014
        }

        private void txtRD_TextChanged(object sender, EventArgs e)
        {
            //lblCorrect.Text = "Incorrect User";
            //groupBox1.Visible = false;
            //lblCorrect.ForeColor = Color.Red;
        }

        private void txthotel_TextChanged(object sender, EventArgs e)
        {
            //lblCorrect.Text = "Incorrect User";
            //groupBox1.Visible = false;
            //lblCorrect.ForeColor = Color.Red;
        }
    }
}
