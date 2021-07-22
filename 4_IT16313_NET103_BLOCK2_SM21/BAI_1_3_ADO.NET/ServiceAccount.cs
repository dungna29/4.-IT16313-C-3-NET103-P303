using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAI_1_3_ADO.NET
{
    class ServiceAccount
    {
        private List<Account> _lstAccounts;
        private SqlConnection _con;
        private SqlCommand _cmd;
        private string _connStr;
        private Account _account;
        public ServiceAccount(string connStr)
        {
            _lstAccounts = new List<Account>();
            _connStr = connStr;//Khi khởi tạo lớp thì sẽ gán đường dẫn kết nối vào CSDL
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

        public string removeAccount(Guid id)
        {
          
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

        public Account findAccountById(Guid id)
        {
            return _lstAccounts.FirstOrDefault(c => c.Id == id);
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

        //Phần triển DB
        public SqlConnection GetSqlConnection(string sqlConnectionString)
        {
            return new SqlConnection(sqlConnectionString);
        }

        public SqlCommand GetSqlCommand(string query, SqlConnection conn)
        {
            return new SqlCommand(query, conn);
        }

        //Phương thức dùng để lấy dữ liệu từ DB
        public List<Account> GetLstAccountsDB()
        {
            _con = GetSqlConnection(_connStr);
            _cmd = GetSqlCommand("Select * from Accounts_ADO", _con);
            _con.Open();
            var data = _cmd.ExecuteReader();
            _lstAccounts = new List<Account>();
            while (data.Read())
            {
                _account = new Account();
                _account.Id = new Guid(data["Id"].ToString());
                _account.Acc = data["Acc"].ToString();
                _account.Pass = data["Pass"].ToString();
                _account.Sex = Convert.ToInt32(data["Sex"].ToString());
                _account.YearofBirth = Convert.ToInt32(data["YearofBirth"].ToString());
                _account.Status = Convert.ToBoolean(data["Status"].ToString());
                _lstAccounts.Add(_account);
            }
            _con.Close();
            return _lstAccounts;
        }
    }
}
