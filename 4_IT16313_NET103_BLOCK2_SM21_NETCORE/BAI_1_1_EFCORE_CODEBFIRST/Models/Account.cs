using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAI_1_1_EFCORE_CODEBFIRST.Models
{
    [Table("Accounts")]//Đặt tên bảng
    public class Account
    {
        [Key]//Khai báo khóa chính cho bảng
        public Guid Id { get; set; }
        [StringLength(28)]//Độ dài của chuỗi trong CSDL
        public string Acc { get; set; }
        [StringLength(28)]
        public string Pass { get; set; }
    }
}
