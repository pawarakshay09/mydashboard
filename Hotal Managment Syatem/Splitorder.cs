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
    public partial class Splitorder : Form
    {
        Database db = new Database();
        string ordrid, date, newid, menuid, sp1qty, rate, old_qty, sp1amt, sp2amt, sp2qty, salesid;

        float item_rate, itemTotal;

        float orderid;

       
        bool pageload=false;
        public Splitorder()
        {
            InitializeComponent();
        }

        private void Splitorder_Load(object sender, EventArgs e)
        {
            db.formFix(this);
           
            db.comboFill(cbprocessing, "select * from table_status where status='" + "Processing" + "'", "table_status", "t_id", "t_id");
            cbprocessing.Text = "Select";
            db.comboFill(cbsplit, "select * from table_status where status='" + "Empty" + "' and section<>'-'", "table_status", "t_id", "t_id");
            cbsplit.Text = "Select";
            pageload = true;

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (cbsplit.Text != "Select")
            {


                orderid = db.getDb_Value("select max(order_id)+1 from table_order");

                newid = orderid.ToString();


                if (newid != "0")
                {
                    DialogResult dlgresult = MessageBox.Show("Are You Sure want to Complete Split Table Order?", "Confirm Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgresult == DialogResult.Yes)
                    {
                        if (dataGridView1.RowCount > 0)
                        {

                            if (!db.ChkDb_Value("select * from table_order where order_id='" + newid + "'"))
                                db.insert("insert into table_order (order_id,t_id,timeing,w_id,order_type,tableStatus) values('" + newid + "','" + cbsplit.Text + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "','61','" + "Novat" + "','Processing') ");


                            db.update("UPDATE table_status SET status='Processing'  WHERE t_id='" + cbsplit.Text + "'");


                            for (int i = 0; i <= dataGridView1.RowCount-1; i++)
                            {
                               // menuid = db.getDb_Value("select menu_id from menu where m_name='" + dataGridView1.Rows[i].Cells["Item Name"].Value.ToString() + "' ").ToString(); //get manu id

                                rate = dataGridView1.Rows[i].Cells["Rate"].Value.ToString();

                                old_qty = sp1qty = dataGridView1.Rows[i].Cells["Qty"].Value.ToString();

                                salesid = dataGridView1.Rows[i].Cells["salesid"].Value.ToString();

                                sp1qty = dataGridView1.Rows[i].Cells["Split_1_Qty"].Value.ToString();

                                sp1amt = dataGridView1.Rows[i].Cells["Split_1_AMT"].Value.ToString();



                                sp2qty = (float.Parse(old_qty) - float.Parse(sp1qty)).ToString();
                                sp2amt = (float.Parse(rate) * float.Parse( sp2qty)).ToString();
                                //sp2qty = dataGridView1.Rows[i].Cells["Split_2_Qty"].Value.ToString();

                                //sp2amt = dataGridView1.Rows[i].Cells["Split_2_AMT"].Value.ToString();



                                if (sp1qty != "0")
                                {

                                    db.insert(@"INSERT INTO [sales_item]([order_id],[menu_id] ,[qty],[rate],[total_amount],[w_id],[kot_id],[Date] )
                                SELECT '" + newid + "',[menu_id] ,'" + sp1qty + "','" + rate + "','" + sp1amt + "' ,[w_id],[kot_id],[Date]  FROM [sales_item] where sales_id='" + salesid + "' and order_id='" + lblorderid.Text + "'");



                                }
                                if (sp2qty != "0")
                                {
                                    db.update("Update  sales_item set qty='" + sp2qty + "',total_amount='" + sp2amt + "' where  sales_id='" + salesid + "' and order_id='" + lblorderid.Text + "'");

                                }

                                if (sp2qty == "0")
                                {
                                    db.update("delete  from  sales_item  where  sales_id='" + salesid + "' and order_id='" + lblorderid.Text + "'");

                                }




                            }




                            //insert table_order id


                        }
                        bind();
                    }
                }
            }
            else
            { MessageBox.Show("Plz Select Split Table No....!"); }
            



        }


        void clear()
        {

        }
        private void cbprocessing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pageload)
            {
                if (cbprocessing.Text != "")
                {
                    ordrid = db.getDbstatus_Value("select max(order_id) from table_order where t_id='" + cbprocessing.Text + "'");
                    lblorderid.Text = ordrid;
                }
                bind();
            }

        }
        void bind()
        {
           // and  timeing='" + Convert.ToDateTime(dateTimePicker1.Text).ToString("MM/dd/yyyy") + "'

            if (db.ChkDb_Value("select * from table_order where order_id='" + lblorderid.Text + "'"))
            {
                dataGridView1.DataSource = db.Displaygrid(" SELECT  menu.m_name AS [Item Name],  sales_item.qty AS Qty, sales_item.rate AS Rate,sales_item.total_amount  AS Amount ,sales_item.sales_id as salesid,'0' as [Split_1_Qty], '0' as [Split_1_AMT],'0' as [Split_2_Qty],'0' as [Split_2_AMT] FROM menu INNER JOIN   sales_item  ON menu.menu_id = sales_item.menu_id and sales_item.order_id = '" + lblorderid.Text + "'");
                dataGridView1.Columns[0].Width = 240;
                dataGridView1.Columns["salesid"].Visible = false;
               


                 //foreach (DataGridViewRow row in dataGridView1.Rows)
                 //{
                 //    row.Cells[4].Value = "Move";
                 //}

            }
            else
            {
                MessageBox.Show("Plz Check Table Order and Date...!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
          


            string old_qty, sp1qty, mname, rate, amt,sp2qty;
            double newqty,sp1AMT,sp2AMT;

            if (e.ColumnIndex == 5)
            {



                //for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                //{

                    int i = dataGridView1.SelectedCells[0].RowIndex;

                    old_qty = dataGridView1.Rows[i].Cells["QTY"].Value.ToString();

                    rate = dataGridView1.Rows[i].Cells["Rate"].Value.ToString();

                    sp1qty = dataGridView1.Rows[i].Cells["Split_1_Qty"].Value.ToString();

                    sp1AMT = double.Parse(rate.ToString()) * double.Parse(sp1qty.ToString());

                    dataGridView1.Rows[i].Cells["Split_1_AMT"].Value = sp1AMT;


                    newqty = double.Parse(old_qty.ToString()) - double.Parse(sp1qty.ToString());

                    dataGridView1.Rows[i].Cells["Split_2_Qty"].Value = newqty;


                    sp2qty = dataGridView1.Rows[i].Cells["Split_2_Qty"].Value.ToString();

                    sp2AMT = double.Parse(rate.ToString()) * double.Parse(sp2qty.ToString());

                    dataGridView1.Rows[i].Cells["Split_2_AMT"].Value = sp2AMT;
                    cal();


                //}
            }
        }

        void cal()
        {
            int total = dataGridView1.Rows.Cast<DataGridViewRow>()
           .Sum(t => Convert.ToInt32(t.Cells["Amount"].Value));

            lbltotal.Text = total.ToString();

            int sp1amt = dataGridView1.Rows.Cast<DataGridViewRow>()
           .Sum(t => Convert.ToInt32(t.Cells["Split_1_AMT"].Value));

            lblsp1.Text = sp1amt.ToString();

            int sp2amt = dataGridView1.Rows.Cast<DataGridViewRow>()
         .Sum(t => Convert.ToInt32(t.Cells["Split_2_AMT"].Value));

            lblsp2.Text = sp2amt.ToString();

        }
    }
}
