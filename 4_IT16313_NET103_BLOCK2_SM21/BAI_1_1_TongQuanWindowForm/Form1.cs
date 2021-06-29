using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAI_1_1_TongQuanWindowForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbl_Name.Text = "FPOLY .NET";
        }

        private void btn_ClickVaoDay_Click(object sender, EventArgs e)
        {
            lbl_Name.Text = "Nút đã được bấm nhé!";
        }

        private void btn_ClickVaoDay_MouseDown(object sender, MouseEventArgs e)
        {
            lbl_Click.Text = "Bạn đang bấm chuột xuống";
        }

        private void btn_ClickVaoDay_MouseUp(object sender, MouseEventArgs e)
        {
            lbl_Click.Text = "Bạn đã nhả chuột ra";
        }
    }
}
