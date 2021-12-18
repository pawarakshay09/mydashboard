using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    public partial class Update_Menu : Form
    {
        Database db = new Database();
        bool cmbFlag = false;
        public Update_Menu()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();
        }
        void bind()
        {
            if (cmbFlag)
            {

                try
                {

                    dataGridView1.DataSource = db.Displaygrid("SELECT item_code as [Menu Code],m_name as [Menu Name],category as Category,rate as [AC Rate],non_ACrate as [Non AC Rate],insentive_rate as [Insentive Rate],menu_id,driverRate as[Driver Rate] from menu where category='" + cmbCategory.Text + "'");
                    griFormation();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    db.cnclose();
                }
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            float ACRate, nonACRate,driverRate,itemCode,insentiveRate;
            string id;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                itemCode = float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                insentiveRate = float.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                ACRate = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                nonACRate = float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                driverRate = float.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                id = (dataGridView1.Rows[i].Cells[7].Value.ToString());

                db.update("update menu set rate='" + ACRate + "',non_ACrate='" + nonACRate + "',driverRate='" + driverRate + "',item_code='" + itemCode + "',insentive_rate='"+insentiveRate+"' where menu_id=" + id + "");

            }
            MessageBox.Show("Menu Rates Updated Successfully!!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            bind();
        }

        private void Update_Menu_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            dataGridView1.DataSource = db.Displaygrid("SELECT item_code as [Menu Code],m_name as [Menu Name],category as Category,rate as [AC Rate],non_ACrate as [Non AC Rate],insentive_rate as [Insentive Rate],menu_id,driverRate as[Driver Rate] from menu");

            griFormation();


            db.comboFill(cmbCategory, "SELECT cat_name FROM category order by cat_name asc", "category", "cat_name", "cat_name");
            cmbCategory.Text = "--Select--";
            cmbFlag = true;
          //  bind();
        }

        void griFormation()
        {
            dataGridView1.Columns[1].Width = 70;
            //dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].Width = 160;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].Width = 100;
            //dataGridView1.Columns[4].
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 90;
            dataGridView1.Columns[8].Width = 90;
            dataGridView1.Columns[7].Visible = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                db.deleteGridRow(dataGridView1, "menu", "menu_id", 7);
                bind();
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "Menu Export.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName, "Menu Export"); // Here dataGridview1 is your grid view name 

                    MessageBox.Show("File Sucessfully Exported");
                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbCategory.Text = "--Select--";
            dataGridView1.DataSource = db.Displaygrid("SELECT item_code as [Menu Code],m_name as [Menu Name],category as Category,rate as [AC Rate],non_ACrate as [Non AC Rate],insentive_rate as [Insentive Rate],menu_id,driverRate as[Driver Rate] from menu");

            griFormation();

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT item_code as [Menu Code],m_name as [Menu Name],category as Category,rate as [AC Rate],non_ACrate as [Non AC Rate],insentive_rate as [Insentive Rate],menu_id,driverRate as[Driver Rate] from menu where m_name like '" + txt_search.Text + "%'");
            griFormation();
        }

        


        
    }
}
