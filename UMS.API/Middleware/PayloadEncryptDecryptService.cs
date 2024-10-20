using System.Security.Cryptography;
using System.Text;

namespace UMS.API.Middleware
{
    public class PayloadEncryptDecryptService
    {
        private readonly IConfiguration _configuration;
        public PayloadEncryptDecryptService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        byte[] key = null;
        byte[] iv = null;

        public string EncryptResponse(string value)
        {
            byte[] bEncrypt = null;
            try
            {
                DESCryptoServiceProvider desSprovider = new DESCryptoServiceProvider();
                Encoding utf8Encoding = new UTF8Encoding();
                byte[] key = utf8Encoding.GetBytes(_configuration.GetSection("Security")["key"]);
                byte[] iv = utf8Encoding.GetBytes(_configuration.GetSection("Security")["iv"]);
                ICryptoTransform encryptor = desSprovider.CreateEncryptor(key, iv);
                desSprovider.Padding = PaddingMode.PKCS7;
                byte[] bValue = utf8Encoding.GetBytes(value);
                bEncrypt = encryptor.TransformFinalBlock(bValue, 0, bValue.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Encryption failed: " + e.Message);
            }
            return Convert.ToBase64String(bEncrypt);
        }

        public string DecryptRequest(string value)
        {
            DESCryptoServiceProvider desSprovider = new DESCryptoServiceProvider();
            Encoding utf1 = new UTF8Encoding();
            key = utf1.GetBytes(_configuration.GetSection("Security")["key"]);
            iv = utf1.GetBytes(_configuration.GetSection("Security")["iv"]);
            ICryptoTransform decryptor = desSprovider.CreateDecryptor(key, iv);
            Encoding utf = new UTF8Encoding();
            value = value.Replace(" ", "+").Replace("'", "");
            byte[] bEncrypt = Convert.FromBase64String(value);
            byte[] bDecrupt = decryptor.TransformFinalBlock(bEncrypt, 0, bEncrypt.Length);
            return utf.GetString(bDecrupt);
        }
    }
}
