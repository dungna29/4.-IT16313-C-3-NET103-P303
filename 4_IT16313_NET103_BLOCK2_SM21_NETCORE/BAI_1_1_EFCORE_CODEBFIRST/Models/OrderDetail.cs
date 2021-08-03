using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAI_1_1_EFCORE_CODEBFIRST.Models
{
    [Table("OrderDetails")]//Đặt tên bảng
    public class OrderDetail
    {
        [Key]//Khai báo khóa chính cho bảng
        public Guid Id { get; set; }
        [ForeignKey("IdProduct")]//Tên khóa phụ
        public Product Product { get; set; }
        [ForeignKey("IdOrder")]//Tên khóa phụ
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
