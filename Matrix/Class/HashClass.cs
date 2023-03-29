using System;
using System.Security.Cryptography;
using System.Text;

namespace Matrix.Class
{
    public static class HashClass
    {
        public static string HashPassword(string password) //Данный метод однократно шифрует пароль, для безопасности хранения в бд
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
