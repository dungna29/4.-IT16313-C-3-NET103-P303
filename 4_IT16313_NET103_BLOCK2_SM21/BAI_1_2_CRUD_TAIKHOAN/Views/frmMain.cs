using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAI_1_2_CRUD_TAIKHOAN.IServices;
using BAI_1_2_CRUD_TAIKHOAN.Models;
using BAI_1_2_CRUD_TAIKHOAN.Services;

namespace BAI_1_2_CRUD_TAIKHOAN.Views
{
    public partial class frmMain : Form
    {
        private IServiceAccount _iServiceAccount;
        private IServiceFile _iServiceFile;
        private string _filePath;
        private List<Account> _lstAccounts;
        private Account _account;
        public frmMain()
        {
            InitializeComponent();
            //Khởi tạo
            _iServiceAccount = new ServiceAccount();
            _iServiceFile = new ServiceFile();
            _lstAccounts = new List<Account>();
            loadCbxNamSinh();//Đưa phần load combobox lên contructor
            //Combobox được load mặc định từ vị trí cuối cùng trừ 10
            cmbNamSinh.SelectedIndex = _iServiceAccount.getYearOfBirths().Length - 10;
            rbtnNam.Checked = true;
            cbxKhongHoatDong.Checked = true;
        }

        public void SenderDataFromLogin(string path, TextBox acc)
        {
            _filePath = path;//Gán lại đường dẫn
            lbl_AccLogin.Text = "Chào Bạn: " +  acc.Text;//Gán giá trị của control bên form login cho label ở form Main.
            //Sau khi có đường dẫn tiến hành đọc file và đổ dữ liệu vào List trên Main
            _lstAccounts = _iServiceFile.openFile<Account>(path);
            //Đổ dữ liệu vào bên Class Service
            _iServiceAccount.fillDataFromFile(_lstAccounts);
            loadDataGrid();//Load dữ liệu lên Grid
        }

        void loadDataGrid()
        {
            _lstAccounts = new List<Account>();//Làm sạch List mỗi lần chạy vào phương thức này
            _lstAccounts = _iServiceAccount.getLstAccounts();//Đổ dữ liệu từ bên Service về List tại Main
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
                dgrid_Account.Rows.Add(x.Id,x.Acc,x.Pass,x.Sex == 1?"Nam":"Nữ",DateTime.Now.Year - x.YearofBirth,x.YearofBirth,x.Status?"Hoạt động":"Không hoạt đông");
            }
        }
        void loadCbxNamSinh()
        {
            foreach (var x in _iServiceAccount.getYearOfBirths())
            {
                cmbNamSinh.Items.Add(x);
            }
        }

        private void cbxHoatDong_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxHoatDong.Checked)
            {
                cbxKhongHoatDong.Checked = false;
            }
        }

        private void cbxKhongHoatDong_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxKhongHoatDong.Checked)
            {
                cbxHoatDong.Checked = false;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            //Fill data vào các thuộc tính đối tượng
            _account = new Account();
            _account.Id = _lstAccounts == null ? 1 : _lstAccounts.Count;
            _account.Acc = txtAcc.Text;
            _account.Pass = txtPass.Text;
            _account.Sex = rbtnNam.Checked ? 1 : 0;
            _account.YearofBirth = Convert.ToInt32(cmbNamSinh.SelectedItem);
            _account.Status = cbxHoatDong.Checked;
            //Tiến hành thêm đối tượng thông qua phương thức service
            MessageBox.Show(_iServiceAccount.addAccount(_account),"Thông báo");
            //Sau khi thi thêm thì load lại data dưới Grid
            loadDataGrid();
        }

        private void mn_LuuFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_iServiceFile.saveFile(_filePath, _lstAccounts), "Thông báo");
        }
    }
}
