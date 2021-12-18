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
    public partial class Add_Material : Form
    {
        Database db = new Database();
        public Add_Material()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            db.InsertData("insert into tbl_material_add(item_name,unit) values('" + txt_name.Text + "','"+txt_unit.Text+"')", "tbl_material_add");
            MessageBox.Show("Record Inserted Successfully","Hotel",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Material_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
        }
    }
}
