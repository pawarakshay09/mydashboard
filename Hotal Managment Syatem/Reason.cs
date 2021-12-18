using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Hotal_Managment_Syatem
{
    public partial class Reason : Form
    {
        public string reason;
        string itNm;
        double qty;
        bool fg = false;
        Database db = new Database();
        public Reason()
        {
            InitializeComponent();
        }
        public Reason(string nm,double qt)
        {
            InitializeComponent();
            this.itNm = nm;
            this.qty = qt;
            label2.Visible = true;
            lblNm.Visible = true;
            lblqty.Visible = true;
            label3.Visible = true;
        }
        public Reason(string nm, double qt,bool flg)
        {
            InitializeComponent();
            this.itNm = nm;
            this.qty = qt;
            this.fg = flg;
            label2.Visible = true;
            lblNm.Visible = true;
            lblqty.Visible = true;
            label3.Visible = true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (txt_reason.Text != "")
            {
                bool flgdel = true;
                string OnlyName = txt_reason.Text;
                reason = RemoveSpecialCharacters(OnlyName);
                Welcome wc = new Welcome(reason, flgdel);
                this.Hide();
            }
        }


        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
        private void Reason_Load(object sender, EventArgs e)
        {
            lblNm.Text = itNm;
            lblqty.Text = qty.ToString();
            if (fg)            
                lblReduce.Visible=true;   
            else
                lblReduce.Visible = false;
        }

        private void buttonclose_Click(object sender, EventArgs e)
        {
            reason = "";
            this.Close();
        }
    }
}
