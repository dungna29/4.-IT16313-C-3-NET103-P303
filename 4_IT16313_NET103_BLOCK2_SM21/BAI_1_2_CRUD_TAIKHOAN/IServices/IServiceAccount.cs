using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAI_1_2_CRUD_TAIKHOAN.Models;

namespace BAI_1_2_CRUD_TAIKHOAN.IServices
{
    public interface IServiceAccount
    {
        //1. Phương thức thêm tài khoản
        string addAccount(Account account);

        //2. Phương thức sửa tài khoản
        string updateAccount(Account account);

        //3. Phương thức xóa tài khoản
        string removeAccount(int id);

        //4. Phương thức lấy danh sách Account
        List<Account> getLstAccounts();

        //5. Phương thức tìm kiếm gần đúng
        List<Account> getLstAccountsByAcc(string acc);

        //6. Phương tìm kiếm theo id
        Account findAccountById(int id);
        
        //7. Fill Data từ File đọc lên vào List trong Service
        void fillDataFromFile(List<Account> lsAccounts);

        //8. Phương thức lấy ra danh sách năm sinh
        string[] getYearOfBirths();
    }
}
