/*
 * Created by SharpDevelop.
 * User: abms2
 * Date: 07/10/2014
 * Time: 3:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Linq;
using System.Xml;


namespace Hotal_Managment_Syatem
{

    public partial class SMS : Form
    {
        //const string ACCESS_KEY="",MESSAGE="",TO_PHONE_NUMBER="",URL="";
        public SMS()
        {

            InitializeComponent();


        }
        void button1_Click(object sender, EventArgs e)
        {
            const string ACCESS_KEY = "419zwicbi3v3v3aee6w";
            const string MESSAGE = "Hello";
            const string TO_PHONE_NUMBER = "8600666155";//9421738200

            //   const string   URL = " http://map-alerts.smsalerts.biz//api/web2sms.php?id="+ACCESS_KEY+"&to="+TO_PHONE_NUMBER+"&content="+MESSAGE;
            send(ACCESS_KEY, TO_PHONE_NUMBER, MESSAGE);
        }
        void SMS_Load(object sender, EventArgs e)
        {


        }
        public void send(string ACCESS_KEY, string TO_PHONE_NUMBER, string MESSAGE)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://map-alerts.smsalerts.biz//api/web2sms.php?workingkey=" + ACCESS_KEY + "&to=" + TO_PHONE_NUMBER + "&sender=PHINIX&message=<MESSAGE>");
            //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://map-alerts.smsalerts.biz//api/web2sms.php?id="+ACCESS_KEY+"&to="+TO_PHONE_NUMBER+"&content="+MESSAGE+"");

            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            MessageBox.Show("Message Send successfully");
        }
    }


}
