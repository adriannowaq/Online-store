using System;
using System.Text;

namespace OnlineStore.Infrastructure.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        public string GenerateToken()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var token = new StringBuilder(Guid.NewGuid().ToString("N"));

            for (int i = 0; i < 50; i++)
            {
                token.Append(chars[random.Next(chars.Length)]);
            }

            return token.ToString();
        }
    }
}
