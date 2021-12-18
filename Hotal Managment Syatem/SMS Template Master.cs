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
    public partial class SMS_Template_Master : Form
    {
        Database db = new Database();
        string id = "";
        public SMS_Template_Master()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtName.Text == "")
                errorProvider1.SetError(txtName, "Please Enter Name.");
            else if (txtMessage.Text == "")
                errorProvider1.SetError(txtMessage, "Please Enter Message.");
            else if (db.ChkDb_Value("select * from SmsMaster where name='" + txtName.Text + "'"))
                MessageBox.Show("This Kind of Message already Exist..","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else
            {
                db.insert("insert into SmsMaster values('" + txtName.Text + "','" + txtMessage.Text + "')");
                MessageBox.Show("Record Inserted Successfully..", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bindGrid();
                clear();
            }
        }
        void clear()
        {
            txtMessage.Text = "";
            txtName.Text = "";
            btn_update.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
        }
        void bindGrid()
        {
            dataGridView1.DataSource = db.Displaygrid("select id,name as Name,message as Message from SmsMaster");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Width = 220;

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int i=dataGridView1.SelectedCells[0].RowIndex;
             id = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtName.Text=dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtMessage.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            btn_update.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             id = db.get_DataGridValue(dataGridView1, "SmsMaster", "id", 0);
            db.deleteGridRow(dataGridView1, "SmsMaster", "id", 0);
            clear();
            bindGrid();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            id = db.get_DataGridValue(dataGridView1, "SmsMaster", "id", 0);
            if (MessageBox.Show("Do You Really Want to Update Record?", "Confirmation Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                db.update("update SmsMaster set name='" + txtName.Text + "', message='" + txtMessage.Text + "' where  id='" + id + "'");
                MessageBox.Show("Record Updated Sucessfully");
            }
           
            clear();
           bindGrid();

        }

        private void SMS_Template_Master_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnClose;
            bindGrid();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text!="")
               dataGridView1.DataSource = db.Displaygrid("select id,name as Name,message as Message from SmsMaster where name like '"+txtSearch.Text+"%'");
            else
                dataGridView1.DataSource = db.Displaygrid("select id,name as Name,message as Message from SmsMaster");

        }
    }
}
