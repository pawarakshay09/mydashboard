using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace Hotal_Managment_Syatem
{
    public partial class SMSTemplet : Form
    {
        Boolean start = false;
        Database db = new Database();

        ArrayList termsList = new ArrayList();
        public SMSTemplet()
        {
            InitializeComponent();
        }
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private void SMSTemplet_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            db.comboFill(cmbSearch, "select * from Custmer", "Custmer", "name", "name");
            cmbSearch.Text = "";
            db.comboFill(cmbTemplate, "select name from SmsMaster", "SmsMaster", "name", "name");
            cmbTemplate.Text = "--Select--";
            dataGridView1.DataSource = db.Displaygrid("select name ,phone from Custmer where phone !='" + "" + "'");
            start = true;
            chek();
        }


        public void chek()
        {
            try
            {
                if (rdbAll.Checked)
                {
                    chkSelet.Checked = true;
                    panelAll.Enabled = true;
                    panelIndividual.Enabled = false;
                    termsList = null;
                    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        dataGridView1.Rows[i].Cells["Select"].Value = true;
                      //  var mobi = dataGridView1.Rows[i].Cells[2].Value.ToString();
                       
                    }
                }
                else if (rdbIndividual.Checked)
                {
                    chkSelet.Checked = false;
                    panelAll.Enabled = false;
                    panelIndividual.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void sensms()
        {
            
            string  mobi ="";
            string name ="";
            if (MessageBox.Show("Do You Want To Send SMS ?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                int count = 0;
                count = 0;
                if (rdbAll.Checked == true)
                {
                    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["Select"].Value) == true)
                        {
                            
                            mobi = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            if (mobi != "" && txtMessage.Text != "")
                            {
                                smssendingcode(mobi, name, txtMessage.Text);
                                count++;
                            }
                        }
                    }
                    MessageBox.Show(count + " Messages Sent Sucessfully");

                }
                else if (rdbIndividual.Checked)
                {
                    if (txtMobileNO.Text != "" && txtMessage.Text != "")
                    {
                        smssendingcode(txtMobileNO.Text, txtName.Text, txtMessage.Text);
                        MessageBox.Show("Messages Sent Sucessfully");
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Mobile No and Message");
                    }
                }
                txtName.Text = "";
                txtMobileNO.Text = "";
                txtMessage.Text = "";
            }
        }


        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
           
            chek();
           
        }

        private void rdbIndividual_CheckedChanged(object sender, EventArgs e)
        {

            chek(); 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start)
            {
                if (cmbSearch.Text != "")
                {
                    if (db.ChkDb_Value("select name ,phone from Custmer where name='" + cmbSearch.Text + "'"))
                    {
                        dataGridView1.DataSource = db.Displaygrid("select name ,phone from Custmer where name='" + cmbSearch.Text + "' and phone!='" + "" + "'");
                        chek();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sensms();
        }

        string mobileno = "";
        string msg = "";
        string name = "";
        public void smssendingcode(string mobileno,string name,string msg)
        {
            string urlName="", senderName="", smsType="", apiKey="";
            try
            {
               this.mobileno = mobileno;
               this.msg = msg;
                this.name=name;
                db.cnopen();
                SqlCommand cmd = new SqlCommand("select * from URLSetting",db.cn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    urlName = rd["url"].ToString();
                    senderName = rd["senderName"].ToString();
                    smsType = rd["smstype"].ToString();
                    apiKey = rd["apiKey"].ToString();
                }
                db.cnclose();
                //string url = "http://sms1.businesslead.co.in:8081/sendSMS?username=abmstechno&message=Dear " + name.Trim() + "," + msg.Trim() + "&sendername=ABMSTE&smstype=PROMO&numbers=" + mobileno + "&apikey=229abe7a-40d2-4ec1-9c75-7f483a027feb";
              
                string url = urlName + name.Trim() + "," + msg.Trim() + "&sendername=" + senderName + "&smstype=" + smsType + "&numbers=" + mobileno + "&apikey=" + apiKey + "";

                smsApiCall(url);

            }
            catch (Exception ex)
            {

            }
        }
        void smsApiCall(string getAPi)
        {

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(getAPi);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes("1");
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
            db.onlyNumber(e);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chekedchk();
        }


        public void chekedchk()
        {
            if (chkSelet.Checked)
            {
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].Cells["Select"].Value = true;
                }
            }
            if (chkSelet.Checked == false)
            {
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].Cells["Select"].Value = false;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.ShowDialog();
            chekedchk();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            dataGridView1.DataSource = null;
            string filePath = openFileDialog1.FileName;
            string extension = Path.GetExtension(filePath);
            //string header = rbHeaderYes.Checked ? "YES" : "NO";
            string header = "YES";
            string conStr, sheetName;

            conStr = string.Empty;
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    conStr = string.Format(Excel03ConString, filePath, header);
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(Excel07ConString, filePath, header);
                    break;
            }

            //Get the name of the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();

                        //Populate DataGridView.
                        dataGridView1.DataSource = dt;
                       
                    }
                }
        }




        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SMS_Template_Master master = new SMS_Template_Master();
            master.ShowDialog();
            db.comboFill(cmbTemplate, "select name from SmsMaster", "SmsMaster","name","name");
        }

        private void chkFrmTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFrmTemplate.Checked)
                panelTemplate.Visible = true;
            else
            {
                panelTemplate.Visible = false;
                txtMessage.Text = "";
            }
        }

        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTemplate.Text != "" && start)
            {
                txtMessage.Text = db.getDbstatus_Value("select message from SmsMaster where name='" + cmbTemplate.Text + "'");
                string todayDate = System.DateTime.Now.ToString("MM-dd-yyyy");
                string todayDay = todayDate.Substring(0, 6);
                
                string[] mailtypearray = new string[100];
                if (cmbTemplate.Text == "BIRTHDAY")
                    dataGridView1.DataSource = db.Displaygrid("select name as [Customer Name],phone as [Mobile No.]  from Custmer where MONTH(dateOfBirth) = MONTH('" + todayDate + "') and DAY(dateOfBirth) = DAY('" + todayDate + "') ");

                else if (cmbTemplate.Text == "ANNIVERSARY")
                    dataGridView1.DataSource = db.Displaygrid("select name as [Customer Name],phone as [Mobile No.] from Custmer where MONTH(aniversaryDate) = MONTH('" + todayDate + "') and DAY(aniversaryDate) = DAY('" + todayDate + "') and status='Married' ");
                else
                    dataGridView1.DataSource = db.Displaygrid("select name ,phone from Custmer where phone!=''");

                chekedchk();
            }
        }
    }
}
