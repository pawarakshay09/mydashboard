using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotal_Managment_Syatem
{
    public partial class CardPayment : Form
    {
        string billAmt="";
        public CardPayment()
        {
            InitializeComponent();
        }
        public CardPayment(string amt)
        {
            InitializeComponent();
            billAmt = amt;
        }

        private void CardPayment_Load(object sender, EventArgs e)
        {

        }
    }
}
