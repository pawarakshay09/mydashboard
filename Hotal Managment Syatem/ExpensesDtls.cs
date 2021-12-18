using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    public partial class ExpensesDtls : Form
    {
        //string constr = @"Data Source=ABMS-PC;Initial Catalog=medicalDB;Persist Security Info=True;User ID=sa;Password=admin@2012";
        Database db = new Database();
        public string dateFrm, dateTo;
        public ExpensesDtls()
        {
            InitializeComponent();
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            string datefrm = date_from.Value.ToString("MM-dd-yyyy");
            string dateto = to_date.Value.ToString("MM-dd-yyyy");
            try
            {
                dataGridView1.DataSource = db.Displaygrid("select material_nm as [Material Name],qty as Qty,unit as Unit,amount as Amount,date as Date,remark As Remark from tbl_expenses where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
                dataGridView1.Columns[0].Width = 180;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExpensesDtls_Load(object sender, EventArgs e)
        {
            this.CancelButton = button1;
            db.formFix(this);
            label5.Text = System.DateTime.Now.ToShortDateString();
            db.comboFill(cmb_category, "select distinct(material_nm) from tbl_expenses", "tbl_expenses", "material_nm", "material_nm");
            cmb_category.Text = "All";
            dataGridView1.DataSource = db.Displaygrid("select material_nm as [Material Name],amount as Amount,date as Date,remark As Remark from tbl_expenses where date ='" + Convert.ToDateTime(label5.Text).ToString("MM/dd/yyyy") + "'");//where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
            dataGridView1.Columns[1].Width = 180;
            //  dataGridView1.DataSource = db.Displaygrid("select * from tbl_expenses");
             cal();
           // dataGridView1.Columns[0].Visible = false;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "ExpensesReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns[0].Index)
            {
                string amt;

                int i = dataGridView1.SelectedCells[0].RowIndex;

                string name = dataGridView1.Rows[i].Cells[1].Value.ToString();

                amt = dataGridView1.Rows[i].Cells[2].Value.ToString();
               string date =(Convert.ToDateTime(label5.Text).ToString("MM/dd/yyyy"));
               // string wid = db.getDbstatus_Value("select w_id from waiter_dtls where wname='" + itemName + "'");

                DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete from tbl_expenses where material_nm='" + name + "' and amount='" + amt + "' and date='" + date + "'", db.cn);
                    try
                    {

                        cmd.ExecuteScalar();
                        MessageBox.Show("Entry Deleted");
                        // bind();
                        //dataGridView1.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name],waiter_prsenty.p_id, waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");
                        dataGridView1.DataSource = db.Displaygrid("select material_nm as [Material Name],amount as Amount,date as Date,remark As Remark from tbl_expenses where date ='" + Convert.ToDateTime(label5.Text).ToString("MM/dd/yyyy") + "'");//where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
                        dataGridView1.Columns[1].Width = 180;
                        cal();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error  " + ex.Message);
                    }

                }
            }
        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        private void date_from_ValueChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //dataGridView1.DataSource = db.Displaygrid("select * from tbl_expenses");
            dataGridView1.DataSource = db.Displaygrid("select material_nm as [Material Name],amount as Amount,date as Date,remark As Remark from tbl_expenses");// where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
            dataGridView1.Columns[0].Width = 180;
            cmb_category.Text = "All";
            chkDate.Checked = false;
            cal();
         //   dataGridView1.Columns[0].Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LPrinter lp = new LPrinter();
            dateFrm = date_from.Value.ToString("MM-dd-yy");
            dateTo = to_date.Value.ToString("MM-dd-yy");
            lp.GrandTot = float.Parse(txt_total.Text);
            lp.print_Expences(dateFrm, dateTo);
        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                date_from.Enabled = true;
                to_date.Enabled = true;
            }
            else
            {
                date_from.Enabled = false;
                to_date.Enabled = false;
            }
        }
        void bindGrid()
        {
            string qry = "select material_nm as [Material Name],amount as Amount,date as Date,remark As Remark from tbl_expenses where ";
            int flag = 0;
            if (cmb_category.Text != "All")
            {
                if(flag!=0)
                  qry += " and";
                qry += " material_nm='"+cmb_category.Text+"'";
                flag++;
            }
            if (chkDate.Checked)
            {
                dateFrm = date_from.Value.ToString("MM-dd-yyyy");
                dateTo = to_date.Value.ToString("MM-dd-yyyy");
                if (flag != 0)
                    qry += " and";
                qry += " date between '" + dateFrm + "' and '"+dateTo+"'";
                flag++;
            }
            dataGridView1.DataSource = db.Displaygrid(qry);
            dataGridView1.Columns[1].Width = 180;
            cal();
        }

        private void to_date_ValueChanged(object sender, EventArgs e)
        {
            bindGrid();
        }
        void cal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum +=double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            txt_total.Text = sum.ToString();
        }
    }
}
