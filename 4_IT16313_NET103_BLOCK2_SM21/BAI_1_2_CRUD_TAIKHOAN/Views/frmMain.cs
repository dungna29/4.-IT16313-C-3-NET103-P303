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

        private void dgrid_Account_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //1. Lấy được index row khi click vào grid
            int rowIndex = e.RowIndex;
            if (rowIndex == _lstAccounts.Count) return;//Khi bấm vào dòng cuối cùng của grid sẽ nằm ngoài index của List thì sẽ ko làm gì
            //2. Sử dụng phương thức tìm kiếm bên Service Acc
            var id = dgrid_Account.Rows[rowIndex].Cells[0].Value;//Lấy giá trị tại dòng bấm và cột số 0 chính là cột ID.
            _account = new Account();
            _account = _iServiceAccount.findAccountById(Convert.ToInt32(id));

            //3. Sau khi tìm đc đối tượng sẽ tiến hành fill các thuộc tính lên trên các control
            txtAcc.Text = _account.Acc;
            txtPass.Text = _account.Pass;
            rbtnNam.Checked = _account.Sex == 1 ? true : false;
            rbtnNu.Checked = _account.Sex == 0 ? true : false;
            cmbNamSinh.SelectedIndex = cmbNamSinh.FindString(_account.YearofBirth.ToString());
            cbxHoatDong.Checked = _account.Status;
            cbxKhongHoatDong.Checked = _account.Status == false ? true : false;

        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_iServiceAccount.removeAccount(_account.Id),"Thông báo");
            //Sau khi xóa cần load data
            loadDataGrid();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            _account.Acc = txtAcc.Text;
            _account.Pass = txtPass.Text;
            _account.Sex = rbtnNam.Checked ? 1 : 0;
            _account.YearofBirth = Convert.ToInt32(cmbNamSinh.SelectedItem);
            _account.Status = cbxHoatDong.Checked;
            MessageBox.Show(_iServiceAccount.updateAccount(_account), "Thông báo");
            //Sau khi sửa cần load data
            loadDataGrid();
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            loadDataGridByAcc(txtTimKiem.Text);
        }
        void loadDataGridByAcc(string acc)
        {
            _lstAccounts = new List<Account>();//Làm sạch List mỗi lần chạy vào phương thức này
            _lstAccounts = _iServiceAccount.getLstAccountsByAcc(acc);//Đổ dữ liệu từ bên Service về List tại Main
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

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txtAcc.Text = "";
            txtPass.Text = "";
            cmbNamSinh.SelectedIndex = _iServiceAccount.getYearOfBirths().Length - 10;
            rbtnNam.Checked = true;
            cbxKhongHoatDong.Checked = true;
            txtTimKiem.Text = "";
        }
    }
}
