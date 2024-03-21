using Microsoft.AspNetCore.DataProtection;

namespace TodoList.Code
{
    public class symetriskHandler
    {
        private readonly IDataProtector _dataProtector;

        public symetriskHandler(IDataProtectionProvider dataProtector)
        {
            _dataProtector = dataProtector.CreateProtector("RandomKeyForHashing");
        }

        public string Encrypt(string textToEncrypt) => _dataProtector.Protect(textToEncrypt);

        public string Decrypt(string textToDecrypt) => _dataProtector.Unprotect(textToDecrypt);
    }
}
