using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotal_Managment_Syatem
{
    public partial class waiterassign : Form
    {
        Database db = new Database();
        public waiterassign()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (!db.ChkDb_Value(" select * from tbl_waiterAssignSection where wName= '" + cbwname.Text + "' and section='" + cbsec.Text + "' "))
            {
                db.insert(" insert into tbl_waiterAssignSection (wName,section) values('" + cbwname.Text + "' ,'" + cbsec.Text + "' ) ");
                MessageBox.Show("Updated Sucessfully......!");
            }
            else
            {
                MessageBox.Show("Already Added......!");
            }

            bind();
        }

        private void waiterassign_Load(object sender, EventArgs e)
        {
            db.comboFill(cbwname, "select distinct wName from tbl_waiterAssignSection", "tbl_waiterAssignSection", "wName", "wName");
            db.comboFill(cbsec, "select distinct section from table_status", "table_status", "section", "section");
            bind();
          
        }
        void bind()
        {
            dataGridView1.DataSource = db.Displaygrid(" select w_id,wName, section from tbl_waiterAssignSection ");
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[3].Width = 180;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns[0].Index)
            {
                int i = dataGridView1.SelectedCells[0].RowIndex;
                string w_idNo = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string wname = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string section = dataGridView1.Rows[i].Cells[3].Value.ToString();

                db.delete("delete from tbl_waiterAssignSection where wName='" + wname + "' and section='" + section + "' ");

                dataGridView1.ClearSelection();
                dataGridView1.Columns[1].Visible = false;
                bind();
               // dataGridView1.Columns[0].Width = 180;
           }

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            string value_id = db.get_DataGridValue(dataGridView1, "tbl_waiterAssignSection ", "w_id ", 1);
                  db.cnopen();
                  SqlCommand cmd = new SqlCommand("select * from tbl_waiterAssignSection where w_id='" + value_id + "'", db.cn);
                  SqlDataReader rd = cmd.ExecuteReader();
                  while (rd.Read())
                  {
                      cbwname.Text = rd["wName"].ToString();
                      cbsec.Text = rd["section"].ToString();
                     
                  }
                  db.cnclose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value_id = db.get_DataGridValue(dataGridView1, "tbl_waiterAssignSection ", "w_id ", 1);

            db.update(" update tbl_waiterAssignSection set wName='" + cbwname.Text + "', section='" + cbsec.Text + "' where  w_id='" + value_id + "' ");
            MessageBox.Show("Updated Sucessfully......!");
            bind();
         
        }
    }
}
