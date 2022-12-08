using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Services
{
    public class Utility
    {
        public static string GenerateUniqueKey(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            var result = new StringBuilder(size);
            foreach (var item in data)
            {
                result.Append(chars[item % (chars.Length)]);
            }
            return result.ToString();
        }
        public static string GenerateUniqueNumericKey(int size)
        {
            char[] chars = "1234567890".ToCharArray();
            byte[] data = new byte[size];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            var result = new StringBuilder(size);
            foreach (var item in data)
            {
                result.Append(chars[item % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
