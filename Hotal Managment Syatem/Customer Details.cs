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
    public partial class Customer_Details : Form
    {
        Database db = new Database();
        public Customer_Details()
        {
            InitializeComponent();
        }

        private void Customer_Details_Load(object sender, EventArgs e)
        {
            db.comboFill(cmb_name, "select * from tbl_personalInfo", "tbl_personalInfo","name","name");
            dataGridView1.DataSource = db.Displaygrid("select name as Name,address as Adress,mobile as [Mobile No],dob as [DOB],age as Age,Gender AS Gender,occupation as Occupation,reason as Reason,address_proof as [Address Proof],proof_id as [Address Proof ID] from tbl_personalInfo");
        }

        private void cmb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select name as Name,address as Adress,mobile as [Mobile No],dob as [DOB],age as Age,Gender AS Gender,occupation as Occupation,reason as Reason,address_proof as [Address Proof],proof_id as [Address Proof ID] from tbl_personalInfo where name='"+cmb_name.Text+"'");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
            else
            {
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
        }

        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            string datefrm=dtp_from.Value.ToString("MM'-'dd'-'yyyy");
            string dateto=dtp_to.Value.ToString("MM'-'dd'-'yyyy");
            dataGridView1.DataSource = db.Displaygrid("select name as Name,address as Adress,mobile as [Mobile No],dob as [DOB],age as Age,Gender AS Gender,occupation as Occupation,reason as Reason,address_proof as [Address Proof],proof_id as [Address Proof ID] from tbl_personalInfo where name='" + cmb_name.Text + "' and date between '"+datefrm+"' and '"+dateto+"'");
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            string datefrm = dtp_from.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dtp_to.Value.ToString("MM'-'dd'-'yyyy");
            dataGridView1.DataSource = db.Displaygrid("select name as Name,address as Adress,mobile as [Mobile No],dob as [DOB],age as Age,Gender AS Gender,occupation as Occupation,reason as Reason,address_proof as [Address Proof],proof_id as [Address Proof ID] from tbl_personalInfo where name='" + cmb_name.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
