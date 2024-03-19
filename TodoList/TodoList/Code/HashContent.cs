using System.Security.Cryptography;
using System.Text;

namespace TodoList.Code
{
    public class HashContent
    {
        public static string hash(string content)
        {
            using (SHA384 sha384 = SHA384.Create())
            {
                byte[] bytes = sha384.ComputeHash(Encoding.UTF8.GetBytes(content));

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
