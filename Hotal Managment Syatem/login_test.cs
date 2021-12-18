using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace Hotal_Managment_Syatem
{
    public partial class login_test : Form
    {
        Database db = new Database();
        string date;
        int flag = 0, NoofDays;
        string dayendDt;
        string cr_st = "", id, trialDate;
        public int Login_Flag = 0;
    string ConString = "";
        StreamReader reader;
        ServiceController myService;

        string conStr = "packet size=4096;integrated security=SSPI;" +
                  "data source=.\\sqlexpress;persist security info=False;" +
                  "initial catalog=master";

        
        public login_test()
        {
            sqlservicestart();
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


           // sqlservicestart();
            login();
           // checkServerStatus();
            //if (CheckForInternetConnection())
            //{
            //  //  MessageBox.Show("Internet is onnn");

            //    checkonline ck = new checkonline();


                
            // //= ck.getDbstatus_Value("select date from RestroOwnerMembership where RDetailsId='" + txtRD.Text + "' ");
            //    string RDID = db.getDbstatus_Value("Select RDetailsId from OwnerDetails");
            //    string days = ck.getDbstatus_Value("select date +'*'+ noofDays  from RestroOwnerMembership where RDetailsId='" + RDID + "'");


            //    date = days.Split('*')[0].ToString();
            //    days = days.Split('*')[1].ToString();



            //    DateTime trDt = new DateTime();
            //    /////
            //    date = date.Replace('/', '-');


            //    string dt = date.Split('-')[2] + '-' + date.Split('-')[0] + '-' + date.Split('-')[1];
            //    db.update("update TrialPeriodSet set NoOfDays='" + days + "' ,TrialDate='" + dt + "'");

              

            //}
           
           
          


           
        
        }

        public static bool CheckForInternetConnection()
        {

            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))// here check PC is online or not
                {

                    IPHostEntry host;
                    string localIP = "?";
                    host = Dns.GetHostEntry(Dns.GetHostName());

                    return true;
                }
            //try
            //{
            //    using (var client = new WebClient())
            //    using (client.OpenRead("http://www.abmstechnologies.com"))
            //    {
            //        return true;

            //       // MessageBox.Show("Internet is onnn");
            //    }
            }
            catch
            {
                return false;
            }
        }
        bool exeStatus;
        string syncStatus, syncExepath;
        private bool ProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();
            bool isRunning = false;

            Process[] pList = Process.GetProcessesByName(FileName);
            foreach (Process p in pList)
                if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    isRunning = true;
                    break;
                }

            return isRunning;
        }
        private void Open()
        {

            if (exeStatus == false)
            {
                try
                {      System.Diagnostics.Process.Start(@"C:\\Program Files\\Default Company Name\\SynchData\\DataSynchronisation.exe"); //C:\\Program Files\\Default Company Name\\SynchData\\DataSynchronisation.exe
                }
                catch (Exception ex)
                {
                    MessageBox.Show("online sync code not working");
                }
            }
        }
        private void popup()
        {

            Thread th = new Thread(() =>
            {
                try
                {
                    Open();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex);
                }
            });
            th.Start();
            Thread.Sleep(1000);   //you can update this time as your requirement
            th.Abort();
        }

        void login()
        {
            if (txt_name.Text == "admin@2014" && txt_password.Text == "admin@123456789")
            {
                Install();
                MessageBox.Show("Genral Setting Compeleted, Please Restart Application "); 
                Application.Exit();
            }
            if (txt_name.Text == "abmstech" && txt_password.Text == "abms")
            {
                Configuation_Panel panel = new Configuation_Panel();
                panel.ShowDialog();
                this.Hide();
                Application.Exit();
            }

            if (txt_name.Text == "abmstech" && txt_password.Text == "abms123")
            {
                Module_Acess_Control acess = new Module_Acess_Control();
                acess.ShowDialog();
                this.Hide();
                Application.Exit();
            }
         if (db.ChkDb_Value("select * from tbl_login where User_Name='" + txt_name.Text + "'and password='" + txt_password.Text + "'"))
            {


                dayendDt = db.getDbstatus_Value("Select ddate from tbl_dayend_status");
                string toDayDt = System.DateTime.Now.ToString("MM/dd/yyyy");
                double dateDiff, count;
                NoofDays = int.Parse(db.getDb_Value("select NoOfDays from TrialPeriodSet").ToString());

                trialDate = db.getDbstatus_Value("select TrialDate from TrialPeriodSet");

                DateTime trDt1 = Convert.ToDateTime(trialDate);

                DateTime dtmain = System.DateTime.Now;

                date = Convert.ToDateTime(dtmain).ToString("MM-dd-yyyy");


                dateDiff = (dtmain - trDt1).TotalDays;
                count = (NoofDays - dateDiff);

                if (count <= 0)
                {
                    string mob = db.getDbstatus_Value("Select mob from Admin_Contacts");
                    MessageBox.Show(" Please Contact System Administrator = '" + mob + "' ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    
                    DialogResult dlg = MessageBox.Show("Please Contact System Administrator = '" + mob + "' ","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                       if (dlg == DialogResult.Yes)
                       {
                           checkonline ck = new checkonline();

                           string RID = db.getDbstatus_Value("select  RDetailsId from OwnerDetails");
                           date = ck.getDbstatus_Value("select date from RestroOwnerMembership where RDetailsId='" + RID + "' ");
                           string days = ck.getDbstatus_Value("select noofDays  from RestroOwnerMembership where RDetailsId='" + RID + "'");

                           DateTime trDt = new DateTime();

                           date = date.Replace('/', '-');


                           string dt = date.Split('-')[2] + '-' + date.Split('-')[0] + '-' + date.Split('-')[1];


                           db.update("update TrialPeriodSet set NoOfDays='" + days + "' ,TrialDate='" + dt + "'");

                       }
                }
                else
                {






                    string user_nm = txt_name.Text;
                    this.Hide();
              

                    if (exeStatus == false)
                    {
                        try
                        {
                                  string syncValue = db.getDbstatus_Value("select  status+'-'+value from tbl_option where grp='onlineSync'");
                            syncStatus = syncValue.Split('-')[0];
                            syncExepath = syncValue.Split('-')[1];
                                if (syncStatus == "Yes")
                                System.Diagnostics.Process.Start(syncExepath); 

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("online sync code not working");
                        }
                    }

                    Welcome wel = new Welcome(this, user_nm);
                    wel.ShowDialog();

                    
                }
            }
            else
            {
                MessageBox.Show("Incorrect User Name Or Password");
            }
            

        }

        void sqlservicestart()
        {
            string getServiceName = string.Empty;
            string status;
            ServiceController mySC = new ServiceController();

            try
            {
                //DataTable dt = new DataTable();
                //dt = db.Displaygrid("select  value from tbl_option where grp='ServiceName'");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                var title = ConfigurationManager.AppSettings["sqlservicestart"];


                getServiceName = title;

                  
                    mySC = new ServiceController(getServiceName);

                    status = mySC.Status.ToString();
               // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                Console.ReadLine();

                return;

            }

            if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
            {
                try
                {
                    mySC.Start();
                    mySC.WaitForStatus(ServiceControllerStatus.Running);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in starting the service: " + ex.Message);

                }

            }

            Console.ReadLine();


            return;

        }




        void checkServerStatus()
        {
            string getServiceName = string.Empty;
            string status;
            ServiceController mySC=new ServiceController();

            try
            {
                DataTable dt = new DataTable();
                dt = db.Displaygrid("select  value from tbl_option where grp='ServiceName'");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                  //  getServiceName =  db.getDbstatus_Value("select  value from tbl_option where grp='ServiceName'").ToUpper();
          //  string myServiceName = "MSSQL$SQLEXPRESS";

             //service status (For example, Running or Stopped)

                    getServiceName = dt.Rows[i]["value"].ToString().ToUpper();

            //display service status: For example, Running, Stopped, or Paused
              mySC = new ServiceController(getServiceName);
          
                status = mySC.Status.ToString();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                Console.ReadLine();

                return;

            }

            //display service status: For example, Running, Stopped, or Paused
            //if service is Stopped or StopPending, you can run it with the following code.
            if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
            {
                try
                {
                    mySC.Start();
                    mySC.WaitForStatus(ServiceControllerStatus.Running);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in starting the service: " + ex.Message);
                    
                }

            }

            Console.ReadLine();


            return;

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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_test_Load(object sender, EventArgs e)
        {

        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
