using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Hotal_Managment_Syatem
{
    public partial class Bill_Details : Form
    {
        Database db = new Database();
        public string bill_no,sup_nm,status,date;
        public float amount;
        public Bill_Details(string bill_no,string sup_nm,string status,string date,float amount)
        {
            InitializeComponent();
            this.bill_no = bill_no;
            this.sup_nm = sup_nm;
            this.status = status;
            this.date = date;
            this.amount = amount;
        }

        private void Bill_Details_Load(object sender, EventArgs e)
        {
           // panel1.Visible = true;
            db.formFix(this);
            this.CancelButton = btn_close;
            lbl_bill_no.Text = bill_no;
            lbl_dealernm.Text = sup_nm;
            txt_date.Text = date;

            //get the bill details from the purchase master table 
            SqlCommand cmd = new SqlCommand("select * from tbl_purchasemaster where invoice_number='" + bill_no + "'", db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txt_amount.Text = rd["amt"].ToString();
                textBoxTaxAmount.Text = rd["taxAmount"].ToString();
                textBoxDiscount.Text = rd["discountAmount"].ToString();
                txt_status.Text = rd["status"].ToString();
            }
            db.cnclose();

            //show the product details into the DGV
            dataGridView1.DataSource = db.Displaygrid("SELECT  tbl_PurchaseDetail.item_name as [Item Name], tbl_PurchaseDetail.qty as Qty, tbl_PurchaseDetail.unit as Unit, tbl_PurchaseDetail.Amount as [Amount], tbl_purchasemaster.date as [Dtae], tbl_purchasemaster.invoice_number as [Bill No] FROM            tbl_PurchaseDetail INNER JOIN  tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid WHERE (tbl_purchasemaster.invoice_number = '" + lbl_bill_no.Text + "')");
        }


        private void btn_show_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT  tbl_PurchaseDetail.item_name, tbl_PurchaseDetail.qty, tbl_PurchaseDetail.unit, tbl_PurchaseDetail.Amount, tbl_purchasemaster.date, tbl_purchasemaster.invoice_number FROM            tbl_PurchaseDetail INNER JOIN  tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid WHERE (tbl_purchasemaster.invoice_number = '" + txt_bill_no.Text + "')");

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "BillDetailReport'"+label5.Text+"'.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Expoted");


                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete Record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                //1st delete from the purchase  details table 
                db.DeleteData("delete from tbl_PurchaseDetail where pur_id=(select purchasemasterid from tbl_purchasemaster where invoice_number='" + bill_no + "' )", "tbl_PurchaseDetail");
                //2nd delete from the master purchse table 
                db.DeleteData("delete from tbl_purchasemaster where invoice_number='" + bill_no + "'", "tbl_purchasemaster");
            }
            this.Close();
        }

        private void txt_bill_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_bill_no_TextChanged(object sender, EventArgs e)
        {
            if(txt_bill_no.Text!="")
               dataGridView1.DataSource = db.Displaygrid("SELECT  tbl_PurchaseDetail.item_name, tbl_PurchaseDetail.qty, tbl_PurchaseDetail.unit, tbl_PurchaseDetail.Amount, tbl_purchasemaster.date, tbl_purchasemaster.invoice_number FROM            tbl_PurchaseDetail INNER JOIN  tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid WHERE (tbl_purchasemaster.invoice_number = '" + txt_bill_no.Text + "')");
           

        }
    }
}
