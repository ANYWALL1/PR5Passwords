using System;
using System.Security.Cryptography;
using System.Text;

namespace HashPasswords
{
    public class Hash
    {
        /// <summary>
        /// Создает хеш-значение SHA256 для входной строки.
        /// </summary>
        /// <param name="password">Строка (пароль), которую нужно хешировать.</param>
        /// <returns>Строковое представление хеша SHA256.</returns>
        public static string GenerateHash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}