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
    public partial class frmRegister : Form
    {
        private IServiceAccount _iServiceAccount;
        private IServiceFile _iServiceFile;
        private string _filePath;
        private List<Account> _lstAccounts;
        private Account _account;
        public frmRegister()
        {
            InitializeComponent();
            //Khởi tạo
            _iServiceAccount = new ServiceAccount();
            _iServiceFile = new ServiceFile();
            _lstAccounts = new List<Account>();
            _account = new Account();
            loadCbxNamSinh();//Đưa phần load combobox lên contructor
            //Combobox được load mặc định từ vị trí cuối cùng trừ 10
            cmbNamSinh.SelectedIndex = _iServiceAccount.getYearOfBirths().Length - 10;
            rbtnNam.Checked = true;
        }

        void loadCbxNamSinh()
        {
            foreach (var x in _iServiceAccount.getYearOfBirths())
            {
                cmbNamSinh.Items.Add(x);
            }
        }
        //Triển khai 1 phương thức để truyền đường dẫn file data từ form login sang form đăng ký
        public void SenderPathFromLoginToRegister(string duongDan)
        {
            _filePath = duongDan;//Gán đường dẫn từ login vào form đăng ký
        }

        private void btn_TaoTaiKhoan_Click(object sender, EventArgs e)
        {
            //Bước 1: Validate form
            //Bước 2: Đọc dữ liệu từ file lên để khi thêm mới thì sẽ thêm đối tượng mới vào cuối của danh sách. Nếu không làm như vậy khi ghi file sẽ bị ghi đè và mất dữ liệu cũ chỉ còn dữ liệu mới
            _lstAccounts = _iServiceFile.openFile<Account>(_filePath);
            if (_lstAccounts == null)
            {
                _lstAccounts = new List<Account>();
            }
            //Bước 3: Lấy dữ liệu từ form lưu vào đối tượng rồi add vào List
            _account.Id = _lstAccounts == null ? 1 : _lstAccounts.Count;
            _account.Acc = txtAcc.Text;
            _account.Pass = txtPass.Text;
            _account.Sex = rbtnNam.Checked ? 1 : 0;
            _account.YearofBirth = Convert.ToInt32(cmbNamSinh.SelectedItem);
            _lstAccounts.Add(_account);
            //Bước 4: Sau khi thêm vào List tiến hành ghi dữ liệu vào file
            MessageBox.Show(_iServiceFile.saveFile(_filePath, _lstAccounts),"Thông báo");
            //Bước 5: Sau khi lưu thành công sẽ trở lại Form Login
            frmLogin frmLogin = new frmLogin();
            this.Hide();//Ẩn form hiện tại
            frmLogin.Show();//Hiển thị form cần hiển thị lên
        }
    }
}
