using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using BAI_1_2_CRUD_TAIKHOAN.IServices;

namespace BAI_1_2_CRUD_TAIKHOAN.Services
{
    class ServiceFile:IServiceFile
    {
        private FileStream _fs;
        private BinaryFormatter _bf;
        public string saveFile<T>(string path, T lstobj)
        {
            try
            {
                _fs = new FileStream(path, FileMode.Create);
                _bf = new BinaryFormatter();
                _bf.Serialize(_fs, lstobj);
                _fs.Close();
                return "Ghi file thành công";
            }
            catch (Exception e)
            {
                _fs.Close();
                Console.WriteLine(e);
                return "Ghi file thất bại";
            }
        }

        public List<T> openFile<T>(string path)
        {
            try
            {
                List<T> lstobj = new List<T>();
                _fs = new FileStream(path, FileMode.Open);
                _bf = new BinaryFormatter();
                var data = _bf.Deserialize(_fs);
                lstobj = (List<T>) data;
                _fs.Close();
                return lstobj;
            }
            catch (Exception e)
            {
                _fs.Close();
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
