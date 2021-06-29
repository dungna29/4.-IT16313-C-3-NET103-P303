using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BAI_1_0_TaoWindowFormBangCode
{
    class Form1:Form
    {
        private Label lblName;
        private Button btnClick;

        public Form1()
        {
            //Phần 1: Câu hình form
            this.Text = "Csharp 3";
            this.Size = new Size(900, 600);

            //Phần 2: Thêm Control vào form
            lblName = new Label();
            lblName.Text = "Bắt đầu vào môn C#3";
            lblName.Location = new Point(100, 100);
            this.Controls.Add(lblName);

            //Phần 3: Thêm button vào form
            btnClick = new Button();
            btnClick.Text = "Click vào đây";
            btnClick.Location = new Point(200, 200);
            btnClick.Width = 300;
            this.Controls.Add(btnClick);
        }

        public static void Main(string[] argv)
        {
            Application.Run(new Form1());
        }
    }
}
