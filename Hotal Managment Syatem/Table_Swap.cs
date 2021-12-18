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

namespace Hotal_Managment_Syatem
{
    public partial class Table_Swap : Form
    {
        Database db = new Database();
        Welcome _wel;
        bool flag = false;
        public Table_Swap()
        {
            InitializeComponent();
        }
        public Table_Swap(Welcome wel)
        {
            InitializeComponent();
            this._wel = wel;
        }

        private void Table_Swap_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            db.comboFill(cmb_current_tbl,"select * from table_status where status='"+"Processing"+"'","table_status","t_id","t_id");
            db.comboFill(cmb_emty_tbl, "select * from table_status where status='" + "Empty" + "' and section<>'-'", "table_status", "t_id", "t_id");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_current_tbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_current_tbl.Text = cmb_current_tbl.Text;
            
        }

        private void cmb_emty_tbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_empty_tbl.Text = cmb_emty_tbl.Text;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (cmb_current_tbl.Text != "")
            {
                DialogResult dlg = MessageBox.Show("Do you want to Swap Table Order?  \n From :  " + txt_current_tbl.Text + " To :  " + txt_empty_tbl.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {
                    flag = true;
                    float order_id = db.getDb_Value("select max(order_id) from table_order where t_id='" + txt_current_tbl.Text + "'");
                    db.update("update table_status set status='" + "Empty" + "' where t_id='" + txt_current_tbl.Text + "'");
                    db.update("update table_status set status='" + "Processing" + "' where t_id='" + txt_empty_tbl.Text + "'");
                    db.update("update table_order set t_id='" + txt_empty_tbl.Text + "' where order_id='" + order_id + "' and t_id='" + txt_current_tbl.Text + "'");
                    MessageBox.Show("Order Swapped Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _wel.welcome_pageload(flag,txt_empty_tbl.Text);
                    test_orderForm test = new test_orderForm();
                    test.Hide();

                }
            }
            else
                MessageBox.Show("No Table in Processing to Swap order!!!!!","confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Table_Swap_Load(sender, e);
           //((Welcome)this.Owner).welcome_pageload();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_current_tbl.Text = "";
            txt_empty_tbl.Text = "";

        }
        //start code 


        //end code
    }
}
