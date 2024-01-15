using ProyectMultiTenant.Domain.Contracts;
using System;
using System.Security.Cryptography;

namespace ProyectMultiTenant.Security
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string Generate(int length)
        {
            using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[length];
                cryptRNG.GetBytes(tokenBuffer);
                return Convert.ToBase64String(tokenBuffer).Remove(length);
            }

        }
    }
}
