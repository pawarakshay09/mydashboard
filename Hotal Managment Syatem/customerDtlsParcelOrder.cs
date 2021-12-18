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
    public partial class customerDtlsParcelOrder : Form
    {
        Database db = new Database();
        public customerDtlsParcelOrder()
        {
            InitializeComponent();
        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select Customer_id as [Customer ID],name as Name ,addr as Address,location as Location,phone as Phone,remark as Remark from Custmer");// where name like '" + txt_search.Text + "%'");

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[4].Width = 150;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }
    }
}
