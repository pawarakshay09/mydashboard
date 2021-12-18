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
    public partial class genralExp : Form
    {
        Database db = new Database();
        bool flag = false;
        string value_id;

        public genralExp()
        {
            InitializeComponent();
        }

        private void genralExp_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            db.comboFill(cmb_material_nm, "select * from tbl_expenses", "tbl_expenses", "material_nm", "material_nm");
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,amount as Amount,date as Date from tbl_expenses");
            dataGridView1.Columns[0].Visible = false;
            db.formFix(this);
            flag = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
                try
                {
                    errorProvider1.Clear();
                     if (cmb_material_nm.Text == "")
                    {
                       // MessageBox.Show("Please Enter Material Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        errorProvider1.SetError(cmb_material_nm, "Please Select Expenses Name");
                        lblError.Text = "Error - Please Select Expenses Name.";
                    }
                     else if (txtAmount.Text == "")
                     {
                         //MessageBox.Show("Please Enter Expenses Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         errorProvider1.SetError(txtAmount, "Please Enter Amount");
                         lblError.Text = "Error - Please Enter Amount.";
                     }
                     else
                     {
                         if (db.confirm())
                         {
                             string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                             db.insert("insert into tbl_expenses (material_nm,amount,date,remark ) values('" + cmb_material_nm.Text + "','" + txtAmount.Text + "','" + date + "','" + txtDisc.Text + "')");
                              MessageBox.Show("Record Save Successfully");
                             dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,amount as Amount,date as Date from tbl_expenses");
                             dataGridView1.Columns[0].Visible = false;
                             db.comboFill(cmb_material_nm, "select * from tbl_expenses", "tbl_expenses", "material_nm", "material_nm");
                             clear();

                         }
                     }

                }
                catch(Exception ex)
                {

                }
           
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (cmb_material_nm.Text == "")
                {
                    errorProvider1.SetError(cmb_material_nm, "Please Select Expenses Name");
                    lblError.Text = "Error - Please Select Expenses Name.";
                }
                else if (txtAmount.Text == "")
                {
                    //MessageBox.Show("Please Enter Expenses Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorProvider1.SetError(txtAmount, "Please Enter Amount");
                    lblError.Text = "Error - Please Enter Amount.";
                }
                else
                {
                    if (MessageBox.Show("Do You Really Want to Update Record?", "Confirmation Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        db.UpdateData("update tbl_expenses set material_nm='" + cmb_material_nm.Text + "',amount='" + txtAmount.Text + "',remark='" + txtDisc.Text + "' where id='" + value_id + "'");

                        dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses");
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.ClearSelection();
                        MessageBox.Show("Record Updated Successfully");
                    }
                    clear();


                }
            }
            catch (Exception ex)
            {

            }
        }

       

        private void btn_delete_Click(object sender, EventArgs e)
        {

            db.deleteGridRow(dataGridView1, "tbl_expenses", "id", 0);
            
            db.reset(this);
            buttonUpdate.Enabled = false;
            dataGridView1.ClearSelection();
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,amount as Amount,date as Date from tbl_expenses");
            clear();
           
        }

        void clear()
        {
            txtAmount.Text = "";
            txtDisc.Text = "";
            cmb_material_nm.Text = "";
            btn_delete.Enabled = false;
            buttonUpdate.Enabled = false;
            lblError.Text = "";
            btnSave.Enabled = true;
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

       

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            value_id = db.get_DataGridValue(dataGridView1, "tbl_expenses", "id", 0);
            db.cnopen();
            SqlCommand cmd = new SqlCommand("select * from tbl_expenses where id='" + value_id + "'", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cmb_material_nm.Text = rd["material_nm"].ToString();
                //txt_qty.Text = rd["qty"].ToString();
                txtAmount.Text = rd["amount"].ToString();
                //  dateTimePicker1.Text = rd["date"].ToString();
                txtDisc.Text = rd["remark"].ToString();
            }
            buttonUpdate.Enabled = true;
            btn_delete.Enabled = true;
            btnSave.Enabled = false;
            db.cnclose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,amount as Amount,date as Date from tbl_expenses where material_nm like '" + txtSearch.Text + "%'");
            else
                dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,amount as Amount,date as Date from tbl_expenses");

            dataGridView1.Columns[0].Visible = false;
        }

      
    }
}
