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
    public partial class DummyBillPrint : Form
    {
        Database db = new Database();
        private List<int> editedCelss = new List<int>();
        private float rate, qty, total_row;
        private int update_flag = 0;
        private int bill_no;
        private int menu_id;
        string getDate;
        string[] orderIdArray = new string[200];
        int billNo;

        public DummyBillPrint()
        {
            InitializeComponent();
        }

        private void DummyBillPrint_Load(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            string tbl_no = (db.getDbstatus_Value("select t_id from dummyTableOrder where order_id='" + lblBill.Text + "' and timeing='" + dtpDate.Value.ToString("MM/dd/yyyy") + "'"));
            LPrinter lp = new LPrinter();
            lp.billDate = dtpDate.Value.ToString("dd/MM/yyyy");
            lp.billtime = db.getDbstatus_Value("select billTime from dummytotal_bill where order_id='" + lblBill.Text + "' and datetime='" + dtpDate.Value.ToString("MM/dd/yyyy") + "'");
            lp.billid = float.Parse(lblBill.Text);
            lp.tableno = (tbl_no.ToString());
            lp.wname = "";
            lp.dummyBill();

        }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            lblBill.Text = txtBillNo.Text;
            if (txtBillNo.Text != "")
            {
                getDate = dtpDate.Value.ToString("MM/dd/yyyy");

                pageLoad();
            }

        }
        void pageLoad()
        {
            getDate = dtpDate.Value.ToString("MM/dd/yyyy");

            if (db.ChkDb_Value("select order_id from dummySalesItem where order_id='" + txtBillNo.Text + "' and orderDate='" + getDate + "'") == true)
            {
                double sum = 0;
                float w_id = float.Parse(db.getDb_Value("select w_id from dummySalesItem where order_id='" + txtBillNo.Text + "' and orderDate='" + getDate + "'").ToString());
                string name = db.getDbstatus_Value("SELECT wname from waiter_dtls where w_id='" + w_id + "' ").ToString();
                lbl_wname.Text = name.ToString();
                dataGridView1.DataSource = db.Displaygrid("SELECT dummySalesItem.order_id, menu.m_name as Menu_Name ,  dummySalesItem.rate as Rate,dummySalesItem.qty as Quantity, dummySalesItem.total_amount as Total,dummySalesItem.menu_id FROM   menu INNER JOIN   dummySalesItem ON menu.menu_id = dummySalesItem.menu_id WHERE(dummySalesItem.order_id = '" + txtBillNo.Text + "') and orderDate='" + getDate + "'");
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 60;
                dataGridView1.Columns[4].Width = 60;
                dataGridView1.Columns[5].Width = 80;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    sum += double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                }
                txt_total.Text = sum.ToString();

                lblDiscValue.Text = db.getDb_Value("select discValue from dummyTableOrder where order_id='" + txtBillNo.Text + "' and timeing='" + getDate + "'").ToString();
                // lbl_disc.Text = db.getDb_Value("select discAmt  from dummyTableOrder where order_id='" + txtBillNo.Text + "' and timeing='" + getDate + "'").ToString();
                lbl_disc.Text = (float.Parse(txt_total.Text) * float.Parse(lblDiscValue.Text) / 100).ToString(); //db.getDb_Value("select discAmt  from dummyTableOrder where order_id='" + txtBillNo.Text + "' and timeing='" + dtpDate.Value.ToString("MM/dd/yyyy") + "'").ToString();

                lbl_grand_tot.Text = Math.Round(float.Parse(txt_total.Text) - float.Parse(lbl_disc.Text)).ToString();
            }
            else
            {
                dataGridView1.DataSource = "";
                lbl_wname.Text = "";

                MessageBox.Show("Bill No Does Not Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            lblBill.Text = txtBillNo.Text;
            if (txtBillNo.Text != "")
            {
                getDate = dtpDate.Value.ToString("MM/dd/yyyy");

                pageLoad();
            }
        }

    }
}
