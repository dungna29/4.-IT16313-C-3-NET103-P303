using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAI_1_2_CRUD_TAIKHOAN.IServices
{
   public interface IServiceFile
    {
        //1. Lưu File
        string saveFile<T>(string path,T lstobj);
        //2. Đọc File
        List<T> openFile<T>(string path);
    }
}
