using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Util
{
    public class MD5Hasher
    {
        public static string GetMD5Hash(string stringToHash)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] strByteArray = Encoding.Default.GetBytes(stringToHash);
            byte[] result = md5.ComputeHash(strByteArray);
            return System.BitConverter.ToString(result);
        }
    }
}
