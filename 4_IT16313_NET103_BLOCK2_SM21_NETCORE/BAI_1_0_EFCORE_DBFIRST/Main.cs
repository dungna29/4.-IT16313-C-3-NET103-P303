using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAI_1_0_EFCORE_DBFIRST.Models;

namespace BAI_1_0_EFCORE_DBFIRST
{
    public partial class Main : Form
    {
        private AccountService _accountService;
        private List<AccountsAdo> _lstAccounts;
        public Main()
        {
            InitializeComponent();
            _accountService = new AccountService();
            Cach1LoadData();
        }
        void Cach1LoadData()
        {
            _lstAccounts = new List<AccountsAdo>();//Làm sạch List mỗi lần chạy vào phương thức này
            _lstAccounts = _accountService.GetLstAccountsAdos();//Đổ dữ liệu từ bên Service về List tại Main
            //Đếm số lượng thuộc tính có trong 1 đối tượng
            Type type = typeof(AccountsAdo);
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
                dgrid_Account.Rows.Add(x.Id, x.Acc, x.Pass, x.Sex == 1 ? "Nam" : "Nữ", DateTime.Now.Year - x.YearofBirth, x.YearofBirth, x.Status== true ? "Hoạt động" : "Không hoạt đông");
            }
        }
    }
}
