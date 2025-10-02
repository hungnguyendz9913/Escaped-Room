using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EscapeRoom.Services
{
    public static class Sha256Hasher
    {
        // Hash string -> hex
        public static string HashToHex(string input)
            => BytesToHex(HashBytes(Encoding.UTF8.GetBytes(input)));

        // Hash string -> Base64
        public static string HashToBase64(string input)
            => Convert.ToBase64String(HashBytes(Encoding.UTF8.GetBytes(input)));

        // Hash byte[] -> byte[]
        public static byte[] HashBytes(byte[] data)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(data);
        }

        // Hash stream -> byte[]
        public static byte[] HashStream(Stream stream)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(stream);
        }

        // Hash string + salt (hex)
        public static string HashWithSaltToHex(string input, byte[] salt)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var combined = new byte[salt.Length + inputBytes.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(inputBytes, 0, combined, salt.Length, inputBytes.Length);
            return BytesToHex(HashBytes(combined));
        }

        // Verify: so sánh input với hash hex đã có
        public static bool VerifyHex(string input, string expectedHex)
            => string.Equals(HashToHex(input), NormalizeHex(expectedHex), StringComparison.OrdinalIgnoreCase);

        // Tạo salt ngẫu nhiên
        public static byte[] GenerateSalt(int size = 16)
        {
            var salt = new byte[size];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

        // Helpers
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
