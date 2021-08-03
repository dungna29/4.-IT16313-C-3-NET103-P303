using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAI_1_1_EFCORE_CODEBFIRST.Models
{
    [Table("Roles")]//Đặt tên bảng
    public class Role
    {
            [Key]//Khai báo khóa chính cho bảng
            public Guid Id { get; set; }
            [StringLength(15)]//Độ dài của chuỗi trong CSDL
            public string Code { get; set; }
            [StringLength(25)]
            public string Name { get; set; }
        }
}
