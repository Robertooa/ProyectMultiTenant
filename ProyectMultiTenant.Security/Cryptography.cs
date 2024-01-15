using ProyectMultiTenant.Domain.Contracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ProyectMultiTenant.Security
{
    public class Cryptography : ICryptography
    {
        public string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return FromBytesToStringHexadecimal(data);
        }
        public bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);
            return CompareStringHexadecimal(hashOfInput, hash);
        }

        private string FromBytesToStringHexadecimal(byte[] data)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private bool CompareStringHexadecimal(string inputOne, string inputTwo)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(inputOne, inputTwo);
        }
    }
}
