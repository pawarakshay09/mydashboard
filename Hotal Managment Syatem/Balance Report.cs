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
    public partial class Balance_Report : Form
    {
        Database db = new Database();
        string qry = "";
       
        public Balance_Report()
        {
            InitializeComponent();
        }

         

        private void dtp2_ValueChanged(object sender, EventArgs e)
        {
            string datefrm = dtp1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dtp2.Value.ToString("MM'-'dd'-'yyyy");
            dgv_summary.DataSource = db.Displaygrid("select grp as [Group],sum(paid_amt) as Amount from payment where date between '" + datefrm + "' and '" + dateto + "' group by grp");
            cal();
        }

        private void Balance_Report_Load(object sender, EventArgs e)
        {
            this.CancelButton = button2;
            db.formFix(this);
        }
        public void cal()
        {
            float sum = 0;
            for (int i = 0; i < dgv_summary.RowCount; i++)
            {
                sum += float.Parse(dgv_summary.Rows[i].Cells[1].Value.ToString());
                    
            }
            txt_tot_sale.Text=  sum.ToString();
            float cash = db.getDb_Value("select sum(Total_bill) as Total from total_bill where status='Cash'");
            txt_cash.Text = cash.ToString();
        }

        private void dgv_summary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // dgv_details.DataSource = db.Displaygrid("SELECT  tbl_vendor.v_name, SUM(tbl_purchasemaster.paid_amt) AS Amount FROM   tbl_vendor INNER JOIN  tbl_purchasemaster ON tbl_vendor.v_id = tbl_purchasemaster.v_id GROUP BY tbl_vendor.v_name");
           // bind();
        }

        void bind()
        {
            int k=dgv_summary.SelectedCells[0].RowIndex;
            string value=dgv_summary.Rows[k].Cells[0].Value.ToString();
            string datefrm = dtp1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dtp2.Value.ToString("MM'-'dd'-'yyyy");
            label_DtsRpt.Text = value.ToString();
            switch(value)
            {
                case "Purchase":
                    qry = "SELECT  supplier_dtls.sup_name as [Vendor Name], SUM(tbl_purchasemaster.paid_amt) AS Amount FROM   supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id  where tbl_purchasemaster.date>='" + datefrm + "' and tbl_purchasemaster.date<='" + dateto + "' GROUP BY supplier_dtls.sup_name";
                    break;
                case "Expences":
                    qry = "SELECT   material_nm as [Material Name], amount as Amount FROM tbl_expenses where date>='" + datefrm + "' and date<='" + dateto + "'";
                    break;
                case "Salary":
                    qry = "SELECT   waiter_dtls.wname as[Waiter Name], sum(Waiter_Payment.amount) as Amount FROM  Waiter_Payment INNER JOIN  waiter_dtls ON Waiter_Payment.w_id = waiter_dtls.w_id where date>='" + datefrm + "' and date<='" + dateto + "' group by waiter_dtls.wname";
                    break;
                case "Credit":
                    qry = "SELECT  Custmer.name as  [Customer Name], SUM(total_bill.Total_bill) AS Total FROM            total_bill INNER JOIN  Custmer ON total_bill.Customer_id = Custmer.Customer_id WHERE (total_bill.status = 'Credit') and total_bill.datetime>='"+datefrm+"' and total_bill.datetime<='"+dateto+"' GROUP BY Custmer.name";
                    break;
            }
            dgv_details.DataSource = db.Displaygrid(qry);
            dgv_details.Columns[0].Width = 155;
            dgv_details.Columns[1].Width = 80;
        }

        private void dgv_details_MouseClick(object sender, MouseEventArgs e)
        {
           // bind();
        }

        private void dgv_summary_MouseClick(object sender, MouseEventArgs e)
        {
            bind();
            float tot = 0;
            for (int i = 0; i < dgv_details.RowCount; i++)
            {
                tot += float.Parse(dgv_details.Rows[i].Cells[1].Value.ToString());

            }
            txt_total.Text = tot.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "BalanceReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dgv_details, sfd.FileName);
                   // db.ToCsV(dgv_summary, sfd.FileName);// Here dataGridview1 is your grid view name 
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

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            string datefrm = dtp1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dtp2.Value.ToString("MM'-'dd'-'yyyy");
            dgv_summary.DataSource = db.Displaygrid("select grp as [Group] ,sum(paid_amt) as Amount from payment where date between '" + datefrm + "' and '" + dateto + "' group by grp");
            cal();
        }

         
    }
}
