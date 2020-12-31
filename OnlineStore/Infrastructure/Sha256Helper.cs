using System;
using System.Security.Cryptography;
using System.Text;

namespace OnlineStore.Infrastructure
{
    public class Sha256Helper
    {
        public string Hash(string value)
        {
            using var sha256 = SHA256.Create();
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(sha256.ComputeHash(valueBytes));
        }
    }
}
