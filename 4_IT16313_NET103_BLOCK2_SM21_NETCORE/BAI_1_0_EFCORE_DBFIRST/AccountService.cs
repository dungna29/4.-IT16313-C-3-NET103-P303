using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAI_1_0_EFCORE_DBFIRST.Context;
using BAI_1_0_EFCORE_DBFIRST.Models;

namespace BAI_1_0_EFCORE_DBFIRST
{
    class AccountService
    {
        private List<AccountsAdo> _lstAccountsAdos;
        private DatabaseContext _dbContext;//Nhân viên phục vụ có nhiệm vụ truy vấn CSDL
        public AccountService()
        {
            _lstAccountsAdos = new List<AccountsAdo>();
            _dbContext = new DatabaseContext();
            GetLstAccountsAdosDB();


        }

        public List<AccountsAdo> GetLstAccountsAdos()
        {
            return _lstAccountsAdos;
        }

        void GetLstAccountsAdosDB()//Phương thức truy vấn vào CSDL và gán lại dữ liệu mới cho LIST đối tượng
        {
            // _dbContext.AccountsAdos.ToList() - Đây chính là hành động chọc vào CSDL
            _lstAccountsAdos = _dbContext.AccountsAdos.ToList();
        }

        /// <summary>
        /// Phương thức Insert vào CSDL
        /// </summary>
        /// <param name="accounts">Cần truyền 1 đối tượng vào trong phương thức</param>
        /// <returns>True khi insert thành công</returns>
        public bool Insert(AccountsAdo accounts)
        {
            _dbContext.AccountsAdos.Add(accounts);
            _dbContext.SaveChanges();
            return true;
        }
        public bool Update(AccountsAdo accounts)
        {
            _dbContext.AccountsAdos.Update(accounts);
            _dbContext.SaveChanges();
            return true;
        }
        public bool Delete(Guid id)
        {
            var acc = _lstAccountsAdos.FirstOrDefault(c => c.Id == id);
            _dbContext.AccountsAdos.Remove(acc);
            _dbContext.SaveChanges();
            return true;
        }

        //Tìm kiếm gần đúng
        public List<AccountsAdo> GetLstAccountsAdosByAcc(string acc)
        {
            return _lstAccountsAdos.Where(c=>c.Acc.StartsWith(acc)).ToList();
        }
    }
}
