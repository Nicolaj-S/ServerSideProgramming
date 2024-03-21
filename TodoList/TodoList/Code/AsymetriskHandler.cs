using System.Security.Cryptography;
using System.Text;

namespace TodoList.Code
{
    public class AsymetriskHandler
    {
        private string _privateKey;
        public string PublicKey;
        private static readonly string KeyFilePath = "keys.xml";

        public AsymetriskHandler()
        {
            string projectRootPath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPathToFile = Path.Combine(projectRootPath, KeyFilePath);

            if (File.Exists(fullPathToFile))
            {
                string xmlKeys = File.ReadAllText(fullPathToFile);
                using (RSA rsa = RSA.Create())
                {
                    rsa.FromXmlString(xmlKeys);
                    _privateKey = rsa.ToXmlString(true);
                    PublicKey = rsa.ToXmlString(false);
                }
            }
            else
            {
                using (RSA rsa = RSA.Create())
                {
                    // Generate new keys
                    _privateKey = rsa.ToXmlString(true);
                    PublicKey = rsa.ToXmlString(false);

                    // Save the private key to file for future use
                    File.WriteAllText(fullPathToFile, _privateKey);
                }
            }
        }

        public string Encrypt(string content) => Encrypter.encrypter(content, PublicKey);

        public string Decrypt(string content)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(_privateKey);

                byte[] dataToDecrypt = Convert.FromBase64String(content);
                byte[] decryptedData = rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);

                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}