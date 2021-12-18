using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    public partial class Payment_Report : Form
    {
        Database db = new Database();
        bool ispageLoad = false;
        public Payment_Report()
        {
            InitializeComponent();
        }

        private void Payment_Report_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            pageLoadFun();
        }
        void pageLoadFun()
        {
            db.comboFill(cmb_dealer_nm, "select * from supplier_dtls order by sup_name asc", "supplier_dtls", "sup_name", "sup_name");
            cmb_dealer_nm.Text = "All";
            ispageLoad = true;
            label7.Text = System.DateTime.Now.ToShortDateString();
            // dataGridView1.DataSource = db.Displaygrid("SELECT  invoice_number as Bill_No ,date as Date,amt as Bill_Amount,paid_amt as Paid_Amount,balance as Balance,status as Status FROM  tbl_purchasemaster ");
            //SELECT        supplier_dtls.sup_name, tbl_purchasemaster.invoice_number, tbl_purchasemaster.amt, tbl_purchasemaster.paid_amt, tbl_purchasemaster.balance,  tbl_purchasemaster.date, tbl_purchasemaster.status FROM            supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.v_id
            dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.],tbl_purchasemaster.date as Date,tbl_purchasemaster.amt as [Total Amount],tbl_purchasemaster.paid_amt as [Paid Amount],tbl_purchasemaster.balance as Balance, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id");

 
        }
        void select_qry()
        {
             if (ispageLoad)
            {
                int flag = 0;

                // string qry = "SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.], tbl_purchasemaster.amt as Amount, tbl_purchasemaster.paid_amt as [Paid Amount], tbl_purchasemaster.balance as [Balance],  tbl_purchasemaster.date as Date, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where ";
                string qry = "SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.],tbl_purchasemaster.date as Date,tbl_purchasemaster.amt as [Total Amount],tbl_purchasemaster.paid_amt as [Paid Amount],tbl_purchasemaster.balance as Balance, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where ";
                if (cmb_dealer_nm.Text != "All")//&& cmb_bill_type.Text!="All" && cmb_bill_status.Text!="All")
                {
                    string v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + cmb_dealer_nm.Text + "'").ToString();
                    if (flag != 0)
                        qry += "and ";

                     qry += "(tbl_purchasemaster.sup_id='" + v_id + "') ";
                    flag++;
                }
                 
                if (cmb_status.Text != "All")
                {

                    if (flag != 0)
                        qry += "and ";
                    qry += " tbl_purchasemaster.status='" + cmb_status.Text + "'";
                    flag++;
                }

                if (chk_date.Checked)
                {
                    string datefrm = dtp1.Value.ToString("MM'-'dd'-'yyyy");
                    string dateto = dtp2.Value.ToString("MM'-'dd'-'yyyy");
                    if (flag != 0)
                        qry += "and ";
                    // name += "tbl_Receipt_Details.chequeDate >='" + dTP1.Text + "' and tbl_Receipt_Details.chequeDate <='" + dTP2.Text + "'"
                    qry += "tbl_purchasemaster.date between '" + datefrm + "' and '" + dateto + "'";
                    flag++;
                }

                 if(flag==0)
                       qry = "SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.],tbl_purchasemaster.date as Date,tbl_purchasemaster.amt as [Total Amount],tbl_purchasemaster.paid_amt as [Paid Amount],tbl_purchasemaster.balance as Balance, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id";

                 dataGridView1.DataSource = db.Displaygrid(qry);
             }
        }

        private void cmb_dealer_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            select_qry();
 
        }

        private void cmb_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_qry();
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            select_qry();
        }

        private void dtp2_ValueChanged(object sender, EventArgs e)
        {
            select_qry();
        }

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            dtp1.Enabled = true;
            dtp2.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pageLoadFun();
        }

        private void txt_bill_TextChanged(object sender, EventArgs e)
        {
            if (txt_bill.Text != "")
            {

                dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.],tbl_purchasemaster.date as Date,tbl_purchasemaster.amt as [Total Amount],tbl_purchasemaster.paid_amt as [Paid Amount],tbl_purchasemaster.balance as Balance, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where tbl_purchasemaster.invoice_number like '" + txt_bill.Text + "%'");

              //   dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.],tbl_purchasemaster.date as Date,tbl_purchasemaster.taxAmount as [Tax Amount],tbl_purchasemaster.discountAmount as [Discount Amt],(tbl_purchasemaster.amt)as [Total Amount], tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where tbl_purchasemaster.invoice_number like '" + txt_bill.Text + "%'");

            }
            else
            {
                dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.],tbl_purchasemaster.date as Date,tbl_purchasemaster.amt as [Total Amount],tbl_purchasemaster.paid_amt as [Paid Amount],tbl_purchasemaster.balance as Balance, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id");

              //  dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.],tbl_purchasemaster.date as Date,tbl_purchasemaster.taxAmount as [Tax Amount],tbl_purchasemaster.discountAmount as [Discount Amt],(tbl_purchasemaster.amt)as [Total Amount], tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where tbl_purchasemaster.invoice_number like '" + txt_bill.Text + "%'");

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cmb_dealer_nm.Text = "All";
               // select_qry();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "PaymentReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    //******  db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName, "Payment Report");

                    MessageBox.Show("File Sucessfully Expoted");

                    // string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Solution.xls");
                    // string filePath = Path.Combine(Environment.GetFolderPath(sfd.Selected), sfd.FileName);

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmb_dealer_nm.Text = "All";
            cmb_status.Text = "All";
            chk_date.Checked = false;
            select_qry();
        }
    }
}
