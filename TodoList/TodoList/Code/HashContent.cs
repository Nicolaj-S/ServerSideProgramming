using BCrypt.Net;
using Humanizer.Bytes;
using System.Security.Cryptography;
using System.Text;

namespace TodoList.Code
{
    public class HashContent
    {
        public static object SequentialHash(string content, string Type)
        {
            string hash1Result = hash(content);
            string hash2Result = hash2(hash1Result);
            string hash3Result = hash3(hash2Result);
            string hash4Result = hash4(hash3Result);


            if (Type == "string")
                return hash4Result;
            else
                return Encoding.ASCII.GetBytes(hash4Result);
        }
        public static string hash(string content)
        {
            byte[] bytes = SHA512.HashData(Encoding.ASCII.GetBytes(content));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
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

            var hashType = new HashAlgorithmName("SHA512");

            byte[] newContent = Rfc2898DeriveBytes.Pbkdf2(bytes, salt, 10, hashType, 32);

            StringBuilder builder = new StringBuilder();
            foreach (byte b in newContent)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
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
