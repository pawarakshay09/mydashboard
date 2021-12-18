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
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace Hotal_Managment_Syatem
{
    public partial class Login : Form
    {
        Database db = new Database();
        public int Login_Flag=0;
        string ConString = "";
        StreamReader reader;
        string conStr = "packet size=4096;integrated security=SSPI;" +
                  "data source=.\\sqlexpress;persist security info=False;" +
                  "initial catalog=master";
        public Login()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //   login();
        //}
        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    txt_UserName.Text = "";
        //    txt_Password.Text = "";
        //}

    void login()
    {
        if (txt_UserName.Text == "admin@2014" && txt_Password.Text == "admin@123456789")
         {

           Install();
           MessageBox.Show("Genral Setting Compeleted, Please Restart Application ");
           Application.Exit();
        }

        if (db.ChkDb_Value("select * from tbl_login where User_Name='" + txt_UserName.Text + "'and password='" + txt_Password.Text + "'"))
        {
          // string type = db.getDbstatus_Value("select type from tbl_login where User_Name='" + txt_UserName.Text + "'and password='" + txt_Password.Text + "'");
            //if (type == "Vat")
            //    {
                
            //        Login_Flag = 1;
            //       db.assignDB("RestrosoftDB_Vat");
            //        string user_nm = txt_UserName.Text;


            //        this.Hide();
            //        Welcome wel = new Welcome(this,user_nm);
            //        wel.ShowDialog();

                  
            //    }
            //    else
            //    {
                    db.assignDB("RestrosoftDB");
                    string user_nm = txt_UserName.Text;


                    this.Hide();

                    //Welcome wel = new Welcome(this,user_nm);
                    //wel.ShowDialog();
                   
               
         }
         else
         {
             MessageBox.Show("incorrect Login_ID and Password");
         }
       
     }

        private void button1_Click_1(object sender, EventArgs e)
        {
            login();
            
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public string GetScript(string name)
        {

            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                
                Stream str = asm.GetManifestResourceStream(name);
                reader = new StreamReader(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return reader.ReadToEnd();
        }
        private static string GetLogin(string databaseServer, string userName, string userPass, string database)
        {
            return "server=" + databaseServer + ";database=" + database + ";User ID=" + userName + ";Password=" + userPass;
        }
        public void ExecuteSql(SqlConnection sqlCon)
        {
            string[] SqlLine;
            Regex regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            string txtSQL = GetScript(Application.StartupPath + "\\install.txt");
            SqlLine = regex.Split(txtSQL);

            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.Connection = sqlCon;

            foreach (string line in SqlLine)
            {
                if (line.Length > 0)
                {
                    cmd.CommandText = line;
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        //rollback
                        ExecuteDrop(sqlCon);
                        break;
                    }
                }
            }
        }
        public void ExecuteDrop(SqlConnection sqlCon)
        {
            if (sqlCon.State != ConnectionState.Closed) sqlCon.Close();
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.Connection = sqlCon;
            cmd.CommandText = GetScript(Application.StartupPath + "\\uninstall.txt");
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public void Install()
        {

            SqlConnection sqlCon = new SqlConnection(conStr);

            sqlCon.Open();
            ExecuteSql(sqlCon);
            if (sqlCon.State != ConnectionState.Closed) sqlCon.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

    }
}
