using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAI_1_1_EFCORE_CODEBFIRST.DBContext_FPOLY;
using BAI_1_1_EFCORE_CODEBFIRST.Models;


namespace BAI_1_1_EFCORE_CODEFIRST.Services
{
    public class ServiceQL
    {
        private List<Account> _lstAccounts;
        private List<Order> _lsvOrders;
        private List<OrderDetail> _lstOrderDetails;
        private List<Product> _lstProducts;
        private List<Role> _lstRoles;
        private DBContext_Dungna _dbContext;
        public ServiceQL()
        {
            _dbContext = new DBContext_Dungna();
            _lstAccounts = new List<Account>();
            _lsvOrders = new List<Order>();
            _lstOrderDetails = new List<OrderDetail>();
            _lstProducts = new List<Product>();
            _lstRoles = new List<Role>();
            GetlstAccountsFromDB();
            GetlstRolesFromDB();
            GetlstProductsFromDB();
        }

        #region 1. Chức năng quản lý tài khoản
        public List<Role> GetlstRoles()
        {
            return _lstRoles;
        }

        public void GetlstRolesFromDB()
        {
            _lstRoles = _dbContext.Roles.ToList();
        }
        public List<Account> GetlstAccounts()
        {
            return _lstAccounts;
        }

        public void GetlstAccountsFromDB()
        {
            _lstAccounts = _dbContext.Accounts.ToList();
        }

        public string AddAccount(Account account)
        {
            _dbContext.Add(account);
            _dbContext.SaveChanges();
            GetlstAccountsFromDB();
            return "Thêm thành công";
        }
        public string UpdateAccount(Account account)
        {
            _dbContext.Update(account);
            _dbContext.SaveChanges();
            GetlstAccountsFromDB();
            return "Sửa thành công";
        }
        public string DeleteAccount(Guid id)
        {
            _dbContext.Remove(_lstAccounts.FirstOrDefault(c=>c.Id == id));
            _dbContext.SaveChanges();
            GetlstAccountsFromDB();
            return "Xóa thành công";
        }
        //Tìm kiếm gần đúng
        public List<Account> GetlstAccounts(string acc)
        {
            return _lstAccounts.Where(c=>c.Acc.ToLower().StartsWith(acc)).ToList();
        }
        #endregion

        #region 2. Chức năm thêm sản phẩm
        public List<Product> GetlstProduct()
        {
            return _lstProducts;
        }
        public List<Product> GetlstProduct(string name)
        {
            return _lstProducts.Where(c => c.Name.StartsWith(name)).ToList();
        }
        public void GetlstProductsFromDB()
        {
            _lstProducts = _dbContext.Products.ToList();
        }

        public string AddProduct(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();
            GetlstProductsFromDB();
            return "Thêm thành công";
        }
        public string UpdateProduct(Product product)
        {
            _dbContext.Update(product);
            _dbContext.SaveChanges();
            GetlstProductsFromDB();
            return "Sửa thành công";
        }
        public string DeleteProduct(Guid id)
        {
            _dbContext.Remove(_lstProducts.FirstOrDefault(c => c.Id == id));
            _dbContext.SaveChanges();
            GetlstProductsFromDB();
            return "Xóa thành công";
        }
        #endregion
    }
}
