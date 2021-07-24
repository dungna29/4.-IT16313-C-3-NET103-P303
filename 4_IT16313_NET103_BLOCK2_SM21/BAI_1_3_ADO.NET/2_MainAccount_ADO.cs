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
    public partial class _2_MainAccount_ADO : Form
    {
        private string _sqlConnectionString =
            @"Data Source=DUNGNA_PC2021\SQLEXPRESS;Initial Catalog=CSharp3_Dungna29;User ID=dungna29;Password=123";
        private SqlConnection _con;
        private SqlCommand _cmd;
        private ServiceAccount _serviceAccount;
        private List<Account> _lstAccounts;
        private Account _account;

        public _2_MainAccount_ADO()
        {
            InitializeComponent();
            _serviceAccount = new ServiceAccount(_sqlConnectionString);
            _lstAccounts = new List<Account>();
            Cach1LoadData();
            Cach2LoadData();
            Cach4LoadData();
        }
        void Cach1LoadData()
        {
            _lstAccounts = new List<Account>();//Làm sạch List mỗi lần chạy vào phương thức này
            _lstAccounts = _serviceAccount.GetLstAccountsDB();//Đổ dữ liệu từ bên Service về List tại Main
            //Đếm số lượng thuộc tính có trong 1 đối tượng
            Type type = typeof(Account);
            int soLuongThuocTinh = type.GetProperties().Length + 1;//Độ dài trong mảng chính là số lượng thuộc tính của đối tượng
            dgrid_Account.ColumnCount = soLuongThuocTinh;//Khởi tạo số lượng cột trong Grid
            dgrid_Account.Columns[0].Name = "Id";
            dgrid_Account.Columns[1].Name = "Tài khoản";
            dgrid_Account.Columns[2].Name = "Mật khẩu";
            dgrid_Account.Columns[3].Name = "Giới tính";
            dgrid_Account.Columns[4].Name = "Tuổi";
            dgrid_Account.Columns[5].Name = "Năm sinh";
            dgrid_Account.Columns[6].Name = "Trạng thái";

            dgrid_Account.Rows.Clear();//Xóa dữ liệu trên Grid View
            //Đổ dữ liệu vào Grid
            foreach (var x in _lstAccounts)
            {
                dgrid_Account.Rows.Add(x.Id, x.Acc, x.Pass, x.Sex == 1 ? "Nam" : "Nữ", DateTime.Now.Year - x.YearofBirth, x.YearofBirth, x.Status ? "Hoạt động" : "Không hoạt đông");
            }
        }

        void Cach2LoadData()
        {
            _con = new SqlConnection(_sqlConnectionString);
            _con.Open();
            _cmd = new SqlCommand("Select * from Accounts_ADO", _con);//Nếu muốn thay đổi tên cột hãy làm việc này từ câu truy vấn dùng theo tên cột và AS. SELECT ID AS 'MÃ'......
           
            //Sử dụng SqlDataAdapter để hứng dữ liệu từ DB
            SqlDataAdapter dataAdapter = new SqlDataAdapter(_cmd);

            //DataTable là con của Dataset và nó tương ứng 1 bảng trong CSDL
            //DataSet là 1 tập hợp các bảng trong CSDL

            DataTable table = new DataTable();
            dataAdapter.Fill(table);//Đổ dữ liệu vào DataTable
            dgrid_Account2.DataSource = table;
            //Với cách này không nên can thiệp vào trong cột của bảng
            _con.Close();
        }
        void Cach3LoadData()
        {
            _con = new SqlConnection(_sqlConnectionString);
            _con.Open();
            //Sử dụng SqlDataAdapter ko cần dùng sql conmmand
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Accounts_ADO", _con);

            //DataTable là con của Dataset và nó tương ứng 1 bảng trong CSDL
            //DataSet là 1 tập hợp các bảng trong CSDL

            DataTable table = new DataTable();
            dataAdapter.Fill(table);//Đổ dữ liệu vào DataTable
            dgrid_Account2.DataSource = table;
            //Với cách này không nên can thiệp vào trong cột của bảng
            _con.Close();
        }
        void Cach4LoadData()
        {
            _con = new SqlConnection(_sqlConnectionString);
            _con.Open();
            //Sử dụng SqlDataAdapter ko cần dùng sql conmmand
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Accounts_ADO", _con);

            //DataTable là con của Dataset và nó tương ứng 1 bảng trong CSDL
            //DataSet là 1 tập hợp các bảng trong CSDL

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);//Đổ dữ liệu vào Dataset
            dgrid_Account3.DataSource = dataSet.Tables[0];
            //Với cách này không nên can thiệp vào trong cột của bảng
            _con.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            _con = new SqlConnection(_sqlConnectionString);
            _con.Open();
            //Thực thi câu truy vấn thêm vào cơ sở dữ liệu
            string query = @"INSERT INTO Accounts_ADO(Acc,Pass,Sex,YearofBirth,Status) VALUES(@Acc,@Pass,@Sex,@YearofBirth,@Status)";
            _cmd = _serviceAccount.GetSqlCommand(query, _con);
            _cmd.CommandText = query;
            //_cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());//Tự động tạo ra mã Guild tự động
            _cmd.Parameters.AddWithValue("@Acc",txtAcc.Text);
            _cmd.Parameters.AddWithValue("@Pass", txtPass.Text);
            _cmd.Parameters.AddWithValue("@Sex",rbtnNam.Checked?1:0);
            _cmd.Parameters.AddWithValue("@YearofBirth", cmbNamSinh.Text);
            _cmd.Parameters.AddWithValue("@Status", cbxHoatDong.Checked);
            _cmd.ExecuteNonQuery();//Thực thi câu truy vấn không trả về dữ liệu

            _con.Close();
            Cach1LoadData();
            Cach2LoadData();
            Cach4LoadData();
        }
    }
}
