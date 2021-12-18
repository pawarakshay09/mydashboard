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
    public partial class Table_status : Form
    {
        Database db = new Database();
        public Table_status()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Table_status_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            dataGridView1.DataSource = db.Displaygrid("select t_id as [Table No.],status as Status from table_status where status!='Empty'");
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 80;
        }
    }
}
