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
    public partial class Tax_Deduction : Form
    {
        Database db = new Database();
        string tax_id = null;
        public Tax_Deduction()
        {
            InitializeComponent();
        }

        private void Tax_Deduction_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            Grid();
            load();
            txtTax_Type.Focus();
        }
        public void load()
        {
            if (db.ChkDb_Value("select * from tbl_tax") == true)
            {
                db.cnopen();
               // db.comboFill(cmbTax_Type, "select distinct tax_type from tbl_tax", "tbl_tax", "tax_type", "tax_type");
                db.cnclose();
               // cmbTax_Type.SelectedValue = "";
            }
        }

        public void Grid()
        {
            dataGridView1.DataSource = db.Displaygrid("select tax_id,tax_type as [Tax Type],tax_amt as [Tax Amount],disciption from tbl_tax");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.ClearSelection();
        }

        public void clear()
        {
            txt_Tax_amt.Text = "";
            txt_Desc.Text = ""; 
            txtTax_Type.Text = "";
            errorProvider1.Clear();
            enable();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clear();
            Grid();
            enable();
        }

        public void enable()
        {
            button_save.Enabled = true;
            btn_delete.Enabled = false;
            btn_Update.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            txtTax_Type.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_Tax_amt.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_Desc.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            tax_id = dataGridView1.Rows[i].Cells[0].Value.ToString();
            button_save.Enabled = false;
            btn_delete.Enabled = true;
            btn_Update.Enabled = true;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (txtTax_Type.Text == "")
                {
                    errorProvider1.SetError(txtTax_Type, "Please Enter OR Select Tax Type");
                    lblError.Text = "Error - Please Enter OR Select Tax Type";
                }
                else if (txt_Tax_amt.Text == "")
                {
                    errorProvider1.SetError(txt_Tax_amt, "Please Enter Amount");
                    lblError.Text = "Error - Please Enter Amount";
                }
                else if (db.ChkDb_Value("select * from tbl_tax where tax_type='" + txtTax_Type.Text + "'"))
                {
                    MessageBox.Show("Record Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    errorProvider1.Clear();
                    lblError.Text = "";
                    db.insert("insert into tbl_tax values('" + txtTax_Type.Text + "','" + txt_Tax_amt.Text + "','" + txt_Desc.Text + "','" + 0 + "')");

                    MessageBox.Show("Record Inserted Sucessully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    Grid();
                    enable();
                    load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (txtTax_Type.Text == "")
                {
                    errorProvider1.SetError(txtTax_Type, "Please Enter OR Select Tax Type");
                    lblError.Text = "Error - Please Enter OR Select Tax Type";
                }
                else if (txt_Tax_amt.Text == "")
                {
                    errorProvider1.SetError(txt_Tax_amt, "Please Enter Amount");
                    lblError.Text = "Error - Please Enter Amount";
                }
                else if (db.ChkDb_Value("select * from tbl_tax where tax_type='" + txtTax_Type.Text + "' and tax_id!='" + tax_id + "'"))
                {
                    MessageBox.Show("Record Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    if (MessageBox.Show("Do You Really Want to Update Record?", "Confirmation Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        db.update("update tbl_tax set tax_type='" + txtTax_Type.Text + "',tax_amt='" + txt_Tax_amt.Text + "',disciption='" + txt_Desc.Text + "' where tax_id='" + tax_id + "'");
                        MessageBox.Show("Record Updated Sucessfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    clear();
                    Grid();
                    enable();
                    load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                db.deleteGridRow(dataGridView1, "tbl_tax", "tax_id", 0);
               // MessageBox.Show("Record Deleted Sucessfully","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            clear();
            Grid();
            enable();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_Desc.Text = "";
            txt_Tax_amt.Text = "";
            txtTax_Type.Text = "";
            errorProvider1.Clear();
            enable();
        }

        private void txtTax_Type_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();
            lblError.Text = "";
        }

        private void txt_Tax_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();
            lblError.Text = "";
            db.onlyNumber(e);
        }
    }
}
