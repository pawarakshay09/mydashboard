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
    public partial class Expenses : Form
    {
        Database db = new Database();
        string value_id = "0";
        bool flag = false;
        public Expenses()
        {
            InitializeComponent();
        }

        private void Expenses_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            db.comboFill(cmb_material_nm, "select * from tbl_material_add", "tbl_material_add", "item_name", "item_name");
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses");
            dataGridView1.Columns[0].Visible = false;
           db.formFix(this);
           flag = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmb_material_nm.Text == "")
                {
                    MessageBox.Show("Please Enter Material Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txt_qty.Text == "")
                {
                    MessageBox.Show("Please Enter Qty", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtAmount.Text == "")
                {
                    MessageBox.Show("Please Enter Expenses Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (db.confirm())
                    {
                        string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                        if (db.ChkDb_Value("select material_nm,qty,amount from tbl_expenses") == true)
                        {
                            string unit = db.getDbstatus_Value("select unit from tbl_expenses where material_nm='" + cmb_material_nm.Text + "'");
                            txt_unit.Text = unit;
                            float old_qty = db.getDb_Value("select qty from tbl_expenses where material_nm='" + cmb_material_nm.Text + "'");
                            float new_qty = old_qty + float.Parse(txt_qty.Text);
                            float amount = db.getDb_Value("select amount from tbl_expenses where material_nm='" + cmb_material_nm.Text + "'");
                            float new_amt = amount + float.Parse(txtAmount.Text);
                            db.update("update tbl_expenses set qty='" + new_qty + "',amount='" + new_amt + "' where material_nm='" + cmb_material_nm.Text + "'");
                            MessageBox.Show("Record save successfully!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses ");
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.ClearSelection();
                        }
                        else
                        {

                            string insert = "insert into tbl_expenses values('" + cmb_material_nm.Text + "','" + txt_qty.Text + "','" + txt_unit.Text + "','" + txtAmount.Text + "','" + date + "','" + txtDisc.Text + "')";
                            db.cnopen();
                            SqlCommand cmd = new SqlCommand(insert, db.cn);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Record save successfully!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses ");
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.ClearSelection();
                            db.cnclose();
                        }

                        txt_qty.Text = "";
                        txtAmount.Text = "";
                        txtDisc.Text = "";
                        txt_unit.Text = "";
                        cmb_material_nm.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void bind_dg()
        {
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses where material_nm like '" + txt_search.Text + "%" + "'");
            dataGridView1.Columns[0].Visible = false;
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (cmb_material_nm.Text == "")
            {
                MessageBox.Show("Please Enter Material Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txt_qty.Text == "")
            {
                MessageBox.Show("Please Enter Qty", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtAmount.Text == "")
            {
                MessageBox.Show("Please Enter Expenses Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Do You Really Want to Update Record?", "Confirmation Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    db.UpdateData("update tbl_expenses set material_nm='" + cmb_material_nm.Text + "',qty='" + txt_qty.Text + "',amount='" + txtAmount.Text + "',remark='" + txtDisc.Text + "' where id='" + value_id + "'");
                }
                dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses");
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
                txt_qty.Text = "";
                txtAmount.Text = "";
                txtDisc.Text = "";
                cmb_material_nm.Text = "";

            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            db.deleteGridRow(dataGridView1, "tbl_expenses", "id", 0);
            bind_dg();
            db.reset(this);
            buttonUpdate.Enabled = false;
            dataGridView1.ClearSelection();
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses");

            txt_qty.Text = "";
            txtAmount.Text = "";
            txtDisc.Text = "";
            cmb_material_nm.Text = "";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
        //    db.reset(this);
        //    bind_dg();
        //    dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses");
        //    dataGridView1.Columns[0].Visible = false;
        //    buttonUpdate.Enabled = false;
        //    dataGridView1.ClearSelection();
            txt_qty.Text = "";
            txtAmount.Text = "";
            txtDisc.Text = "";
            cmb_material_nm.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

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
                txt_qty.Text = rd["qty"].ToString();
                txtAmount.Text = rd["amount"].ToString();
              //  dateTimePicker1.Text = rd["date"].ToString();
                txtDisc.Text = rd["remark"].ToString();
            }
            buttonUpdate.Enabled = true;
            db.cnclose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses where material_nm like '" + txtSearch.Text + "%" + "'");
               
            }
            else
                dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,amount as Amount,date as Date from tbl_expenses ");
            dataGridView1.Columns[0].Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Material add = new Add_Material();
            add.ShowDialog();
            db.comboFill(cmb_material_nm, "select * from tbl_material_add", "tbl_material_add", "item_name", "item_name");

        }

        private void cmb_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
              string unit=  db.getDbstatus_Value("select unit from tbl_material_add where item_name='"+cmb_material_nm.Text+"'");
              txt_unit.Text = unit;
               
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }
    }
}
