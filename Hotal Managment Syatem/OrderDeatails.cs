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
    public partial class OrderDeatails : Form
    {
        Database db = new Database();
        int billNo;
        double sum = 0;
        public OrderDeatails()
        {
            InitializeComponent();
        }
        public OrderDeatails(int o_id)
        {
            InitializeComponent();
            this.billNo = o_id;
        }
        private void OrderDeatails_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
          //  dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name as [Menu Name], sales_item.qty as Qty, sales_item.rate as Rate, sales_item.total_amount as [Total Amount] FROM            menu INNER JOIN    sales_item ON menu.menu_id = sales_item.menu_id WHERE        (sales_item.order_id = '"+billNo+"')");
            dataGridView1.DataSource = db.Displaygrid("SELECT menuName as [Menu Name], qty as Qty, rate as Rate, sum(amount) as [Total Amount] FROM  CancelKOTDetails   WHERE   orderid = '" + billNo + "' group by menuName,qty,rate,amount"); 
           
            
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 50;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum+=double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }
            txt_total.Text = sum.ToString();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
