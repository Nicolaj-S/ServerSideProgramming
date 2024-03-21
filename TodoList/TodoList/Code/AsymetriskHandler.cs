using System.Security.Cryptography;
using System.Text;

namespace TodoList.Code
{
    public class AsymetriskHandler
    {
        private string _PrivateKey;
        public string _publicKey;

        public AsymetriskHandler()
        {
            if (File.Exists)
            {
                using (RSA rsa = RSA.read())
                {
                    RSAParameters privateKey = rsa.ExportParameters(true);
                    _PrivateKey = rsa.ToXmlString(true);

                    RSAParameters publicKey = rsa.ExportParameters(false);
                    _publicKey = rsa.ToXmlString(false);
                }
            }
            else
            {
                using (RSA rsa = RSA.Create())
                {
                    RSAParameters privateKey = rsa.ExportParameters(true);
                    _PrivateKey = rsa.ToXmlString(true);

                    RSAParameters publicKey = rsa.ExportParameters(false);
                    _publicKey = rsa.ToXmlString(false);
                }
            }
            
        }

        public string Encrypt(string context) =>
            Encrypter.encrypter(context, _publicKey);

        public string Decrypt(string context)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_PrivateKey);

                byte[] ToByteArray = Convert.FromBase64String(context);
                byte[] decrypted = rsa.Decrypt(ToByteArray,true);

                return Encoding.UTF8.GetString(decrypted);
            }
        }
    }
}
