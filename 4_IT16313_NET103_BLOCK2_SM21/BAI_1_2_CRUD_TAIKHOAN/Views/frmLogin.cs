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
    public partial class frmLogin : Form
    {
        private IServiceAccount _iServiceAccount;
        private IServiceFile _iServiceFile;
        private string _filePath;
        private List<Account> _lstAccounts;
        public frmLogin()
        {
            InitializeComponent();
            _iServiceAccount = new ServiceAccount();
            _iServiceFile = new ServiceFile();
            _lstAccounts = new List<Account>();
        }

        private void btn_OpenData_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _filePath = dlg.FileName;//Lấy được đường dẫn tuyệt đối của file
                lbl_DuongDan.Text = _filePath;//Hiển thị đường dẫn trên giao diện
                //Đọc dữ liệu từ file data mình chọn và gán vào List hiện tại
                _lstAccounts = _iServiceFile.openFile<Account>(_filePath);
                //Đổ dữ liệu đã đọc lên về phía ServiceAccount
                _iServiceAccount.fillDataFromFile(_lstAccounts);
            }
        }

        private void lbl_DangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegister frmRegister = new frmRegister();
            //Truyền đường dẫn của file lên form đăng ký
            frmRegister.SenderPathFromLoginToRegister(_filePath);
            this.Hide();//Ẩn form hiện tại
            frmRegister.Show();//Hiển thị form cần hiển thị
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (_iServiceAccount.getLstAccounts() == null)
            {
                MessageBox.Show("File data rỗng", "Thông báo");
                return;
            }
            if (_iServiceAccount.getLstAccounts().Any(c=>c.Acc == txt_Acc.Text && c.Pass == txt_Pass.Text))
            {
                this.Hide();
                MessageBox.Show("Đăng nhập thành công", "Thông báo");
                frmMain frmMain = new frmMain();
                frmMain.SenderDataFromLogin(_filePath,txt_Acc);//Truyền data từ Login lên Main
                frmMain.Show();
            }
        }
    }
}
