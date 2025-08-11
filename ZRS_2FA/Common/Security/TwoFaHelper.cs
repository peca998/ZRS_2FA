using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using OtpNet;

namespace Common.Security
{
    public static class TwoFaHelper
    {
        public static string GenerateSecret(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"; // Base32 characters
            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            StringBuilder result = new();
            foreach (byte b in randomBytes)
            {
                result.Append(chars[b % chars.Length]);
            }
            return result.ToString();
        }

        public static string GenerateQrCodeUrl(string secret, string username, string issuer = "Zrs_Projekat")
        {
            return $"otpauth://totp/{issuer}:{username}?secret={secret}&issuer={issuer}&digits=6";
        }

        public static string GenerateQrCodeImage(string qrCodeUrl)
        {
            using QRCodeGenerator qRCodeGenerator = new();
            using QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrCodeUrl, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qRCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            return Convert.ToBase64String(qrCodeImage);
        }

        public static bool ValidateCode(string secret, string enteredCode)
        {
            if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(enteredCode) || enteredCode.Length != 6)
            {
                return false;
            }
            var secretBytes = Base32Encoding.ToBytes(secret);
            Totp totp = new(secretBytes);
            bool isValid = totp.VerifyTotp(enteredCode, out long timeStepMatched, new VerificationWindow(2, 2));
            return isValid;
        }
    }
}
