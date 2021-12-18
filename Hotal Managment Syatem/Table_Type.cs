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
    public partial class Table_Type : Form
    {
        Database db = new Database();
        public Table_Type()
        {
            InitializeComponent();
        }

        private void Table_Type_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button2;
            lblNm.Text = cmb_type.Text;

            db.comboFill(cmb_table_no,"select * from table_status","table_status","t_id","t_id");
            //cmb_table_no.Text = "Select";
            //cmb_type.Text = "Select";
            dataGridView1.DataSource = db.Displaygrid("select t_id as [Table No],table_type as [Table Type] from table_status where table_type='" + "A/C" + "'");
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 120;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (cmb_type.Text == "")
                {
                    errorProvider1.SetError(cmb_type, "Please Select Type.");
                    lblError.Text = "Error - Please Select Type.";
                }
                else if (cmb_table_no.Text == "")
                {
                    errorProvider1.SetError(cmb_table_no, "Please Select Table No.");
                    lblError.Text = "Error - Please Select Table No.";
                }
                else
                {
                    db.update("update table_status set table_type='" + cmb_type.Text + "' where t_id='" + cmb_table_no.Text + "'");
                    dataGridView1.DataSource = db.Displaygrid("select t_id as [Table No],table_type as [Table Type] from table_status where table_type='" + cmb_type.Text + "'");
                    lblNm.Text = cmb_type.Text;
                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[1].Width = 120;
                }
            }
            catch (Exception ex)
            { }
        }

        private void cmb_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select t_id as [Table No],table_type as [Table Type] from table_status where table_type='" + cmb_type.Text + "'");
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 120;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select t_id as [Table No],table_type as [Table Type] from table_status where table_type='" + "A/C " + "'");
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 120; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select t_id as [Table No],table_type as [Table Type] from table_status where table_type='" + cmb_type.Text + "'");

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 118;
            lblNm.Text = cmb_type.Text;

        }
    }
}
