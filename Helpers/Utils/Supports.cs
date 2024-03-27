using System;
using System.Text;
using System.Security.Cryptography;

namespace SuperHeros.Helpers.Utils
{
    public static class Supports
    {

        public static string GetMd5HashedOutput(string input)
        {
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, input);
            return hash;
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
