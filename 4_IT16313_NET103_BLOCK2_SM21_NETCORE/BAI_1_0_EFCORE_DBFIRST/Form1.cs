using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAI_1_0_EFCORE_DBFIRST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AccountService accountService = new AccountService();
        }

    }
}
