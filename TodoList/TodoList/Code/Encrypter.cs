using System.Security.Cryptography;
using System.Text;

namespace TodoList.Code
{
    public class Encrypter
    {
        public static string encrypter(string context, string publicKey)
        {
            using(RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                byte[] ToByteArray = Encoding.UTF8.GetBytes(context);
                byte[] Encrypt = rsa.Encrypt(ToByteArray, true);

                return Convert.ToBase64String(Encrypt);
            }
        }
    }
}
