using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EscapeRoom.Services
{

    public static class AesGcmHelper
    {
        public static string EncryptedEmail { get; } = "sjBzfrqdSrdyxPRC9q95qLtpJm9VeiIlZOEJhcY+CAnLFTVpLFOuz1JH7/7X7MwY+uHMmuqrldw2";
        // Decrypt from Base64 produced above
        public static string? DecryptFromBase64(byte[] key, string base64Input)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(base64Input)) return "";

            var buf = Convert.FromBase64String(base64Input);
            if (buf.Length < 12 + 16) throw new ArgumentException("Invalid input");

            byte[] nonce = new byte[12];
            byte[] tag = new byte[16];
            byte[] cipher = new byte[buf.Length - 12 - 16];

            Buffer.BlockCopy(buf, 0, nonce, 0, nonce.Length);
            Buffer.BlockCopy(buf, nonce.Length, tag, 0, tag.Length);
            Buffer.BlockCopy(buf, nonce.Length + tag.Length, cipher, 0, cipher.Length);

            byte[] plain = new byte[cipher.Length];
            try
            {
                using (var aesGcm = new AesGcm(key))
                {
                    aesGcm.Decrypt(nonce, cipher, tag, plain, null);
                }
                return Encoding.UTF8.GetString(plain);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
    }
}
