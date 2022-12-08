using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ASL.Utility
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
