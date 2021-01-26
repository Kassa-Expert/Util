using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KassaExpert.Util.Lib.Encryption.Impl
{
    internal class DefaultEncryption : IEncryption
    {
        public byte[] EncodeRevenue(long umsatz)
        {
            byte[] umsatzBytes = BitConverter.GetBytes(umsatz);
            Array.Resize(ref umsatzBytes, 16);

            // reverse to get big-endian array
            Array.Reverse(umsatzBytes, 0, umsatzBytes.Length);
            return umsatzBytes;
        }

        public byte[] GenerateIV(string cashregisterIdentification, long receiptNumber)
        {
            byte[] inBytes = Encoding.UTF8.GetBytes(cashregisterIdentification + receiptNumber.ToString());
            using (var sha256hash = SHA256Managed.Create())
            {
                return sha256hash.ComputeHash(inBytes).Take(16).ToArray();
            }
        }

        public string EncryptRevenueCounter(long revenue, string cashregisterIdentification, long receiptNumber, byte[] aesKey)
        {
            var encryptedRevenueCounter = Encrypt(EncodeRevenue(revenue), GenerateIV(cashregisterIdentification, receiptNumber), aesKey);
            return Convert.ToBase64String(encryptedRevenueCounter);
        }

        public byte[]? Encrypt(byte[] data, byte[] IV, byte[] aesKey)
        {
            byte[] encrypted;
            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");

            try
            {
                KeyParameter skey = new KeyParameter(aesKey);
                ParametersWithIV aesIVKeyParam = new ParametersWithIV(skey, IV);
                cipher.Init(true, aesIVKeyParam);
            }
            catch (Exception e)
            {
                Console.WriteLine("Encrypt exception (init): " + e.Message);
                return null;
            }

            try
            {
                using (MemoryStream bOut = new MemoryStream())
                using (CipherStream cOut = new CipherStream(bOut, null, cipher))
                {
                    cOut.Write(data, 0, data.Length);
                    encrypted = bOut.ToArray();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Encrypt exception: " + e.Message);
                return null;
            }

            return encrypted;
        }

        public byte[]? Decrypt(byte[] data, byte[] iv, byte[] aesKey)
        {
            byte[] output;

            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");

            try
            {
                KeyParameter skey = new KeyParameter(aesKey);
                ParametersWithIV aesIVKeyParam = new ParametersWithIV(skey, iv);
                cipher.Init(false, aesIVKeyParam);
            }
            catch (Exception e)
            {
                Console.WriteLine("Decrypt exception (init): " + e.Message);
                return null;
            }

            try
            {
                using (MemoryStream bIn = new MemoryStream(data, false))
                using (CipherStream cIn = new CipherStream(bIn, cipher, null))
                using (BinaryReader dIn = new BinaryReader(cIn))
                {
                    output = new byte[data.Length];
                    Array.Clear(output, 0, output.Length);
                    output = dIn.ReadBytes(output.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Decrypt exception : " + e.Message);
                return null;
            }

            return output;
        }

        public byte[] GenerateAesKey()
        {
            using (var aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Mode = CipherMode.CFB;
                aesAlg.IV = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                aesAlg.GenerateKey();
                return aesAlg.Key;
            }
        }
    }
}