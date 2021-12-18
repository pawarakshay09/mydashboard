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
    public partial class Parcel_order_Details : Form
    {
        Database db = new Database();
        string todayDate;
        public Parcel_order_Details()
        {
            InitializeComponent();
        }

        private void Parcel_order_Details_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where tbl_parcel_order.order_status='Deliver'");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 155;
            dataGridView1.Columns[2].Width = 85;
            dataGridView1.Columns[3].Width = 69;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 120;
            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string c_id = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string status = dataGridView1.Rows[i].Cells[5].Value.ToString();
                db.update("update tbl_parcel_order set order_status='" + status + "' where Customer_id='" + c_id + "'");
                MessageBox.Show("Record Updated Successfully");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                 DialogResult dlgresult = MessageBox.Show("Are you sure want to Pay this order?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dlgresult == DialogResult.Yes)
                 {
                     int i = dataGridView1.SelectedCells[0].RowIndex;
                     int cust_id = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                     string wname=dataGridView1.Rows[i].Cells[6].Value.ToString();
                     
                      db.update("update tbl_parcel_order set w_name='" + wname + "',order_status='" + "Paid" + "' where Customer_id='" + cust_id + "'");
                     MessageBox.Show("Paid Successfully ");
                     dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where tbl_parcel_order.order_status='Deliver'");
                     dataGridView1.Columns[0].Visible = false;

                 }

            }
            if (e.ColumnIndex == 1)
            {
                DialogResult dlgresult = MessageBox.Show("Are you sure want to Cancel this order?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {
                    int i = dataGridView1.SelectedCells[0].RowIndex;
                    int cust_id = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    string wname = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    
                    db.update("update tbl_parcel_order set w_name='" + wname + "',order_status='" + "Cancel" + "' where Customer_id='" + cust_id + "'");
                    MessageBox.Show("Order Cancel Successfully ");
                    dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where tbl_parcel_order.order_status='Deliver'");
                    dataGridView1.Columns[0].Visible = false;

                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (dataGridView1.CurrentCell.ColumnIndex == 4)
            //{
            //    TextBox txt = e.Control as TextBox;
            //    db.autosuggest_Cell("waiter_dtls", "wname", txt);

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string c_id = dataGridView1.Rows[i].Cells[0].Value.ToString();
              //  string status = dataGridView1.Rows[i].Cells[3].Value.ToString();
                string wname = dataGridView1.Rows[i].Cells[4].Value.ToString();
                db.update("update tbl_parcel_order set w_name='" + wname + "' where Customer_id='" + c_id + "'");
            
            }
            MessageBox.Show("Waiter Assigned Successfully");
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int i = dataGridView1.SelectedCells[0].RowIndex;
            //string c_name = dataGridView1.Rows[i].Cells[3].Value.ToString();
            //string order_no = dataGridView1.Rows[i].Cells[4].Value.ToString();
            //string w_name = dataGridView1.Rows[i].Cells[6].Value.ToString();
            //Parcel_Detail_list list = new Parcel_Detail_list(c_name, order_no,w_name);
            //list.ShowDialog();
            //dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id ");
            //dataGridView1.Columns[0].Visible = false;
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
             todayDate = dtp_date.Value.ToString("MM-dd-yyyy");
            dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where tbl_parcel_order.order_status='Deliver' and tbl_parcel_order.date='"+todayDate+"'");

        }

        private void cmb_status_SelectedIndexChanged(object sender, EventArgs e)
        {
             todayDate = dtp_date.Value.ToString("MM-dd-yyyy");
            if(cmb_status.Text!="All")

            dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where tbl_parcel_order.date='" + todayDate + "' and tbl_parcel_order.order_status='"+cmb_status.Text+"'");
            else

                dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where  tbl_parcel_order.date='" + todayDate + "' ");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where tbl_parcel_order.order_status='Deliver'");
            dataGridView1.Columns[0].Visible = false;

        }
         
        
    }
}
