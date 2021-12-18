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
    public partial class Add_Designation : Form
    {
        Database db = new Database();
        public Add_Designation()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_designation.Text == "")
                errorProvider1.SetError(txt_designation, "Enter Designation");
            else if(db.ChkDb_Value("select * from tbl_designation where design_name='"+txt_designation.Text+"'"))
                MessageBox.Show("Record Already Exist!!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                db.insert("insert into tbl_designation values('" + txt_designation.Text + "')");
                txt_designation.Text = "";
                dataGridView1.DataSource = db.Displaygrid("select design_name as [Designation],id from tbl_designation");
                dataGridView1.Columns[0].Width = 90;
                dataGridView1.Columns[1].Visible = false;
                MessageBox.Show("Record Inserted Succesfully!!!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Designation_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            dataGridView1.DataSource = db.Displaygrid("select design_name as [Designation],id from tbl_designation");
            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            db.deleteGridRow(dataGridView1, "tbl_designation", "id", 1);
            dataGridView1.DataSource = db.Displaygrid("select design_name as [Designation],id from tbl_designation");
            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Visible = false;
            btnDelete.Enabled = false;
            txt_designation.Text = "";
            btn_save.Enabled = true;

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            txt_designation.Text=dataGridView1.Rows[i].Cells[0].Value.ToString();
            btnDelete.Enabled = true;
            btn_save.Enabled = false;
        }
    }
}
