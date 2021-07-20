using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAI_1_3_ADO.NET
{
    public partial class Form1 : Form
    {
        private string _sqlConnectionString =
            @"Data Source=DUNGNA_PC2021\SQLEXPRESS;Initial Catalog=CSharp3_Dungna29;User ID=dungna29;Password=123";

        private SqlConnection _con;
        private SqlCommand _cmd;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_KetNoiCSDL_Click(object sender, EventArgs e)
        {
            //1. Khởi tạo lớp kết nối vào CSDL thông qua đường dẫn
            _con = new SqlConnection(_sqlConnectionString);
            //2. Mở kết nối
            _con.Open();
            //3. Sau khi mở kết nối thì thực hiện 1 hành động nào đó
            //4. Thực thi 1 câu Query với SqlCommand
            _cmd = new SqlCommand("Select * from Accounts_ADO", _con);
            //var data = _cmd.ExecuteReader();//Thực thi câu truy vấn
            SqlDataAdapter data = new SqlDataAdapter(_cmd);
            DataTable table = new DataTable();
            data.Fill(table);//Đổ dữ liệu vào Table
            dataGridView1.DataSource = table;
            //5. Sau khi thực hiện xong hành động thì đóng kết nối
        }
    }
}
