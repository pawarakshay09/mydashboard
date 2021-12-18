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
   
    public partial class DatabaseBackup : Form
    {
        Database db = new Database();
        DbBackup db1 = new DbBackup();
        DbBackup bc = new DbBackup();
        public DatabaseBackup()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_dbname.Text == "")
                errorProvider1.SetError(txt_dbname, "select database name");
            else
            {
                db.insert("delete from tbl_dbBackup");
                db.insert("insert into tbl_dbBackup values('" + txt_dbname.Text + "','" + txt_path.Text + "','" + txtdpath.Text + "')");
                MessageBox.Show("Database Path Saved successfully");
            }
        }

        private void btnEncript_Click(object sender, EventArgs e)
        {
            db1.EncryptFile(txt_path.Text, txtdpath.Text);
        }

        private void btnDecript_Click(object sender, EventArgs e)
        {
            db1.DecryptFile(txtdpath.Text, txt_path.Text);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatabaseBackup_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                String path = db.getDbstatus_Value("select top 1 path from tbl_dbBackup   order by 1 Asc");
                String databaseName = db.getDbstatus_Value("select top 1 databasename from tbl_dbBackup   order by 1 Asc");
                bc.DBBackup(databaseName, path);
                MessageBox.Show("Database Script Generate Successfully...! Path- D:\\DbScript\\");
            }
            catch
            {
                MessageBox.Show("Path Not Found -D:\\DbScript\\");
            }


        }

        private void txt_path_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
