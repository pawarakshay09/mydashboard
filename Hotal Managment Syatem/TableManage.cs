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
    public partial class TableManage : Form
    {
        Database db = new Database();
        public TableManage()
        {
            InitializeComponent();
        }

        private void TableManage_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            bind();
        }

        void bind()
        {

            db.cnopen();
            int i = 0;
            SqlCommand cmd = new SqlCommand("select t_id,status,table_type,lblName,tblStatus from table_status ", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                dgv.Rows.Insert(i, rd[0].ToString(), rd[1].ToString(), rd[2].ToString(), rd[3].ToString(), rd[4].ToString());
                i++;
            }
            db.cnclose();

        }
        void clear()
        {
            txtTblNm.Text = "";
            txtSection.Text = "";
            cmbType.Text = "";
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txtTblNm.Text == "" || txtTblNm.Text == " ")
            {
                lblError.Text = "Error - Please Enter Table Name.";
            }
            else if (cmbType.Text == "" || cmbType.Text == " ")
            {
                lblError.Text = "Error - Please Select Table Type.";
            }
            else if (txtSection.Text == "" || txtSection.Text == " ")
            {
                lblError.Text = "Error - Please Enter Section Type.";
            }
            else if (db.ChkDb_Value("select * from table_status where t_id='" + txtTblNm.Text + "'"))
                MessageBox.Show("Record Already Exists!!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                dgv.Rows.Clear();
                db.insert("insert into table_status (t_id,status,table_type,lblName,tblStatus) values ('" + txtTblNm.Text + "','Empty','" + cmbType.Text + "','"+txtSection.Text+"','Active')");
                bind();
                clear();
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns[5].Index)
            {
                int i = dgv.SelectedCells[0].RowIndex;
                string payMode = dgv.Rows[i].Cells[0].Value.ToString();
                string status = dgv.Rows[i].Cells[4].Value.ToString();
                //db.DeleteData("delete from tbl_printerMaster where printerName='" + prtName + "' and type='" + typ + "'", "tbl_printerMaster");
                if (status == "Active")
                    db.update("update table_status set tblStatus='Deactive' where t_id='" + payMode + "'");
                else
                    db.update("update table_status set tblStatus='Active' where t_id='" + payMode + "'");

            }
            dgv.Rows.Clear();
            bind();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
