using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAI_1_1_EFCORE_CODEBFIRST.Models;
using Microsoft.EntityFrameworkCore;

namespace BAI_1_1_EFCORE_CODEBFIRST.DBContext_FPOLY
{
    class DBContext_Dungna:DbContext//Phải kế thừa lớp DbContext
    {
        //1. Cấu hình kết nối vào CSDL
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Khi cấu hình đường dẫn nếu chưa có DB thì có đặt tên DB ngay tại đây
                optionsBuilder.UseSqlServer("Data Source=DUNGNA_PC2021\\SQLEXPRESS;Initial Catalog=IT16313_EFCODEFIRST;Persist Security Info=True;User ID=dungna29;Password=123");
            }
        }

        //2. Khai báo các bảng
        public DbSet<Account> Accounts { get; set; }
    }
}
