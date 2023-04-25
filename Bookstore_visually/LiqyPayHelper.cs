using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_visually
{
    public static class LiqyPayHelper
    {
        public static string CreateSign(string data, string key)
        {
            string mydata = key + data + key;
            var sha1 = new System.Security.Cryptography.SHA1Managed();
            byte[] dt = Encoding.UTF8.GetBytes(mydata);
            byte[] res = sha1.ComputeHash(dt);
            return Convert.ToBase64String(res);

        }

        public static string CreateData(string data)
        {
            byte[] dataB = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(dataB);
        }
    }
}
