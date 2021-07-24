using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAI_1_0_EFCORE_DBFIRST.Context;

namespace BAI_1_0_EFCORE_DBFIRST
{
    class AccountService
    {
        private DatabaseContext _dbContext;
        public AccountService()
        {
            _dbContext = new DatabaseContext();
            var lstAcc = _dbContext.AccountsAdos.ToList();

        }
        
    }
}
