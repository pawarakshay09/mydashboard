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
    public partial class Issue_Kitchen : Form
    {
        Database db = new Database();
        string value_id = "0";
        bool flag = false;
        public Issue_Kitchen()
        {
            InitializeComponent();
        }

        private void Issue_Kitchen_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            db.formFix(this);
            db.comboFill(cmb_material_nm, "select distinct material_nm from material_nm", "material_nm", "material_nm", "material_nm");
            cmb_material_nm.Text = "--Select--";
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,issue_by as [Issue By],date as Date,remark as Remark from Issue_Kitchen ");
            dataGridView1.Columns[0].Visible = false;
            buttonUpdate.Enabled = false;
            btn_delete.Enabled = false;
            btnSave.Enabled = true;
            flag = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            if (db.confirm())
            {
                if (cmb_material_nm.Text == "")
                    errorProvider1.SetError(cmb_material_nm,"Select Material Name");
                else if(txt_qty.Text=="")
                    errorProvider1.SetError(txt_qty, "Enter Material Qty");
                else if (txt_issue_by.Text == "")
                    errorProvider1.SetError(txt_issue_by, "Enter Issued by name");
                else
                {
                    db.insert("insert into Issue_Kitchen values('" + cmb_material_nm.Text + "','" + txt_qty.Text + "','" + txt_issue_by.Text + "','" + date + "','" + txt_remark.Text + "')");
                    float old_qty = db.getDb_Value("select qty from tbl_stock where item_name='" + cmb_material_nm.Text + "'");
                    float new_qty = old_qty - float.Parse(txt_qty.Text);
                    db.update("update tbl_stock set qty='" + new_qty + "' where item_name='" + cmb_material_nm.Text + "'");
                    MessageBox.Show("Record save successfully!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,issue_by as [Issue By],date as Date,remark as Remark from Issue_Kitchen ");
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.ClearSelection();
                    txt_qty.Text = "";
                    txt_issue_by.Text = "";
                    txt_remark.Text = "";
                    float qty = db.getDb_Value("select qty from tbl_stock where item_name='" + cmb_material_nm.Text + "'");
                    txt_availbl_qty.Text = qty.ToString();
                }
            }
        }

        private void txtDisc_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
             if (cmb_material_nm.Text == "")
                    errorProvider1.SetError(cmb_material_nm,"Select Material Name");
                else if(txt_qty.Text=="")
                    errorProvider1.SetError(cmb_material_nm, "Enter Material Qty");
             else if (txt_issue_by.Text == "")
                 errorProvider1.SetError(cmb_material_nm, "Enter Issued by name");
             else
             {
                 string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                 value_id = db.get_DataGridValue(dataGridView1, "Issue_Kitchen", "id", 0);
                 if (MessageBox.Show("Do You Really Want to Update Record?", "Confirmation Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                 {
                     db.UpdateData("update Issue_Kitchen set material_nm='" + cmb_material_nm.Text + "',qty='" + txt_qty.Text + "',issue_by='" + txt_issue_by.Text + "',date='" + date + "',remark='" + txt_remark.Text + "' where id='" + value_id + "'");
                 }
                 dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,issue_by as [Issue By],date as Date,remark as Remark from Issue_Kitchen ");
                 dataGridView1.Columns[0].Visible = false;
                 dataGridView1.ClearSelection();
                 clear();
             }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
             string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            db.deleteGridRow(dataGridView1, "tbl_expenses", "id", 0);
         
            buttonUpdate.Enabled = false;
            dataGridView1.ClearSelection();
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,issue_by as [Issue By],date as Date,remark as Remark from Issue_Kitchen ");
            dataGridView1.Columns[0].Visible = false;
            clear();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            txt_qty.Text = "";
            txt_issue_by.Text = "";
            txt_remark.Text = "";
            cmb_material_nm.Text = "--Select--";
            buttonUpdate.Enabled = false;
            btn_delete.Enabled = false;
            btnSave.Enabled = true;
            txt_availbl_qty.Text = "";
            txt_unit.Text = "";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            value_id = db.get_DataGridValue(dataGridView1, "Issue_Kitchen", "id", 0);
            db.cnopen();
            SqlCommand cmd = new SqlCommand("select * from Issue_Kitchen where id='" + value_id + "'", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cmb_material_nm.Text = rd["material_nm"].ToString();
                txt_qty.Text = rd["qty"].ToString();
                txt_issue_by.Text = rd["issue_by"].ToString();
                //  dateTimePicker1.Text = rd["date"].ToString();
                txt_remark.Text = rd["remark"].ToString();
            }
            buttonUpdate.Enabled = true;
            btn_delete.Enabled = true;
            btnSave.Enabled = false;
            db.cnclose();
        }

       
        private void cmb_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                //float qty = db.getDb_Value("select qty from tbl_expenses where material_nm='" + cmb_material_nm.Text + "'");
                if (db.ChkDb_Value("select qty from tbl_stock where item_name='" + cmb_material_nm.Text + "'"))
                {
                    float qty = db.getDb_Value("select qty from tbl_stock where item_name='" + cmb_material_nm.Text + "'");
                    txt_availbl_qty.Text = qty.ToString();


                    string unit = db.getDbstatus_Value("select unit from tbl_stock where item_name='" + cmb_material_nm.Text + "'");
                    txt_unit.Text = unit;
                }
                else
                {

                  //  MessageBox.Show("Stock Not Available!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_availbl_qty.Text = "0";
                }
            }
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text!="")
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,issue_by as [Issue By],date as Date,remark as Remark from Issue_Kitchen where material_nm like '" + txtSearch.Text + "%" + "'");
            else
                dataGridView1.DataSource = db.Displaygrid("select id,material_nm as Name,qty as Qty,issue_by as [Issue By],date as Date,remark as Remark from Issue_Kitchen");

            dataGridView1.Columns[0].Visible = false;
        }

    }
}
