using System;
using System.Security.Cryptography;
using System.Text;

namespace EscapeRoom.Services
{
    public static class Sha256Hasher
    {
        public static string HashToHex(string input)
            => BytesToHex(HashBytes(Encoding.UTF8.GetBytes(input)));

        public static byte[] HashBytes(byte[] data)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(data);
        }

        public static bool VerifyHex(string input, string expectedHex)
            => string.Equals(HashToHex(input), NormalizeHex(expectedHex), StringComparison.OrdinalIgnoreCase);

        private static string BytesToHex(byte[] bytes)
        {
            var c = new char[bytes.Length * 2];
            int ci = 0;
            foreach (var b in bytes)
            {
                c[ci++] = GetHexValue(b / 16);
                c[ci++] = GetHexValue(b % 16);
            }
            return new string(c);
        }

        private static char GetHexValue(int i) => (char)(i < 10 ? i + '0' : i - 10 + 'a');

        private static string NormalizeHex(string hex) => hex.Replace(" ", "").Replace("-", "");
    }
}
