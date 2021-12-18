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
    public partial class AccessControl : Form
    {
        Database db = new Database();
        List<string> ChkedRow = new List<string>();
        bool pageload = false;
        public AccessControl()
        {
            InitializeComponent();
        }

        private void AccessControl_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnClose;
            db.comboFill(comboBoxUsersList, "select * from tbl_login", "tbl_login", "User_Name", "User_Name");

            db.comboFill(cmb_grp, "select distinct (ControlUnder) from tbl_accessControl", "tbl_accessControl", "ControlUnder", "ControlUnder");
           // bind_chkBoxList();
            pageload = true;
                dataGridViewAssignedContrls.DataSource = db.Displaygrid("select FormName as Access,ControlUnder as [Group] from tbl_user_access_control where user_name='" + comboBoxUsersList.Text + "'");
                dataGridViewAssignedContrls.Columns[0].Width = 200;

        }
        void bind_chkBoxList()
        {
            checkedListAccessList.Items.Clear();

            SqlCommand cmd = new SqlCommand("select * from tbl_accessControl where ControlUnder='" + cmb_grp.Text + "' ", db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 0;
            while (rd.Read())
            {
                checkedListAccessList.Items.Add(rd["FormName"]);

                if (rd["status"].ToString() == "True")
                    checkedListAccessList.SetItemChecked(i, true);

                i++;
            }


            db.cnclose();

        }

        private void cmb_grp_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_chkBoxList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (db.confirm())
            {
                
                string f_name = "";
               
               // db.update("update tbl_accessControl set status='False' where ControlUnder='" + cmb_grp.Text + "'");
                db.DeleteData("delete from tbl_user_access_control  where ControlUnder='" + cmb_grp.Text + "' and user_name='"+comboBoxUsersList.Text+"'","tbl_user_access_control");
                int i = 0;
                foreach (int indexChecked in checkedListAccessList.CheckedIndices)
                {
                    f_name = checkedListAccessList.Items[indexChecked].ToString();
                //    db.update("update tbl_accessControl set status='True' where FormName='" + f_name + "'");
                    db.insert("insert into tbl_user_access_control values ('" + comboBoxUsersList.Text + "','" + f_name + "','" + cmb_grp.Text + "','True')");

                }

                dataGridViewAssignedContrls.DataSource = db.Displaygrid("select FormName as Access,ControlUnder as [Group] from tbl_user_access_control where user_name='" + comboBoxUsersList.Text + "'");
                dataGridViewAssignedContrls.Columns[0].Width = 200;
            }
        }

        private void checkedListAccessList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListAccessList_Leave(object sender, EventArgs e)
        {
            
        }

        //private void comboBoxUsersList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (pageload)
        //    {
        //        // show th prticuler user all access cobntrols
        //        dataGridViewAssignedContrls.DataSource = db.Displaygrid("select FormName as Access,ControlUnder as [Group] from tbl_user_access_control where user_name='" + comboBoxUsersList.Text + "'");
        //        dataGridViewAssignedContrls.Columns[0].Width = 200;
        //    }
        //}

        private void comboBoxUsersList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (pageload)
            {
                // show th prticuler user all access cobntrols
                dataGridViewAssignedContrls.DataSource = db.Displaygrid("select FormName as Access,ControlUnder as [Group] from tbl_user_access_control where user_name='" + comboBoxUsersList.Text + "'");
                dataGridViewAssignedContrls.Columns[0].Width = 200;
            }
        }

        private void cmb_grp_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            bind_chkBoxList();
        }
   }
}
    
