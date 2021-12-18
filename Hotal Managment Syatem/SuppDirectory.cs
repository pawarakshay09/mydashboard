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
    public partial class SuppDirectory : Form
    {
        Database db = new Database();
        public SuppDirectory()
        {
            InitializeComponent();
        }

        private void SuppDirectory_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls ");
            gridCellformation();
        }
        public void gridCellformation()
        {
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 85;

        }

        private void rdb_name_CheckedChanged(object sender, EventArgs e)
        {
            txt_search.Text = "";
        }

        private void rdb_addr_CheckedChanged(object sender, EventArgs e)
        {
            txt_search.Text = "";
        }

        private void rdb_mob_CheckedChanged(object sender, EventArgs e)
        {
            txt_search.Text = "";
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            if (rdb_name.Checked)
            {
                dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls where sup_name like '" + txt_search.Text + "%'");
                gridCellformation();
            }
            else if (rdb_addr.Checked)
            {
                dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls where address like '" + txt_search.Text + "%" + "'");
                gridCellformation();
            }
            else
            {
                dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls where contact_1 like '" + txt_search.Text + "%" + "'");
                gridCellformation();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_search.Text = "";
            dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls");
            gridCellformation();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (rdb_name.Checked)
            {
                dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls where sup_name like '" + txt_search.Text + "%'");
                gridCellformation();
            }
            else if (rdb_addr.Checked)
            {
                dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls where address like '" + txt_search.Text + "%" + "'");
                gridCellformation();
            }
            else
            {
                dataGridView1.DataSource = db.Displaygrid("select sup_name as Name,address as Address,shop_name as [Shop Name],contact_1 as [Mobile No] from supplier_dtls where contact_1 like '" + txt_search.Text + "%" + "'");
                gridCellformation();
            }
        }
    }
}
