using BCrypt.Net;
using Humanizer.Bytes;
using System.Security.Cryptography;
using System.Text;

namespace TodoList.Code
{
    public class HashContent
    {
        public static hash(string content, Type type)
        {
            byte[] bytes = SHA512.HashData(Encoding.ASCII.GetBytes(content));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            if (type == string)
                return builder.ToString();
            else
                return builder;
        }

        public static string hash2(string content)
        {
            byte[] bytes = MD5.HashData(Encoding.ASCII.GetBytes(content));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string hash3(string content)
        {
            byte[] KeyString = Encoding.ASCII.GetBytes("RandomKeyForHashing");

            HMACSHA512 hashType = new()
            {
                Key = KeyString
            };

            byte[] bytes = hashType.ComputeHash(Encoding.ASCII.GetBytes(content));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string hash4(string content)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(content);
            byte[] salt = Encoding.ASCII.GetBytes("saltData");

            var hashtype = new HashAlgorithmName("SHA512");

            var newContent = Rfc2898DeriveBytes.Pbkdf2(bytes, salt, 10, hashtype, 32);

            return newContent.ToString();
        }


        //bcrypt hashing

        public static string hash5(string content)
        {
            return BCrypt.Net.BCrypt.HashPassword(content);
        }

        public static bool hash5validate(string content, string hashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(content,hashedValue);
        }

        public static string hash6(string content)
        {
            int itira = 10;
            return BCrypt.Net.BCrypt.HashPassword(content, itira, true);
        }

        public static bool hash6validate(string content, string hashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(content, hashedValue, true);
        }

        public static string hash7(string content)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashType = HashType.SHA512;

            return BCrypt.Net.BCrypt.HashPassword(content, salt, true, hashType);
        }

        public static bool hash7validate(string content, string hashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(content, hashedValue, true, HashType.SHA512);
        }
    }
}
