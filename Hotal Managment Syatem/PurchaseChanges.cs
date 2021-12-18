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
    public partial class PurchaseChanges : Form
    {
        Database db = new Database();
        double  sum = 0;
        int count=0;
        public PurchaseChanges()
        {
            InitializeComponent();
        }

        private void PurchaseChanges_Load(object sender, EventArgs e)
        {
            //this.CancelButton = btnclose;
            //DataSet ds = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter("select  bill_no  from invoice_dtls order by bill_no desc", db.cn);
            //da.Fill(ds, "invoice_dtls");
            //dataGridView1.DataSource = db.Displaygrid("SELECT invoice_dtls.bill_no as BillNo,invoice_dtls.bill_date as BillDate,purchase_entry.qty as Total_Item,invoice_dtls.g_total as Total_Amount,invoice_dtls.bill_status as Status FROM invoice_dtls INNER JOIN purchase_entry ON invoice_dtls.invoice_id = purchase_entry.invoice_id");
            //txtInvoiceNo.DataSource = ds.Tables[0].DefaultView;
            //txtInvoiceNo.DisplayMember = "bill_no";
            this.CancelButton = btnclose;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select  invoice_number  from tbl_purchasemaster order by invoice_number desc", db.cn);
            da.Fill(ds, "tbl_purchasemaster");
            dataGridView1.DataSource = db.Displaygrid("SELECT tbl_purchasemaster.invoice_number as Bill_No, tbl_purchasemaster.date as BillDate, tbl_PurchaseDetail.qty as Total_Item,  tbl_purchasemaster.amt as Amount,tbl_purchasemaster.status as Status FROM  tbl_PurchaseDetail INNER JOIN  tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid");
            txtInvoiceNo.DataSource = ds.Tables[0].DefaultView;
            txtInvoiceNo.DisplayMember = "invoice_number";
        //     db.comboFill(txtInvoiceNo,"SELECT tbl_purchasemaster.invoice_number as Bill_No, tbl_purchasemaster.date as BillDate, tbl_PurchaseDetail.qty as Total_Item,  tbl_purchasemaster.amt as Amount,tbl_purchasemaster.status as Status FROM  tbl_PurchaseDetail INNER JOIN  tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid","")            
            db.cnclose();
        }
        public void cal()
        {
            txtTotal.Text = "";
            txtItamNo.Text = "";
            txtTotal.Text = 0.ToString();
            count = 0;
            sum = 0;
            for(int i=0;i<dgv.Rows.Count;i++)
            {
            sum += Convert.ToDouble(dgv.Rows[i].Cells[1].Value.ToString());
            count++;
            }
            txtTotal.Text = sum.ToString();
            txtItamNo.Text = count.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtInvoiceNo.Text!="")
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT item_name as[Name], qty as[Qty] FROM  tbl_PurchaseDetail ", db.cn);//where invoice_id='" + txtInvoiceNo.Text + "'
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_PurchaseDetail");
            dgv.DataSource =ds.Tables[0].DefaultView;
            cal();

                //..................
            db.cnclose();
            SqlCommand cmd = new SqlCommand("SELECT tbl_purchasemaster.invoice_number as BillNo, tbl_purchasemaster.date as BillDate, tbl_PurchaseDetail.qty as Total_Item,  tbl_purchasemaster.amt as Amount,tbl_purchasemaster.status as Status FROM  tbl_PurchaseDetail INNER JOIN  tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid WHERE (tbl_purchasemaster.invoice_number ='" + txtInvoiceNo.Text + "')", db.cn);
            db.cnopen();                   
            SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    dateTimePicker1.Text = dr["BillDate"].ToString();
                    txtStatus.Text = dr["Status"].ToString();
                    txtBillNo.Text = dr["BillNo"].ToString();
                    txtItamNo.Text=dr["Total_Item"].ToString();
                    txtTotal.Text = dr["Amount"].ToString();

                }
                dataGridView1.DataSource = db.Displaygrid("SELECT tbl_purchasemaster.invoice_number as BillNo, tbl_purchasemaster.date as BillDate, tbl_PurchaseDetail.qty as Total_Item,  tbl_purchasemaster.amt as Amount,tbl_purchasemaster.status as Status FROM  tbl_PurchaseDetail INNER JOIN  tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid WHERE (tbl_purchasemaster.invoice_number ='" + txtInvoiceNo.Text + "')");

                db.cnclose();
               
                //.............................
            }
            else 
            {
                MessageBox.Show("Please enter invoice number","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
