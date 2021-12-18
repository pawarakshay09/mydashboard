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
    public partial class CRT_Insentive_dtls : Form
    {
        Database db=new Database();
        
        public CRT_Insentive_dtls()
        {
            InitializeComponent();
        }

        private void CRT_Insentive_dtls_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnClose;
            dtpFrom.Enabled = true;
            dtpTo.Enabled = true;
            db.comboFill(cmbWaiter, "select * from waiter_dtls","waiter_dtls","wname","wname");
            cmbWaiter.Text = "All";
            bindQry();



            gridFormation();
            cal();
        }

        void reduceCancleItemQty()
        {
            string getQty,mName;
            db.cnopen();
            SqlCommand cmd = new SqlCommand("SELECT        menu.m_name, sales_item.qty, Cancel_order.order_id FROM  Cancel_order INNER JOIN   sales_item ON Cancel_order.order_id = sales_item.order_id INNER JOIN  menu ON sales_item.menu_id = menu.menu_id ", db.cn);//AND Cancel_order.m_name = menu.m_name
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                mName = rd["m_name"].ToString();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if(dataGridView1.Rows[i].Cells[1].Value.ToString()==rd["m_name"].ToString())
                    {
                        getQty = (float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()) - float.Parse(rd["qty"].ToString())).ToString();
                        dataGridView1.Rows[i].Cells[2].Value = getQty;
                        dataGridView1.Rows[i].Cells[4].Value =float.Parse(getQty) * float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    }
                }
            }
        }
        void gridFormation()
        {
            dataGridView1.Columns[0].Width = 180;
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            bindQry();
        }
        void dateChange()
        {
            string datefrm = dtpFrom.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dtpTo.Value.ToString("MM'-'dd'-'yyyy");
            dataGridView1.DataSource = db.Displaygrid("SELECT   waiter_dtls.wname AS [Captain Name], menu.m_name as [Menu Name], SUM(sales_item.qty) AS Qty, menu.insentive_rate AS [Intensive Rate], SUM(sales_item.qty)  * menu.insentive_rate AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  waiter_dtls ON sales_item.w_id = waiter_dtls.w_id INNER JOIN    total_bill ON sales_item.order_id = total_bill.order_id WHERE        (menu.insentive_rate <> 0) AND total_bill.datetime between '" + datefrm + "' and '" + dateto + "' GROUP BY menu.m_name, waiter_dtls.wname, menu.insentive_rate ");
            gridFormation();
            cal();
        }
        void bindQry()
        {
            string datefrm = dtpFrom.Value.ToString("MM/dd/yyyy");
            string dateto = dtpTo.Value.ToString("MM/dd/yyyy");
            string qry = "SELECT   waiter_dtls.wname AS [Captain Name], menu.m_name as [Menu Name], SUM(sales_item.qty) AS Qty, menu.insentive_rate AS [Intensive Rate], SUM(sales_item.qty)  * menu.insentive_rate AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  waiter_dtls ON sales_item.w_id = waiter_dtls.w_id  INNER JOIN  table_order ON sales_item.order_id = table_order.order_id WHERE        (menu.insentive_rate <> 0) AND  (table_order.t_id NOT LIKE 'P%') AND (sales_item.order_id NOT IN  (SELECT order_id   FROM Cancel_order  WHERE   (date between '" + datefrm + "' and '" + dateto + "'))) and ";
            string grpBy = " GROUP BY menu.m_name, waiter_dtls.wname, menu.insentive_rate ";
            int flag = 0;
            if (cmbWaiter.Text != "All")
            {
                if (flag != 0)
                    qry += " and ";
                qry += " waiter_dtls.wname='" + cmbWaiter.Text + "'";
                flag++;
            }
            if (chkDate.Checked)
            {
                if (flag != 0)
                    qry += " and ";
                qry += " table_order.timeing between '" + datefrm + "' and '" + dateto + "'";
                flag++;
            }
            if (flag == 0)
                dataGridView1.DataSource = db.Displaygrid("SELECT   waiter_dtls.wname AS [Captain Name], menu.m_name as [Menu Name], SUM(sales_item.qty) AS Qty, menu.insentive_rate AS [Intensive Rate], SUM(sales_item.qty)  * menu.insentive_rate AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  waiter_dtls ON sales_item.w_id = waiter_dtls.w_id INNER JOIN table_order ON sales_item.order_id = table_order.order_id WHERE        (menu.insentive_rate <> 0) and   (table_order.t_id NOT LIKE 'P%') GROUP BY menu.m_name, waiter_dtls.wname, menu.insentive_rate ");
            else
                dataGridView1.DataSource = db.Displaygrid(qry + grpBy);
            gridFormation();
           // reduceCancleItemQty();
            cal();

        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            bindQry();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "Insentive_Report.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void cal()
        {
            float sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum+=float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }
            lblTotal.Text = sum.ToString();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
            bindQry();
        }

        private void cmbWaiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindQry();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbWaiter.Text = "All";
            chkDate.Checked = false;
            bindQry();

        }
    }
}
