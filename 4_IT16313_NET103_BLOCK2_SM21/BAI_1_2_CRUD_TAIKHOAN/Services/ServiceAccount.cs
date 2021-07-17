using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAI_1_2_CRUD_TAIKHOAN.IServices;
using BAI_1_2_CRUD_TAIKHOAN.Models;

namespace BAI_1_2_CRUD_TAIKHOAN.Services
{
    class ServiceAccount:IServiceAccount
    {
        private List<Account> _lstAccounts;
        public ServiceAccount()
        {
            _lstAccounts = new List<Account>();
        }

        public string addAccount(Account account)
        {
            if (account == null) return "Thêm thất bại";
            _lstAccounts.Add(account);
            return "Thêm thành công";
        }

        public string updateAccount(Account account)
        {
            int index = _lstAccounts.FindIndex(c => c.Id == account.Id);
            if (index == -1) return "Không tìm thấy";
            _lstAccounts[index] = account;
            return "Sửa thành công";
        }

        public string removeAccount(int id)
        {
            if (id >= _lstAccounts.Count) return "Xóa thất bại";
            _lstAccounts.RemoveAt(_lstAccounts.FindIndex(c=>c.Id == id));
            return "Xóa thành công";
        }

        public List<Account> getLstAccounts()
        {
            return _lstAccounts;
        }

        public List<Account> getLstAccountsByAcc(string acc)
        {
            return _lstAccounts.Where(c => c.Acc.StartsWith(acc)).Select(c=>c).ToList();
        }

        public Account findAccountById(int id)
        {
            return _lstAccounts.FirstOrDefault(c => c.Id == id);
        }

        public void fillDataFromFile(List<Account> lsAccounts)
        {
            _lstAccounts = lsAccounts;
        }

        public string[] getYearOfBirths()
        {
            string[] arrYears = new string[2021 - 1900];
            int temp = 1900;
            for (int i = 0; i < arrYears.Length; i++)
            {
                arrYears[i] = Convert.ToString(temp);
                temp++;
            }
            return arrYears;
        }
    }
}
