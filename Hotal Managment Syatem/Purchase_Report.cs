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
    
    public partial class Purchase_Report : Form
    {
        Database db = new Database();
        bool ispageLoad = false;
        public Purchase_Report()
        {
            InitializeComponent();
        }

        private void Purchase_Report_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            pageLoadFun();
        }
        void pageLoadFun()
        {
            db.comboFill(cmb_dealer_nm, "select * from supplier_dtls", "supplier_dtls", "sup_name", "sup_name");
            cmb_dealer_nm.Text = "All";
            cmb_status.Text = "All";
            ispageLoad = true;
            label7.Text = System.DateTime.Now.ToShortDateString();
            // dataGridView1.DataSource = db.Displaygrid("SELECT  invoice_number as Bill_No ,date as Date,amt as Bill_Amount,paid_amt as Paid_Amount,balance as Balance,status as Status FROM  tbl_purchasemaster ");
            //SELECT        supplier_dtls.sup_name, tbl_purchasemaster.invoice_number, tbl_purchasemaster.amt, tbl_purchasemaster.paid_amt, tbl_purchasemaster.balance,  tbl_purchasemaster.date, tbl_purchasemaster.status FROM            supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.v_id

            dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.], tbl_purchasemaster.amt as Amount, tbl_purchasemaster.paid_amt as [Paid Amount], tbl_purchasemaster.balance as Balance,  tbl_purchasemaster.date as Date, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id");
            dataGridView1.Columns[6].Width = 70;
            dataGridView1.Columns[0].Width = 170;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[4].Width = 85;
            dataGridView1.Columns[5].Width = 80;
       
        }
       
        void select_qry()
        {
           
            if (ispageLoad)
            {
                int flag = 0;

                string qry = "SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.], tbl_purchasemaster.amt as Amount, tbl_purchasemaster.paid_amt as [Paid Amount], tbl_purchasemaster.balance as Balance,  tbl_purchasemaster.date as Date, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where ";
                if (cmb_dealer_nm.Text != "All")//&& cmb_bill_type.Text!="All" && cmb_bill_status.Text!="All")
                {
                    string v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + cmb_dealer_nm.Text + "'").ToString();


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
                     qry += "tbl_purchasemaster.date>='" + datefrm + "' and tbl_purchasemaster.date<='" + dateto + "'";
                    flag++;
                }

               if(flag==0)
                         qry = "SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.], tbl_purchasemaster.amt as Amount, tbl_purchasemaster.paid_amt as [Paid Amount], tbl_purchasemaster.balance as Balance,  tbl_purchasemaster.date as Date, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id";

                 dataGridView1.DataSource = db.Displaygrid(qry);
                 dataGridView1.Columns[6].Width = 70;
                 dataGridView1.Columns[0].Width = 170;
                 dataGridView1.Columns[1].Width = 80;
                 dataGridView1.Columns[4].Width = 85;
                 dataGridView1.Columns[5].Width = 80;
             }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "PurchaseReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                  //******  db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    db.withReportTitle_ToCsV(dataGridView1,sfd.FileName,"Purches Report ");

                    MessageBox.Show("File Sucessfully Exported");

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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            string bill_no = "0";
            if(cmb_dealer_nm.Text!="All")
                bill_no = dataGridView1.Rows[i].Cells[1].Value.ToString();
            else
                bill_no = dataGridView1.Rows[i].Cells[1].Value.ToString();

          string sup_nm=dataGridView1.Rows[i].Cells[0].Value.ToString();
           // string sup_nm = cmb_dealer_nm.Text;
             string status=dataGridView1.Rows[i].Cells[6].Value.ToString();
             string date=dataGridView1.Rows[i].Cells[5].Value.ToString();
            float amount=float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());

            Bill_Details bill_dtls = new Bill_Details(bill_no,sup_nm,status,date,amount);
            bill_dtls.ShowDialog();
            pageLoadFun();

        }
       
       

        private void cmb_dealer_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Bind_DGV();
            select_qry();
        }

        private void cmb_bill_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Bind_DGV();
            select_qry();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmb_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///Bind_DGV();
            select_qry();
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
          //  Bind_DGV();
            select_qry();
        }

        private void dtp2_ValueChanged(object sender, EventArgs e)
        {
           // Bind_DGV();
            select_qry();
        }

        private void txt_bill_TextChanged(object sender, EventArgs e)
        {
            if (txt_bill.Text != "")
            {
               // dataGridView1.DataSource = db.Displaygrid("select invoice_number as Bill_no,amt as Amount,paid_amt as Paid_Amount,balance as Balance,date as Date from tbl_purchasemaster where invoice_number like '" + txt_bill.Text + "%'");
                dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.], tbl_purchasemaster.amt as Amount, tbl_purchasemaster.paid_amt as [Paid Amount], tbl_purchasemaster.balance as Balance,  tbl_purchasemaster.date as Date, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where tbl_purchasemaster.invoice_number like '" + txt_bill.Text + "%'");
                dataGridView1.Columns[6].Width = 70;
                dataGridView1.Columns[0].Width = 170;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[4].Width = 85;
                dataGridView1.Columns[5].Width = 80;
            }
            else
            {
               // dataGridView1.DataSource = db.Displaygrid("select invoice_number as Bill_no,amt as Amount,paid_amt as Paid_Amount,balance as Balance,date as Date from tbl_purchasemaster");// where invoice_number='" + txt_bill.Text + "'");
                dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.], tbl_purchasemaster.amt as Amount, tbl_purchasemaster.paid_amt as [Paid Amount], tbl_purchasemaster.balance as Balance,  tbl_purchasemaster.date as Date, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id ");
                dataGridView1.Columns[6].Width = 70;
                dataGridView1.Columns[0].Width = 170;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[4].Width = 85;
                dataGridView1.Columns[5].Width = 80;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date.Checked)
            {
                dtp1.Enabled = true;
                dtp2.Enabled = true;
            }
            else
            {
                dtp1.Enabled = false;
                dtp2.Enabled = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //dataGridView1.Columns[0].Visible = false;
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmb_status.Text = "All";
            cmb_dealer_nm.Text = "All";
            chk_date.Checked = false;
            txt_bill.Text = "";
           // select_qry();
            dataGridView1.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name as [Supplier Name], tbl_purchasemaster.invoice_number as [Invoice No.], tbl_purchasemaster.amt as Amount, tbl_purchasemaster.paid_amt as [Paid Amount], tbl_purchasemaster.balance as Balance,  tbl_purchasemaster.date as Date, tbl_purchasemaster.status as Status FROM supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id");
            dataGridView1.Columns[6].Width = 70;
            dataGridView1.Columns[0].Width = 170;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[4].Width = 85;
            dataGridView1.Columns[5].Width = 80;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cmb_dealer_nm.Text = "All";
                // select_qry();
            }
        }

        private void txt_bill_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }
    }
}
