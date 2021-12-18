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
    public partial class creatGroup : Form
    {
        Database db = new Database();
        string group_name = "";
        public creatGroup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtCategory.Text == "")
            {
                //MessageBox.Show("Please Enter Group Name");
                //txtCategory.Focus();
                errorProvider1.SetError(txtCategory, "Please Enter Group Name");
                lblError.Text = "Error - Please Enter Group Name";

            }
            else {

                db.insert("insert into createGrp(Grp_name,descp)values('" + txtCategory.Text + "','"+textBox1_Desacription.Text+"')");
                MessageBox.Show("Category saved Succesfully");
                resetFrom();
                bind_DGV();
            }
        }

        private void Category_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            bind_DGV();

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            resetFrom();
        }
        void resetFrom()
        {
            txtCategory.Text = "";
            textBox1_Desacription.Text = "";
            txt_search.Text = "";
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            btn_save.Enabled = true;
            errorProvider1.Clear();
        }
        void bind_DGV()
        {
            dataGridView1.DataSource = db.Displaygrid("select Grp_name as [Group Name],descp as Description FROM createGrp");
            dataGridView1.Columns[1].Width = 125;
            dataGridView1.Columns[0].Width = 130;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtCategory.Text == "")
            {
                errorProvider1.SetError(txtCategory, "Please Enter Group Name");
                lblError.Text = "Error - Please Enter Group Name";

            }


            else
            {
                DialogResult dlgresult = MessageBox.Show("Are you sure want to Update?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {
                    string group_id = db.getDb_Value("select id FROM createGrp where Grp_name='" + group_name + "'").ToString();

                    db.update("update createGrp set Grp_name='" + txtCategory.Text + "',descp='" + textBox1_Desacription.Text + "' where id='" + group_id + "'");
                    MessageBox.Show("Record Updated Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bind_DGV();

                    resetFrom();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
          
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            db.deleteGridRow(dataGridView1, "createGrp", "Grp_name", 0);
            resetFrom();
            bind_DGV();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            btn_update.Enabled = true;
            btn_delete.Enabled = true;
            btn_save.Enabled = false;

            int i = dataGridView1.SelectedCells[0].RowIndex;

            group_name = dataGridView1.Rows[i].Cells[0].Value.ToString();

            txtCategory.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox1_Desacription.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select Grp_name as [Group Name],descp as Description FROM createGrp where Grp_name like '" + txt_search.Text + "%'  ");
            dataGridView1.Columns[1].Width = 138;
            dataGridView1.Columns[0].Width = 130;
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            errorProvider1.Clear();
        }
    }
}
