using System.Security.Cryptography;
using System.Text;

namespace TodoList.Code
{
    public class Encrypter
    {
        public static string encrypter(string context, string publicKey)
        {
            using(RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(publicKey);

                byte[] ToByteArray = Encoding.UTF8.GetBytes(context);
                byte[] Encrypt = rsa.Encrypt(ToByteArray, RSAEncryptionPadding.OaepSHA256);

                return Convert.ToBase64String(Encrypt);
            }
        }
    }
}
