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
    public partial class Receipt : Form
    {
        float TotalCreditAmount = 0;
        Database db = new Database();
        public Receipt()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clearText()
        {
            txtAmount.Text = "0";
            txt_balance.Text = "0";
            txt_paid.Text = "0";
            label_customerName.Text = "";
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                MessageBox.Show("Please Enter Amount");
            }
            else
            {
                float c_id=0;
                c_id = db.getDb_Value("select Customer_id from Custmer where name='" + label_customerName.Text + "'");
                //db.insert("insert into tbl_Cust_Receipt values('" + txt_paid.Text + "','" + dateTimePicker1.Text + "','"+c_id+"')");
                string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                db.insert("insert into tbl_Cust_Receipt(PaidAmt,PaidDate,Customer_id,totalBill,CustomerName) values('" + txt_paid.Text + "','" + date + "','" + c_id + "','" + "0" + "','" + label_customerName.Text + "')");
                db.insert("insert into total_bill(order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark) values('200','" + txt_paid.Text + "','" + date + "','"+ cbpayment.Text +"','" + c_id + "','0','0')");

                MessageBox.Show("Record Saved Successfully","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                clearText();
               // dataGridView1.DataSource = db.Displaygrid("SELECT Custmer.name, SUM(total_bill.Total_bill) AS TotalAmount, SUM(tbl_Cust_Receipt.PaidAmt) AS PaidAmount, SUM(total_bill.Total_bill) - SUM(tbl_Cust_Receipt.PaidAmt) AS BalanceAmount FROM  Custmer INNER JOIN tbl_Cust_Receipt ON Custmer.Customer_id = tbl_Cust_Receipt.Customer_id INNER JOIN total_bill ON Custmer.Customer_id = total_bill.Customer_id GROUP BY total_bill.Customer_id, tbl_Cust_Receipt.Customer_id, Custmer.name");
               databind();
            }
        }

        private void recipt_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button3;
            databind();
            

        }
        public void databind()
        {
            string total = "0";
            TotalCreditAmount = 0;
         //   dataGridView1.DataSource = db.Displaygrid("SELECT Custmer.name, SUM(total_bill.Total_bill) AS TotalAmount, SUM(tbl_Cust_Receipt.PaidAmt) AS PaidAmount, SUM(total_bill.Total_bill) - SUM(tbl_Cust_Receipt.PaidAmt) AS BalanceAmount FROM  Custmer INNER JOIN tbl_Cust_Receipt ON Custmer.Customer_id = tbl_Cust_Receipt.Customer_id INNER JOIN total_bill ON Custmer.Customer_id = total_bill.Customer_id GROUP BY total_bill.Customer_id, tbl_Cust_Receipt.Customer_id, Custmer.name ");
            /*SELECT        CustomerName, SUM(totalBill) AS TotalAmount, SUM(PaidAmt) AS PaidAmount, SUM(totalBill) - SUM(PaidAmt) AS BalanceAmount
FROM            tbl_Cust_Receipt
GROUP BY CustomerName, totalBill*/
           // dataGridView1.DataSource = db.Displaygrid("SELECT CustomerName as[Customer Name], SUM(totalBill) AS Total, SUM(PaidAmt) AS Paid, SUM(totalBill) - SUM(PaidAmt) AS Balance FROM   tbl_Cust_Receipt GROUP BY CustomerName");
            dataGridView1.DataSource = db.Displaygrid("  select  [Customer Name],Total,Paid,Balance from (SELECT CustomerName as[Customer Name], SUM(totalBill) AS Total, SUM(PaidAmt) AS Paid, SUM(totalBill) - SUM(PaidAmt) AS Balance  FROM  tbl_Cust_Receipt GROUP BY CustomerName ) A where Balance <> 0");
         
            dataGridView1.Columns[0].Width = 210;
            for (int i = 0; i < dataGridView1.RowCount ; i++)
            {
                total = dataGridView1.Rows[i].Cells[3].Value.ToString();

                TotalCreditAmount += float.Parse(total);
            }
            label_totalCredit.Text = TotalCreditAmount.ToString();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            label_customerName.Text=dataGridView1.Rows[selectedRow].Cells[0].Value.ToString();
            txtAmount.Text= dataGridView1.Rows[selectedRow].Cells[3].Value.ToString();
            txt_balance.Text = txtAmount.Text;
        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {

            if (txt_paid.Text != "")
            {
                if (float.Parse(txt_paid.Text) > float.Parse(txtAmount.Text))
                {
                    MessageBox.Show("Paid Amount Should be Less Than Total Amount");
                    btnSave.Enabled = false;
                }
                else
                {
                    txt_balance.Text = (float.Parse(txtAmount.Text) - float.Parse(txt_paid.Text)).ToString();
                    btnSave.Enabled = true;
                }
            }
            else
            {
                txt_paid.Text = "0";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Receipt_Dtls dtl = new Receipt_Dtls(label_customerName.Text);
            dtl.ShowDialog();
        }

        private void txt_paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }
    }
}
