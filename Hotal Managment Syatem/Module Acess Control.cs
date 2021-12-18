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
    public partial class Module_Acess_Control : Form
    {
        Database db = new Database();
        public Module_Acess_Control()
        {
            InitializeComponent();
        }

        private void Module_Acess_Control_Load(object sender, EventArgs e)
        {
            bind_chkBoxList();
            dataGridViewAssignedContrls.DataSource = db.Displaygrid("select ControlUnder as [Group] from AssignModules");
            dataGridViewAssignedContrls.Columns[0].Width = 200;

        }
        void bind_chkBoxList()
        {
            checkedListAccessList.Items.Clear();

            SqlCommand cmd = new SqlCommand("select distinct ControlUnder from ModuleAcessCntrol ", db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 0;
            while (rd.Read())
            {
                checkedListAccessList.Items.Add(rd["ControlUnder"]);

                //if (rd["Modulestatus"].ToString() == "True")
                //    checkedListAccessList.SetItemChecked(i, true);

                i++;
            }


            db.cnclose();

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (db.confirm())
            {

                string f_name = "";

                // db.update("update tbl_accessControl set status='False' where ControlUnder='" + cmb_grp.Text + "'");
                db.DeleteData("delete from AssignModules ", "AssignModules");
                int i = 0;
                foreach (int indexChecked in checkedListAccessList.CheckedIndices)
                {
                    f_name = checkedListAccessList.Items[indexChecked].ToString();
                    //    db.update("update tbl_accessControl set status='True' where FormName='" + f_name + "'");
                    db.insert("insert into AssignModules values ('" + f_name + "','True')");

                }

                dataGridViewAssignedContrls.DataSource = db.Displaygrid("select ControlUnder as [Group] from AssignModules");
                dataGridViewAssignedContrls.Columns[0].Width = 200;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
