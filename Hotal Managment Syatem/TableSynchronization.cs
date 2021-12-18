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
    public partial class TableSynchronization : Form
    {
        Database db = new Database();
        public TableSynchronization()
        {
            InitializeComponent();
        }

        private void TableSynchronization_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            dgv.DataSource = db.Displaygrid("select table_name from ws_table_list");
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            for(int i=0;i<=dgv.RowCount - 1; i++)
            {
                if (db.ChkDb_Value("SELECT *  FROM INFORMATION_SCHEMA.TABLES   WHERE  TABLE_NAME = '" + dgv.Rows[i].Cells[0].Value.ToString() + "'"))
                {

                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
