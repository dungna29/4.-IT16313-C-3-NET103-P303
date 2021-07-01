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
            loadNamSinh();
            dgrid_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            loadTable();
           

        }

        void loadTable()
        {
            TheLoai tl = new TheLoai();
            //Cách 1:
            //dgrid_Data.DataSource = tl.GetListTheLoais();

            //Cách 2: Sử dụng DataTable
            DataTable table = new DataTable();
            //Tạo tên cột
            table.Columns.Add("Khóa chính", typeof(int));
            table.Columns.Add("Mã TL", typeof(string));
            table.Columns.Add("Tên TL", typeof(string));
            table.Columns.Add("Trạng Thái", typeof(string));
            table.Columns.Add("Status", typeof(string));
            //Fill data
            foreach (var x in tl.GetListTheLoais())
            {
                table.Rows.Add(x.Id, x.MaTheLoai, x.TenTheLoai, x.TrangThai, x.statuEdit);
            }
            //dgrid_Data.DataSource = table;

            //Cách 3: Làm việc trực tiếp với data grid view
            //Cấu hình cột
            dgrid_Data.ColumnCount = 4;//Set cột cho data grid view
            dgrid_Data.Columns[0].Name = "Mã TL";
            dgrid_Data.Columns[1].Name = "Tên TL";
            dgrid_Data.Columns[2].Name = "TT";
            dgrid_Data.Columns[3].Name = "Status";

            //Fill data
            foreach (var x in tl.GetListTheLoais())
            {
                dgrid_Data.Rows.Add(x.Id, x.MaTheLoai, x.TenTheLoai, x.TrangThai, x.statuEdit);
            }
         

        }

        void loadNamSinh()
        {
            int temp = 1600;
            for (int i = 0; i <= 2021-1600; i++)
            {
                cbo_NamSinh.Items.Add(temp);
                temp++;
            }

            cbo_NamSinh.SelectedIndex = 2021 - 1600;//Chọn giá trị ở vị trí hiển thị lên Combobox
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

        private void btn_XinChao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chào bạn " + txt_Name.Text, "Thông báo");
        }
    }
}
